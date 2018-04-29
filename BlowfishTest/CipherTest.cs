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
    }
}
