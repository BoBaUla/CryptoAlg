using System;
using KryptoAlg;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TranslationTest
{
    [TestClass]
    public class Translationtest
    {
        [TestMethod]
        public void TestTranslationToString()
        {
            Translation trans = new Translation();
            string val = trans.TranslateNumber(0x0123456789ABCDEF);
            for (int i = 0; i < 16; i++)
                if (val[i] != trans.DICT[i])
                    throw new Exception("Übersetzung falsch");
        }

        [TestMethod]
        public void TestTranslationToNumber()
        {
            Translation trans = new Translation();
            ulong val = trans.TranslateString(trans.DICT);
                if (val != 0x0123456789ABCDEF)
                    throw new Exception("Übersetzung falsch");
        }

    }
}
