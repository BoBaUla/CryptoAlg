using System;
using KryptoAlg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlowfishTest
{
    [TestClass]
    public class TestSplitting
    {
        [TestMethod]
        public void TestSplit64()
        {
            Blowfish blow = new Blowfish();
            uint[] test = blow.Split64(0xAFFFFFFFFBFFFFFF);
            if (test[0] != 0xAFFFFFFF)
                throw new Exception("wrong splitting");
            if (test[1] != 0xFBFFFFFF)
                throw new Exception("wrong splitting");
        }

        [TestMethod]
        public void TestSplit8()
        {
            Blowfish blow = new Blowfish();
            uint[] test = blow.Split8(0xAABBCCDD);
            for (int i = 0; i < test.Length; i++)
                if (test[i] != (0xDD - 0x11 * i) )
                    throw new Exception("wrong splitting");
        }


        [TestMethod]
        public void TestRecomination()
        {
            Blowfish blow = new Blowfish();
            if (blow.Recombine64(0xAAAAAAAA, 0xBBBBBBBB) != 0xAAAAAAAABBBBBBBB)
                throw new Exception("wrong recomination");
        }
    }
}
