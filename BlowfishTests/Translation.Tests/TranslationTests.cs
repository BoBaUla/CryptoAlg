using KryptoAlg;
using KryptoAlg.Interfaces;
using NUnit.Framework;

namespace TranslationTests
{
    [TestFixture]
    public class TranslationTests
    {
        KryptoAlg.EncryptedRepresentation _cut;

        [SetUp]
        public void Setup()
        {
            _cut = new KryptoAlg.EncryptedRepresentation(new UlongBitShifter());
        }

        [TestFixture]
        public class TranslateNumber: TranslationTests
        {
            [TestCase((ulong)0x0000000000000001, "C")]
            [TestCase((ulong)0x0000000000000010, "AC")]
            [TestCase((ulong)0x0000000000000101, "CAC")]
            [TestCase((ulong)0x1000000000000021, "CDAAAAAAAAAAAAAC")]
            public void ULongToString(ulong numberToTest, string expectedResult)
            {
                var result = _cut.NumberToString(numberToTest);
                Assert.AreEqual(expectedResult, result);
            }
        }

        [TestFixture]
        public class TranslateString : TranslationTests
        {
            [TestCase((ulong)0x0000000000000001, "C")]
            [TestCase((ulong)0x0000000000000010, "AC")]
            [TestCase((ulong)0x0000000000000101, "CAC")]
            [TestCase((ulong)0x1000000000000021, "CDAAAAAAAAAAAAAC")]
            public void StringToSULong(ulong expectedNumber, string stringToTest)
            {
                var result = _cut.StringToNumber(stringToTest);
                Assert.AreEqual(expectedNumber, result);
            }
        }
    }
}
