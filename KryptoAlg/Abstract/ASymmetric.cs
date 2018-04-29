using System;
using System.Collections.Generic;
using System.Text;

namespace KryptoAlg.Abstract
{
    public abstract class ASymmetric<T>
    {
        protected T _key;

        public abstract void SetKey(T Key);
    }
}
