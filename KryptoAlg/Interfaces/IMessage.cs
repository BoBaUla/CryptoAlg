using System;
using System.Collections.Generic;
using System.Text;

namespace KryptoAlg.Interfaces
{
    public interface IMessage<T>
    {
        ulong CreateNumber(string text);

        string CreateSubmessage(ulong value);
    }
}
