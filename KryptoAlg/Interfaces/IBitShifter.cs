namespace KryptoAlg.Interfaces
{
    public interface IBitShifter<T>
    {
        T ShiftRight(T value, int bitsToShift);
        T ShiftLeft(T value, int bitsToShift);
    }

    public class UlongBitShifter : IBitShifter<ulong>
    {
        public ulong ShiftLeft(ulong value, int bitsToShift)
        {
            return value << bitsToShift;
        }

        public ulong ShiftRight(ulong value, int bitsToShift)
        {
            return value >> bitsToShift;
        }
    }
}
