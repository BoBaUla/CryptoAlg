using System;
using System.Collections.Generic;
using System.Text;

namespace KryptoAlg.Interfaces
{
    public interface IMessage<T>
    {
        List<string> SplitMessage(string message);

        string RecombineMessage(List<string> cipheredMessage);

        T MakeTFromSubmessage(string submessage);

        string MakeSubmessageFromT(T value);
    }
}
