using System.Globalization;

namespace RavenDB.Indexing.BrazilianAnalyzer
{
    public static class CharUtils
    {
        public static char RemoveAccentMark(char c)
        {
            if ((c == 'á') ||
                (c == 'à') ||
                (c == 'ä') ||
                (c == 'â') ||
                (c == 'ã'))
            {
                return 'a';
            }
            if ((c == 'é') ||
                (c == 'ê'))
            {
                return 'e';
            }
            if (c == 'í')
            {
                return 'i';
            }
            if ((c == 'ó') ||
                (c == 'ô') ||
                (c == 'ö') ||
                (c == 'õ'))
            {
                return 'o';
            }
            if ((c == 'ú') ||
                (c == 'ü'))
            {
                return 'u';
            }
            if (c == 'ç')
            {
                return 'c';
            }
            if (c == 'ñ')
            {
                return 'n';
            }

            return c;
        }

        public static char ToLower(char c)
        {
            int cInt = c;

            if (c < 128 && isAsciiCasingSameAsInvariant)
            {
                if (65 <= cInt && cInt <= 90)
                    c |= ' ';

                return c;
            }
            return invariantTextInfo.ToLower(c);

        }

        private static readonly bool isAsciiCasingSameAsInvariant = CultureInfo.InvariantCulture.CompareInfo.Compare("abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CompareOptions.IgnoreCase) == 0;
        private static readonly TextInfo invariantTextInfo = CultureInfo.InvariantCulture.TextInfo;
    }
}