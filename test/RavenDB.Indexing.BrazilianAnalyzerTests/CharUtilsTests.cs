using NUnit.Framework;
using RavenDB.Indexing.BrazilianAnalyzer;

namespace RavenDB.Indexing.BrazilianAnalyzerTests
{
    [TestFixture]
    public class CharUtilsTests
    {
        [TestCase('á', 'a')]
        [TestCase('Á', 'a')]
        [TestCase('ü', 'u')]
        [TestCase('à', 'a')]
        [TestCase('ç', 'c')]
        [TestCase('Ç', 'c')]
        public void ToLowerAndRemovingAccentMarksShouldResultIn(
            char input,
            char expectedOutput
        )
        {
            Assert.AreEqual(
                expectedOutput,
                CharUtils.RemoveAccentMark(CharUtils.ToLower(input))
            );
        }
    }
}

