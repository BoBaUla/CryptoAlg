using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;
using System.Collections.Generic;
using System.Text;

namespace KryptoAlg
{
    public class Translation : ITranslation<ulong>
    {
        private string _dict = "ACDEFGHJKLMPRTUW";

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
        
        /// <summary>
        /// Expresses the giben ulong as a string, by using DICT
        /// </summary>
        /// <param name="number">Number that has to be expressed</param>
        /// <returns></returns>
        public string TranslateNumber(ulong number)
        {
            string result = "";
            for(int i = 0; i < (int)ETranslation.DICT_Length; i++)
            {
                int val = (int)((number & CBitValues.Bit4FromLeft) >> (int)ETranslation.ULong_Bits - (int)ETranslation.DICT_Bits);
                result += DICT[val];
                number = number << (int)ETranslation.DICT_Bits;
            }
            return result;
        }

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
    }
}
