using KryptoAlg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace KryptoAlg.Klassen
{
    public class SerialCreator: ISerial<string>
    {
        ISerial<ulong> _serial;
        IAlgorithm<ulong> _algorithm;
        ITranslation<ulong> _translation;

        public SerialCreator(ISerial<ulong> serial, IAlgorithm<ulong> algorithm, ITranslation<ulong> trans)
        {
            _serial = serial;
            _algorithm = algorithm;
            _translation = trans;
        }

        public string CreateSerial(uint productID, uint customerID, DateTime date)
        {
            ulong serialTemp = _serial.CreateSerial(productID, customerID, date);
            ulong serialEncrypted = _algorithm.Encrypt(serialTemp);
            return _translation.TranslateNumber(serialEncrypted);
        }

        public uint GetCustomerID(string serial)
        {
            ulong serialEncrypted = _translation.TranslateString(serial);
            ulong serialTemp = _algorithm.Decrypt(serialEncrypted);
            return _serial.GetCustomerID(serialTemp);
        }

        public DateTime GetDate(string serial)
        {

            ulong serialEncrypted = _translation.TranslateString(serial);
            ulong serialTemp = _algorithm.Decrypt(serialEncrypted);
            return _serial.GetDate(serialTemp);
        }

        public uint GetProductID(string serial)
        {
            ulong serialEncrypted = _translation.TranslateString(serial);
            ulong serialTemp = _algorithm.Decrypt(serialEncrypted);
            return _serial.GetProductID(serialTemp);
        }
    }
}
