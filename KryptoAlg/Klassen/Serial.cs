using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using System;

namespace KryptoAlg
{
    public class Serial : ISerial<ulong>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID">ID between 0 and 65535</param>
        /// <param name="customerID">ID between 0 and 65535</param>
        /// <param name="date"></param>
        /// <returns></returns>
        public ulong CreateSerial(uint productID, uint customerID, DateTime date)
        {
            ulong result = 0;
            int day = date.Day;
            int month = date.Month;
            int year = date.Year - 1000;
            AddToUlong(16, productID, ref result);
            AddToUlong(16, customerID, ref result);
            AddToUlong(5, (uint)day, ref result);
            AddToUlong(4, (uint)month, ref result);
            AddToUlong(10, (uint)year, ref result);
            return result;
        }

        public uint GetCustomerID(ulong serial)
        {
            return (uint)((serial >> CSerialPosition.GetCustomerIDShift()) & CSerialPosition.BitCustomerID);
        }

        public DateTime GetDate(ulong serial)
        {
            int day = (int)((serial >> CSerialPosition.GetDayShift()) & CSerialPosition.BitDay);
            int month = (int)((serial >> CSerialPosition.GetMonthShift()) & CSerialPosition.BitMonth);
            int year = (int)((serial >> CSerialPosition.GetYearShift()) & CSerialPosition.BitYear) + 1000;
            return new DateTime(year, month, day);
        }

        public uint GetProductID(ulong serial)
        {
            return (uint)((serial >> CSerialPosition.GetProductIDShift()) & CSerialPosition.BitProductID);
        }

        private void AddToUlong(int shiftLeft, uint value, ref ulong result)
        {
            result = (result << shiftLeft);
            result += value;
        }
    }
}
