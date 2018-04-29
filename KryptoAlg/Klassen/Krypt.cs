using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;
using System.Collections.Generic;
using System.Text;

namespace KryptoAlg
{
    public class Krypt<T>: IKrypt
    {
        IAlgorithm<T> _cipherAlgorithm;
        ITranslation<T> _translation;
        IMessage<T> _messageHandler;
        
        public Krypt(IAlgorithm<T> alg, ITranslation<T> trans, IMessage<T> messageHandler)
        {
            _cipherAlgorithm = alg;
            _translation = trans;
            _messageHandler = messageHandler;
        }

        public string DecryptMessage(string message)
        {
            string result = "";
            for(int i = 0; i < message.Length - 1; i = i + (int)EMessageSplit.Char_Bits)
            {
                string substring = message.Substring(i, (int)EMessageSplit.Char_Bits);
                T val = _translation.TranslateString(substring);
                result  += _messageHandler.MakeSubmessageFromT(_cipherAlgorithm.Decrypt(val));
            }
            return result;
        }

        public string EncryptMessage(string message)
        {
            string result = "";
            foreach (string val in _messageHandler.SplitMessage(message))
            {
                T temp = _cipherAlgorithm.Encrypt(_messageHandler.MakeTFromSubmessage(val));
                result += _translation.TranslateNumber(temp);
            }
            return result;
        }

        public void SetKey(string key)
        {
            
        }
    }
}
