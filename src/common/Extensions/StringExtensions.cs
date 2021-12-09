namespace Common.Extensions
{
    using System;
    using System.Linq;

    public static class StringExtensions
    {
        public static int[] ToIntArray(this string source) =>
            source.Select(c => int.Parse(c.ToString())).ToArray();
        public static int[] ToIntArray(this string source, char seperator) =>
            source.Split(seperator, StringSplitOptions.RemoveEmptyEntries).ToIntArray();
    }
}
