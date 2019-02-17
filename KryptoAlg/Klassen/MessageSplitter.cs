using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;

namespace KryptoAlg
{
    public class DecryptedRepresentation : IMessage<ulong>
    {
        IBitShifter<ulong> _bitShifter;

        public DecryptedRepresentation(IBitShifter<ulong> bitShifter)
        {
            _bitShifter = bitShifter;
        }

        public string CreateSubmessage(ulong value)
        {
            string result = "";

            var charValue = value & 0x00000000000000FF;
            if (!ValidateValueSmaller(charValue))
            {
                charValue += 0x0000000000000021;    
            }
            result += AddValue(result, charValue);
            var shiftedValue = _bitShifter.ShiftRight(value, 8);
            if (ValidateValueSmaller(shiftedValue))
            {
                result += CreateSubmessage(shiftedValue);
            }

            return result;
        }

        private static string AddValue(string result, ulong charValue)
        {
            return ((char)charValue).ToString();
        }

        public ulong CreateNumber(string text)
        {
            ulong result = 0;
            for(int position = 0; position < text.Length; position++)
            {
                var shifted = _bitShifter.ShiftLeft((ulong)text[position] , position * 8);
                result += shifted;
            }
            return result;
        }

        private static bool ValidateValueSmaller(ulong charValue)
        {
            return charValue >= 0x0000000000000020;
        }

        
        /// <summary>
        /// Shifts result about 'shift' Bit to left and adds value
        /// </summary>
        /// <param name="shift">Number of bits to shift left</param>
        /// <param name="value">Value to add</param>
        /// <param name="result">result that has to be changed by value</param>
        private void CreateUlong(int shift, ulong value, ref ulong result)
        {
            result = (result << shift) + value;
        }

        [Obsolete("untested method")]
        public string MakeSubmessageFromT(ulong value)
        {
            string result = "";
            while (result.Length * sizeof(char) < sizeof(ulong))
            {
                var shifting = ((int)EMessageSplit.ULong_Bits - (int)EMessageSplit.Char_Bits);
                var afterShift = (value >> shifting);
                char val = (char)(afterShift & (ulong)CBitValues.Bit32FromRight);
                value = value << (int)EMessageSplit.Char_Bits;
                result += val;
            }
            return result;
        }

        [Obsolete("untested method")]
        public ulong MakeTFromSubmessage(string submessage)
        {
            if (submessage.Length * sizeof(char) != sizeof(ulong))
                throw new Exception("Submessage has wrong length");
            ulong result = 0;
            foreach (char ch in submessage)
                CreateUlong((int)EMessageSplit.Char_Bits, ch, ref result);
            return result;
        }
    }
}
