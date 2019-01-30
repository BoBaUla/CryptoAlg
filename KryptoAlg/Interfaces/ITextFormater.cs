using System.Collections.Generic;

namespace KryptoAlg.Interfaces
{
    public interface ITextFormater
    {
        List<string> SplitMessage(string message, int blockSize);
        string RecombineMessage(List<string> cipheredMessage);
    }
}
