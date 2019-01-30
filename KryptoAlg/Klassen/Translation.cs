using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KryptoAlg
{
    public class EncryptedRepresentation : ITranslation<ulong>
    {
        private string _dict = "ACDEFGHJKLMPRTUW";
        private IBitShifter<ulong> _bitShifter;

        public EncryptedRepresentation(IBitShifter<ulong> bitShifter)
        {
            _bitShifter = bitShifter;
        }

        /// <summary>
        /// Characters, that will be used to express an ulong value
        /// </summary>
        public string DICT
        {
            get { return _dict; }
            set {
                if (value.Length == (int)ETranslation.DICT_Length)
                    _dict = value;
                else
                    throw new Exception("DICT length is unequal to " + ETranslation.DICT_Length);
                }
        }

        public string NumberToString(ulong number)
        {
            string result = "";
            var val = number & 0x000000000000000F;
            result += DICT[(int)val];
            var shiftedNumber = _bitShifter.ShiftRight(number, (int)ETranslation.DICT_Bits);
            if (shiftedNumber != 0)
            {
                result += NumberToString(shiftedNumber);
            }
            return result;
        }

        public ulong StringToNumber(string text)
        {
            ulong result = 0x0;
            result += (ulong)DICT.IndexOf(text.First());
            var subtext = text.Substring(1);
            if(!string.IsNullOrEmpty(subtext))
            {
                var subresult = StringToNumber(subtext);
                subresult = _bitShifter.ShiftLeft(subresult,4);
                result += subresult;
            }
            return result;
        }

        [Obsolete("Prefer StringToNumber")]
        public ulong TranslateString(string text)
        {
            ulong result = 0;
            if (text.Length > (int)ETranslation.ULong_Bits/(int)ETranslation.DICT_Bits)
                throw new Exception("Text is out of range.");
            for (int i = 0; i < text.Length; i++)
            {
                result += (ulong)DICT.IndexOf(text[i]);
                if (i != text.Length - 1)
                    result = result << (int)ETranslation.DICT_Bits;
            }
            return result;
        }

        /// <summary>
        /// Expresses the giben ulong as a string, by using DICT
        /// </summary>
        /// <param name="number">Number that has to be expressed</param>
        /// <returns></returns>
        [Obsolete("Prefer NumberToString")]
        public string TranslateNumber(ulong number)
        {
            string result = "";
            for (int i = 0; i < (int)ETranslation.DICT_Length; i++)
            {
                int val = (int)((number & CBitValues.Bit4FromLeft) >> (int)ETranslation.ULong_Bits - (int)ETranslation.DICT_Bits);
                result += DICT[val];
                number = number << (int)ETranslation.DICT_Bits;
            }
            return result;
        }

    }
}
