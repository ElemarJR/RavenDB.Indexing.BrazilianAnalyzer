using System;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Analysis.Tokenattributes;

namespace RavenDB.Indexing.BrazilianAnalyzer
{
    public sealed class RavenBrazilianFilter : TokenFilter
    {
        public static string[] BRAZILIAN_STOP_WORDS = {
            "a","ainda","alem","ambas","ambos","antes",
            "ao","aonde","aos","apos","aquele","aqueles",
            "as","assim","com","como","contra","contudo",
            "cuja","cujas","cujo","cujos","da","das","de",
            "dela","dele","deles","demais","depois","desde",
            "desta","deste","dispoe","dispoem","diversa",
            "diversas","diversos","do","dos","durante","e",
            "ela","elas","ele","eles","em","entao","entre",
            "essa","essas","esse","esses","esta","estas",
            "este","estes","ha","isso","isto","logo","mais",
            "mas","mediante","menos","mesma","mesmas","mesmo",
            "mesmos","na","nas","nao","nas","nem","nesse","neste",
            "nos","o","os","ou","outra","outras","outro","outros",
            "pelas","pelas","pelo","pelos","perante","pois","por",
            "porque","portanto","proprio","proprios","quais","qual",
            "qualquer","quando","quanto","que","quem","quer","se",
            "seja","sem","sendo","seu","seus","sob","sobre","sua",
            "suas","tal","tambem","teu","teus","toda","todas","todo",
            "todos","tua","tuas","tudo","um","uma","umas","uns"
        };

        private readonly TokenStream _innerInputStream;
        private readonly ITermAttribute _termAtt;
        private readonly ITypeAttribute _typeAtt;
        
        private const string AcronymType = "<ACRONYM>";
        private readonly CharArraySet _stopWords = new CharArraySet(BRAZILIAN_STOP_WORDS, false);

        public RavenBrazilianFilter(TokenStream input) : base(input)
        {
            _innerInputStream = input;
            _termAtt = AddAttribute<ITermAttribute>();
            _typeAtt = AddAttribute<ITypeAttribute>();
        }

        public override bool IncrementToken()
        {
            if (!input.IncrementToken())
            {
                return false;
            }

            var buffer = _termAtt.TermBuffer();
            var bufferLength = _termAtt.TermLength();
            var type = _typeAtt.Type;

            var bufferUpdated = true;

            if (type == AcronymType)
            {
                // remove dots
                var upto = 0;
                for (int i = 0; i < bufferLength; i++)
                {
                    var c = buffer[i];
                    if (c != '.')
                        buffer[upto++] = CharUtils.ToLower(c);
                }
                _termAtt.SetTermLength(upto);
            }
            else
            {
                do
                {
                    //If we consumed a stop word we need to update the buffer and its length.
                    if (!bufferUpdated)
                    {
                        bufferLength = _termAtt.TermLength();
                        buffer = _termAtt.TermBuffer();
                    }

                    for (var i = 0; i < bufferLength; i++)
                    {
                        buffer[i] = CharUtils.RemoveAccentMark(CharUtils.ToLower(buffer[i]));
                    }

                    if (!_stopWords.Contains(buffer, 0, bufferLength))
                    {
                        return true;
                    }

                    bufferUpdated = false;
                } while (input.IncrementToken());

                return false;
            }
            return true;
        }

        internal bool Reset(TextReader reader)
        {
            var input = _innerInputStream as StandardTokenizer;

            if (input == null) return false;

            input.Reset(reader);
            return true;
        }
    }
}