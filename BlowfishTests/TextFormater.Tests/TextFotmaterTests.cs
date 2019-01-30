using KryptoAlg;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TextFormaterTests
{
    [TestFixture]
    public class TextFotmaterTests
    {
        TextFormater _cut;

        [SetUp]
        public void Setup()
        {
            _cut = new TextFormater();
        }

        [Test]
        [TestCase("Ein Wort", 1)]
        [TestCase("Ein langes Wort", 2)]
        [TestCase("Ein langes Wort mit vielen Zeichen", 5)]
        public void SplitMessage(string text, int expectedItemCount)
        {
            var result = _cut.SplitMessage(text, 8);

            Assert.AreEqual(expectedItemCount, result.Count);
            Assert.IsTrue(result.First().Length == 8);
            Assert.IsTrue(result.Last().Length == 8);
        }

        [Test]
        public void RecombineMessage()
        {
            var listToTest = new List<string>
                        {
                            "Ein Wort",
                            " mit vie",
                            "len Zeic",
                            "hen     "
                        };
            var expectedResult = "Ein Wort mit vielen Zeichen";

            var result = _cut.RecombineMessage(listToTest);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
