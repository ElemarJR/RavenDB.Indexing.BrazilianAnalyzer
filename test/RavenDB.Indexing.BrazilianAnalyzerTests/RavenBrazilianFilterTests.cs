using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Util;
using NUnit.Framework;
using RavenDB.Indexing.BrazilianAnalyzer;

namespace RavenDB.Indexing.BrazilianAnalyzerTests
{
    [TestFixture]
    public class RavenBrazilianFilterTests
    {
        [TestCase("À moda Mem de sá", new [] {"moda", "mem", "sa"})]
        [TestCase("Guaraná Fantástica", new[] { "guarana", "fantastica"})]
        public void Case1(string input, string[] expectedTokens)
        {
            var tokenizer = new StandardTokenizer(Version.LUCENE_30, new StringReader(input));
            var sut = new RavenBrazilianFilter(tokenizer) as TokenStream;

            var term = sut.AddAttribute<ITermAttribute>();

            var index = 0;
            sut.Reset();
            while (sut.IncrementToken())
            {
                Assert.AreEqual(expectedTokens[index], term.Term);
                index++;
            }
            Assert.AreEqual(expectedTokens.Length, index);
        }
    }
}
