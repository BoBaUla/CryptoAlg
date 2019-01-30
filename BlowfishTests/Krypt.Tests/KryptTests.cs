using KryptoAlg;
using KryptoAlg.Interfaces;
using KryptoAlg.Typen;
using Moq;
using NUnit.Framework;

namespace KryptTests
{
    [TestFixture]
    public class KryptTests
    {
        CipherService _cut;
        CipherService _ctc;


        [SetUp]
        public void Setup()
        {
            _cut = new CipherService();
            _cut.SetKey("1234");
            _ctc = new CipherService();
            _ctc.SetKey("4321");
        }


        [TestFixture]
        public class EncryptTests : KryptTests
        {
            [Test]
            public void Encryption()
            {
                var toEncrypt = "Hallo liebe Welt";

                var result = _cut.EncryptMessage(toEncrypt);

                Assert.AreNotEqual(result, toEncrypt);
            }

            [Test]
            public void DifferentKeys()
            {
                var toEncrypt = "Hallo liebe Welt";
                var result1 = _cut.EncryptMessage(toEncrypt);
                var result2 = _ctc.EncryptMessage(toEncrypt);

                Assert.AreNotEqual(result1, result2);
            }
        }

        [TestFixture]
        public class DecryptTests : KryptTests
        {
            [Test]
            [TestCase("Hallo")]
            [TestCase("Hallo Welt")]
            [TestCase("Hallo schöne Welt!")]
            public void Decryption(string message)
            {
                var toEncrypt = message;

                var resultEncrypted = _cut.EncryptMessage(toEncrypt);

                var resultDecrypted = _cut.DecryptMessage(resultEncrypted);

                Assert.AreEqual(toEncrypt, resultDecrypted);
                Assert.AreNotEqual(resultEncrypted, toEncrypt);
            }

            [Test]
            public void DifferentKeys()
            {
                var toEncrypt = "Hallo";
                var resultEncrypted = _cut.EncryptMessage(toEncrypt);

                var result1 = _cut.DecryptMessage(resultEncrypted);
                var result2 = _ctc.DecryptMessage(resultEncrypted);
                Assert.AreNotEqual(result1, result2);
            }
        }
    }

    [TestFixture]
    public class CreateKeyFromString
    {
        [Test]
        [TestCase(" ")]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("acc")]
        [TestCase("aaaa")]
        [TestCase("aaaaa")]
        [TestCase("aaaaaa")]
        [TestCase("aaaaaaa")]
        [TestCase("aaaaaaaa")]
        public void NumberCreatorTest_NotNull(string text)
        {
            var result = NumberCreator.CreateNumber(text);
            Assert.IsNotNull(result);
        }

        [Test]
        [TestCase("19", "91")]
        [TestCase("1", "11")]
        public void NumberCreatorTest_NotEquals(string text1, string text2)
        {
            var result1 = NumberCreator.CreateNumber(text1);
            var result2 = NumberCreator.CreateNumber(text2);
            Assert.AreNotEqual(result1, result2);
        }


    }
}
