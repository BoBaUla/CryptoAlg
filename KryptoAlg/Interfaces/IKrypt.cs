namespace KryptoAlg.Interfaces
{
    /// <summary>
    /// Offers methods to implement the cipher-algorithm
    /// </summary>
    public interface IKrypt
    {
        void SetKey(string key);
        string EncryptMessage(string message);
        string DecryptMessage(string message);
    }
}
