namespace Common.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static int[] ToIntArray(this string source, char seperator) =>
            source.Split(seperator, StringSplitOptions.RemoveEmptyEntries).ToIntArray();
    }
}
