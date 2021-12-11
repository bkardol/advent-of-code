namespace Common.IEnumerable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Matrix;
    using Common.String;

    public static class Extensions
    {
        public static int[] ToIntArray(this IEnumerable<string> source) =>
            source.Select(n => Convert.ToInt32(n)).ToArray();

        public static T[][] ToIntMatrix<T>(this IEnumerable<string> source, bool includeDiagonal = false)
            where T : Cell<T, int>, new()
        {
            T[][] matrix = source.Select(l => l.ToIntArray().Select((val) =>
            {
                var cell = new T();
                cell.SetValue(val);
                return cell;
            }).ToArray()).ToArray();

            for (int y = 0; y < matrix.Length; ++y)
            {
                var row = matrix[y];
                for (int x = 0; x < row.Length; ++x)
                {
                    if (x > 0)
                    {
                        matrix[y][x].SetLeft(row[x - 1]);
                    }
                    if (y > 0)
                    {
                        matrix[y][x].SetTop(matrix[y - 1][x], includeDiagonal);
                    }
                }
            }

            return matrix;
        }

        public static IEnumerable<IEnumerable<TSource>> Chunk<TSource>(this IEnumerable<TSource> source, int chunkSize) =>
            source
                .Select((entry, index) => new { entry, index })
                .GroupBy(l => l.index / chunkSize)
                .Select(g => g.Select(r => r.entry));
    }
}
