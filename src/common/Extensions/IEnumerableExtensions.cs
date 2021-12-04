namespace Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class IEnumerableExtensions
    {
        public static int[] ToIntArray(this IEnumerable<string> source) =>
            source.Select(n => Convert.ToInt32(n)).ToArray();

        public static IEnumerable<IEnumerable<TSource>> Chunk<TSource>(this IEnumerable<TSource> source, int chunkSize) =>
            source
                .Select((entry, index) => new { entry, index })
                .GroupBy(l => l.index / chunkSize)
                .Select(g => g.Select(r => r.entry));
    }
}
