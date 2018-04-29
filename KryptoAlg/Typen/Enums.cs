using System;
using System.Collections.Generic;
using System.Text;

namespace KryptoAlg.Typen
{
    internal enum EBlowfish
    {
        Steps = 2,
        sBoxesCount1 = 4,
        Blocks = 8,
        Rounds = 16,
        pArrayLength = 18,
        sBoxesCount2 = 256
    }

    internal enum ETranslation
    {
        DICT_Bits = 4,
        DICT_Length  = 16,
        ULong_Bits = 64
    }

    internal enum EMessageSplit
    {
        CharsInSplitMessage  = 4,
        Char_Bits = 16,
        ULong_Bits = 64
    }

    internal static class CBitValues
    {
        internal static ulong Bit4FromRight = 0xF;
        internal static ulong Bit8FromRight = 0xFF;
        internal static ulong Bit16FromRight = 0xFF;
        internal static ulong Bit32FromRight = 0xFFFF;
        internal static ulong Bit64FromRight = 0xFFFFFFFF;

        internal static ulong Bit4FromLeft = 0xF000000000000000;
    }
    
    internal static class CSerialPosition
    {
        internal static uint ProductID = 16;
        internal static uint CustomerID = 16;
        internal static uint Day = 5;
        internal static uint Month = 4;
        internal static uint Year = 10;

        internal static ulong BitProductID = 0xFFFF;
        internal static ulong BitCustomerID = 0xFFFF;
        internal static ulong BitDay = 0x1F;
        internal static ulong BitMonth = 0xF;
        internal static ulong BitYear = 0x3FF;

        internal static int GetProductIDShift()
        {
            return (int)(CustomerID + Day + Month + Year);
        }
        internal static int GetCustomerIDShift()
        {
            return (int)(Day + Month + Year);
        }
        internal static int GetDayShift()
        {
            return (int)(Month + Year);
        }
        internal static int GetMonthShift()
        {
            return (int)(Year);
        }
        internal static int GetYearShift()
        {
            return 0;
        }
    }
}
