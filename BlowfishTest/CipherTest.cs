using System;
using KryptoAlg;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlowfishTest
{
    [TestClass]
    public class CipherTest
    {
        ulong _val = 0x1234567809ABCDEF;

        [TestMethod]
        public void TestEncryption()
        {
            Blowfish blow = new Blowfish();
            ulong crypt = blow.Encrypt(_val);
            if (crypt == _val)
                throw new Exception("No encrytion");
        }

        [TestMethod]
        public void TestDecryption()
        {
            Blowfish blow = new Blowfish();
            ulong crypt = blow.Encrypt(_val);
            if (blow.Decrypt(crypt) != _val) 
                throw new Exception("No decryption");
        }

        [TestMethod]
        public void TestSetKeyAndEncryption()
        {
            Blowfish blow = new Blowfish();
            blow.SetKey(0XFFFF0000);
            blow.StartSettings();
            ulong crypt = blow.Encrypt(_val);
            if (blow.Decrypt(crypt) != _val)
                throw new Exception("No decryption");
        }

        [TestMethod]
        public void TestSetDifferentKey()
        {
            Blowfish blow1 = new Blowfish();
            Blowfish blow2 = new Blowfish();
            blow1.SetKey((uint)5555);
            blow1.StartSettings();
            blow2.SetKey((uint)1225);
            blow2.StartSettings();

            ulong crypt1 = blow1.Encrypt(_val);
            ulong crypt2 = blow2.Encrypt(_val);

            if (crypt1 == crypt2)
                throw new Exception("No decryption");
        }

        [TestMethod]
        public void TestSetKeyAndEncryptionFail()
        {
            Blowfish blow = new Blowfish();
            blow.SetKey(0XFFFF0000);
            blow.StartSettings();
            ulong crypt = blow.Encrypt(_val);
            blow.SetKey(0X0000FFFF);
            blow.StartSettings();
            if (blow.Decrypt(crypt) == _val)
                throw new Exception("No decryption");
        }
    }
}
