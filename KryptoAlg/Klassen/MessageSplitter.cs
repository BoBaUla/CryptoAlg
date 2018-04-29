using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;
using System.Collections.Generic;
using System.Text;

namespace KryptoAlg
{
    public class MessageSplitter : IMessage<ulong>
    {
        public string MakeSubmessageFromT(ulong value)
        {
            string result = "";
            while(result.Length * sizeof(char) < sizeof(ulong))
            {
                char val = (char)((value >> (
                    (int)EMessageSplit.ULong_Bits - (int)EMessageSplit.Char_Bits)
                    ) & (ulong)CBitValues.Bit32FromRight);
                value = value << (int)EMessageSplit.Char_Bits;
                result += val;
            }
            return result;
        }

        public ulong MakeTFromSubmessage(string submessage)
        {
            if (submessage.Length * sizeof(char) != sizeof(ulong))
                throw new Exception("Submessage has wrong length");
            ulong result = 0;
            foreach (char ch in submessage)
                createUlong((int)EMessageSplit.Char_Bits, ch, ref result);
            return result;
        }

        public string RecombineMessage(List<string> cipheredMessage)
        {
            string result = "";
            foreach (string str in cipheredMessage)
                result += str;
            return result;
        }
        
        public List<string> SplitMessage(string message)
        {
            /*  Jeder char hat 16 bit, daher repräsentiert ein ulong 4 char.
             *  Die message muss durch 4 teilbar sein, damit der Split aufgeht
             *  Sonst werden " " angehängt, bis der Split aufgeht
             *  " " wurde gewählt, da dieser leicht durch String.Trim() bereinigt werden kann
             */
            for (int i = 0; i < message.Length % (int)EMessageSplit.CharsInSplitMessage; i++)
                message += " ";
            List<string> result = new List<string>();
            for (int i = 0; i < message.Length; i = i + (int)EMessageSplit.CharsInSplitMessage)
                result.Add(message.Substring(i, (int)EMessageSplit.CharsInSplitMessage));
            return result;
        }

        /// <summary>
        /// Shifts result about 'shift' Bit to left and adds value
        /// </summary>
        /// <param name="shift">Number of bits to shift left</param>
        /// <param name="value">Value to add</param>
        /// <param name="result">result that has to be changed by value</param>
        private void createUlong(int shift, ulong value, ref ulong result)
        {
            result = (result << shift) + value;
        }
    }
}
