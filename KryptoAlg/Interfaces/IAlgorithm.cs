namespace KryptoAlg.Interfaces
{
    public interface IAlgorithm<T>
    {
        T Encrypt(T toEncrypt);
        T Decrypt(T toDecrypt);
        void StartSettings();
    }

  
}
