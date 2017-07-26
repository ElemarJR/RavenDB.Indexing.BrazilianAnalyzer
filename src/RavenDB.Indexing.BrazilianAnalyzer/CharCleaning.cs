using System.Globalization;

namespace RavenDB.Indexing.BrazilianAnalyzer
{
    public static class CharUtils
    {
        public static char RemoveAccentMark(char c)
        {
            if ((c == '�') ||
                (c == '�') ||
                (c == '�') ||
                (c == '�') ||
                (c == '�'))
            {
                return 'a';
            }
            if ((c == '�') ||
                (c == '�'))
            {
                return 'e';
            }
            if (c == '�')
            {
                return 'i';
            }
            if ((c == '�') ||
                (c == '�') ||
                (c == '�') ||
                (c == '�'))
            {
                return 'o';
            }
            if ((c == '�') ||
                (c == '�'))
            {
                return 'u';
            }
            if (c == '�')
            {
                return 'c';
            }
            if (c == '�')
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