using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;

namespace RavenDB.Indexing.BrazilianAnalyzer
{
    public class RavenBrazilianAnalyzer : StandardAnalyzer
    {
        private readonly Version _matchVersion;

        public RavenBrazilianAnalyzer(Version matchVersion) : base(matchVersion)
        {
            _matchVersion = matchVersion;
        }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            var tokenStream = new StandardTokenizer(_matchVersion, reader)
            {
                MaxTokenLength = DEFAULT_MAX_TOKEN_LENGTH
            };

            var res = new RavenBrazilianFilter(tokenStream);
            PreviousTokenStream = res;
            return res;
        }

        public override TokenStream ReusableTokenStream(string fieldName, TextReader reader)
        {
            var previousTokenStream = PreviousTokenStream as RavenBrazilianFilter;
            if (previousTokenStream == null)
                return TokenStream(fieldName, reader);

            // if the inner tokenazier is successfuly reset
            if (previousTokenStream.Reset(reader))
            {
                return previousTokenStream;
            }

            // we failed so we generate a new token stream
            return TokenStream(fieldName, reader); ;
        }
    }
}
