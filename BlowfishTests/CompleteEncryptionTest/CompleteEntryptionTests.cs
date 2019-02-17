using KryptoAlg;
using KryptoAlg.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CompleteEntryptionTests
{
    [TestFixture]
    public class CompleteEntryptionTests
    {
        IKrypt _cut;

        [SetUp]
        public void Setup()
        {
            _cut = new CipherService();
            _cut.SetKey("0815");
        }

        [TestFixture]
        public class CipherString : CompleteEntryptionTests
        {
            [Test]
            public void Encrption()
            {
                string input = "hallo welt";
                var result = _cut.EncryptMessage(input);

                Assert.AreNotEqual(input, result);
                for(int character = 0; character < input.Length; character++)
                {

                    Assert.AreNotEqual(input[character], result[character]);
                }
            }
        }

        [TestFixture]
        public class QualityOfSBoxesInitialisationTest : CompleteEntryptionTests
        {
            IKrypt _cutHelper;

            string result1;
            string result2;

            static string input1 = "abcdefghijklmnopqrst";
            static string input2 = "123456789jklmnopqrst";
            [SetUp]
            public new void Setup()
            {
                _cutHelper = new CipherService();
                _cutHelper.SetKey("abcde");

                result1 = _cut.EncryptMessage(input1);
                result2 = _cutHelper.EncryptMessage(input1);
            }
            
            [Test, TestCaseSource(nameof(input1))]
            public void WrongDecryptionDoesNotContainKeyElements(char character )
            {
                var position = input1.IndexOf(character);
                var result = _cutHelper.DecryptMessage(result1);
                var value = (char)result[position];
                Assert.AreNotEqual(character, value);
            }

            [Test, TestCaseSource(nameof(input2))]
            public void WrongDecryptionDoesNotContainKeyElementsEvaluation(char character)
            {
                var position = input2.IndexOf(character);
                var result = _cutHelper.DecryptMessage(result1);
                var value = (char)result[position];
                Assert.AreNotEqual(character, value);
            }
        }
    }
}
