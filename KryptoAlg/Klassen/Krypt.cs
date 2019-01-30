using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptoAlg
{
    public class CipherService : IKrypt
    {
        Blowfish _cipherAlgorithm;
        ITranslation<ulong> _encryptedRepresentation;
        IMessage<ulong> _decryptedRepresentation;
        ITextFormater _textFormater;

        public CipherService()
        {
            _cipherAlgorithm = new Blowfish();
            _encryptedRepresentation = new EncryptedRepresentation(new UlongBitShifter());
            _decryptedRepresentation = new DecryptedRepresentation(new UlongBitShifter());
            _textFormater = new TextFormater();
        }

        public string DecryptMessage(string message)
        {
            string result = "";
            var text = _textFormater.SplitMessage(message,16);
            foreach (var subtext in text)
            {
                var val = _encryptedRepresentation.TranslateString(subtext);
                var decrypted = _cipherAlgorithm.Decrypt(val);
                result  += _decryptedRepresentation.CreateSubmessage(decrypted);
            }
            return result.Trim();
        }

        public string EncryptMessage(string message)
        {
            string result = "";
            var text = _textFormater.SplitMessage(message, 8);
            foreach (var subtext in text)
            {
                var val = _decryptedRepresentation.CreateNumber(subtext);
                var decrypted = _cipherAlgorithm.Encrypt(val);
                result += _encryptedRepresentation.TranslateNumber(decrypted);
            }
            return result;
        }

        public void SetKey(string key)
        {
            _cipherAlgorithm = new Blowfish();
            _cipherAlgorithm.SetKey(NumberCreator.CreateNumber(key));
            _cipherAlgorithm.StartSettings();
        }
    }
    public class NumberCreator
    {
        public static uint CreateNumber(string text)
        {
            uint temp1 = text.Last();
            uint temp2 = text.First();
            foreach (var character in text)
            {
                temp2 = ((uint)temp2 * (uint)character) ^ 0xFFFF0000;
                temp1 = ((uint)temp1 * (uint)character) ^ 0x0000FFFF;
            }
            var result = temp2 ^ temp1;
            return result;
        }
    }
}
