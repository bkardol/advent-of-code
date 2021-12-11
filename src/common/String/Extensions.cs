namespace Common.String
{
    using System;
    using System.Linq;
    using Common.IEnumerable;

    public static class Extensions
    {
        public static int[] ToIntArray(this string source) =>
            source.Select(c => int.Parse(c.ToString())).ToArray();
        public static int[] ToIntArray(this string source, char seperator) =>
            source.Split(seperator, StringSplitOptions.RemoveEmptyEntries).ToIntArray();
    }
}
