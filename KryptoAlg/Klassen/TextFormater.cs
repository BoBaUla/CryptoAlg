using KryptoAlg.Interfaces;
using System.Collections.Generic;

namespace KryptoAlg
{
    public class TextFormater: ITextFormater
    {
        public string RecombineMessage(List<string> cipheredMessage)
        {
            string result = "";
            foreach (string str in cipheredMessage)
                result += str;
            return result.Trim();
        }

        public List<string> SplitMessage(string message, int blockSize)
        {
            /*  Jeder char hat 16 bit, daher repräsentiert ein ulong 4 char.
             *  Die message muss durch 4 teilbar sein, damit der Split aufgeht
             *  Sonst werden " " angehängt, bis der Split aufgeht
             *  " " wurde gewählt, da dieser leicht durch String.Trim() bereinigt werden kann
             */
            for (int i = 0; i < message.Length % blockSize; i++)
                message += " ";
            List<string> result = new List<string>();
            for (int i = 0; i < message.Length; i = i + blockSize)
                result.Add(message.Substring(i, blockSize));
            return result;
        }

    }
}
