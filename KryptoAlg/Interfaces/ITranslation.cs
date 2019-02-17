namespace KryptoAlg.Interfaces
{
    public interface ITranslation<T>
    {
        string DICT { get; set; }

        string TranslateNumber(T number);
        T TranslateString(string text);
    }
}
