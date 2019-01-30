using KryptoAlg;
using KryptoAlg.Interfaces;
using Moq;
using NUnit.Framework;

namespace MessageHandlerTests
{
    [TestFixture]
    public class MessageHandlerTests
    {
        [TestFixture]
        public class ULongTest : MessageHandlerTests
        {
            DecryptedRepresentation _cut;
            Mock<IBitShifter<ulong>> _bitShifterMock;


            [SetUp]
            public void Setup()
            {
                _cut = new DecryptedRepresentation(new UlongBitShifter());
            }

            [TestFixture]
            public class MakeSubmessageFromT_Test : ULongTest
            {
                [Test]
                [TestCase((ulong)0x0000000000000000, "!")]
                [TestCase((ulong)0x0000000000000020, " ")]
                [TestCase((ulong)0x0000000000000041, "A")]
                [TestCase((ulong)0x000000000000007E, "~")]
                [TestCase((ulong)0x0000000000004141, "AA")]
                [TestCase((ulong)0x0000000000414142, "BAA")]
                public void MakeSubmessage(ulong testValue, string expectedText)
                {
                    var result = _cut.CreateSubmessage(testValue);
                    Assert.AreEqual(expectedText, result);
                }

                [Test]
                [TestCase("", (ulong)0)]
                [TestCase(" ", (ulong)0x0000000000000020)]
                [TestCase("A", (ulong)0x0000000000000041)]
                [TestCase("AA", (ulong)0x0000000000004141)]
                [TestCase("BAA", (ulong)0x0000000000414142)]
                public void MakeNumber(string text, ulong expectedValue)
                {
                    var result = _cut.CreateNumber(text);
                    Assert.AreEqual(expectedValue, result);
                }

                [Test]
                public void ShiftLeft()
                {
                    var value = 0xFFFFFFFF;
                    var result = 0x0000FFFF;
                    var toVerify = value >> 0x0000000000000010;
                    Assert.AreEqual(toVerify, result);
                }


                [Test]
                public void ShiftRight()
                {
                    var value = 0xFFFFFFFFFFFFFFFF;
                    var result = 0xFFFFFFFFFFFFFFF0;
                    var toVerify = value << 0x0000000000000004;
                    Assert.AreEqual(toVerify, result);
                }

                [Test]
                public void TakeBytes()
                {
                    var value = 0xFFFFFFFF;
                    var result = 0x00000FFF;
                    var toVerify = value & 0x00000FFF;
                    Assert.AreEqual(toVerify, result);
                }
            }
        }
    }
}
