using System;

namespace KryptoAlg.Interfaces
{
    public interface ISerial<T>
    {
        T CreateSerial(uint productID, uint customerID, DateTime date);

        uint GetProductID(T serial);
        uint GetCustomerID(T serial);
        DateTime GetDate(T serial);
    }
}
