using KryptoAlg;
using NUnit.Framework;

namespace BlowfishTests
{
    [TestFixture]
    public class BlowfishTests
    {
        Blowfish _cut;
        Blowfish _ContollClass;

        uint _key = uint.MaxValue;
        uint _differentKey = uint.MaxValue - 1;
        [SetUp]
        public void Setup()
        {
            _cut = new Blowfish();
            _cut.SetKey(_key);
            _cut.StartSettings();

            _ContollClass = new Blowfish();
            _ContollClass.SetKey(_differentKey);
            _ContollClass.StartSettings();
        }
        
        [TestFixture]
        public class StartSettingsTest : BlowfishTests
        {
            [TestFixture]
            public class PArrayTest : StartSettingsTest
            {
                [Test]
                public void InitPArray_NotNull()
                {
                    var pArray = _cut.PArray;
                    Assert.IsNotNull(pArray);
                }

                [Test]
                public void InitPArray_ValuesAreIndependentOfInitialisation()
                {
                    var pArrayOrigin = _cut.PArray;
                    var pArrayControl = _ContollClass.PArray;

                    Assert.AreEqual(pArrayOrigin, pArrayControl);
                }
            }

            public class SBoxTest : StartSettingsTest
            {
                [Test]
                public void InitSBoxes_NotNull()
                {
                    var sBox = _cut.SBox;
                    Assert.IsNotNull(sBox);
                }

                [Test]
                public void InitPArray_ValuesAreIndependentOfInitialisation()
                {
                    var sBoxOrigin = _cut.SBox;
                    var sBoxControl = _ContollClass.SBox;

                    Assert.AreNotEqual(sBoxOrigin, sBoxControl);
                }
            }


        }

        [TestFixture]
        public class EncryptTests: BlowfishTests
        {
            [Test]
            public void Encryption()
            {
                var toEncrypt = ulong.MaxValue;

                var result = _cut.Encrypt(toEncrypt);

                Assert.AreNotEqual(result, toEncrypt);
            }

            [Test]
            public void DifferentKeys()
            {
                var toEncrypt = ulong.MaxValue;
                var result1 = _cut.Encrypt(toEncrypt);
                var result2 = _ContollClass.Encrypt(toEncrypt);

                Assert.AreNotEqual(result1, result2);
            }
        }
        
        [TestFixture]
        public class DecryptTests : BlowfishTests
        {
            [Test]
            public void Decryption()
            {
                var toEncrypt = ulong.MaxValue;

                var resultEncrypted = _cut.Encrypt(toEncrypt);

                var resultDecrypted = _cut.Decrypt(resultEncrypted);

                Assert.AreEqual(toEncrypt, resultDecrypted);
                Assert.AreNotEqual(resultEncrypted, toEncrypt);
            }

            [Test]
            [TestCase((uint)1)]
            [TestCase((uint)2)]
            [TestCase((uint)3)]
            [TestCase((uint)4)]
            [TestCase((uint)5)]
            [TestCase((uint)6)]
            [TestCase((uint)7)]
            [TestCase((uint)8)]
            [TestCase((uint)9)]
            [TestCase((uint)10)]
            [TestCase(uint.MaxValue / 2)]
            public void DifferentKeys(uint keyChange)
            {
                _ContollClass = new Blowfish();
                _ContollClass.SetKey(_key - keyChange);
                _ContollClass.StartSettings();

                var toEncrypt = ulong.MaxValue;
                var resultEncrypted = _cut.Encrypt(toEncrypt);

                var result1 = _cut.Decrypt(resultEncrypted);
                var result2 = _ContollClass.Decrypt(resultEncrypted);
                Assert.AreNotEqual(result1, result2);
            }
        }

    }
}
