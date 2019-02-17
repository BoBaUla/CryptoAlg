namespace KryptoAlg.Interfaces
{
    public interface IMessage<T>
    {
        ulong CreateNumber(string text);

        string CreateSubmessage(ulong value);
    }
}
