namespace KryptoAlg.Abstract
{
    public abstract class ASymmetric<T>
    {
        protected T _key;
        protected T _salt;

        public abstract T UseKeyWithSalt();
        public abstract void SetKey(T Key);

        public void SetSalt(T salt)
        {
            _salt = salt;
        }
    }
}
