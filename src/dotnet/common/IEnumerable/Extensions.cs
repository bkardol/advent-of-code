namespace Common.IEnumerable
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Matrix;
    using Common.String;

    public static class Extensions
    {
        public static string[][] GroupByEmptyLine(this IEnumerable<string> source) =>
            source.Aggregate(new List<List<string>> { new List<string>() }, (lines, line) =>
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    lines.Add(new List<string>());
                }
                else
                {
                    lines.Last().Add(line);
                }
                return lines;
            }).Select(linesList => linesList.ToArray()).ToArray();

        public static int[] ToIntArray(this IEnumerable<string> source) =>
            source.Select(n => Convert.ToInt32(n)).ToArray();

        public static long[] ToLongArray(this IEnumerable<string> source) =>
            source.Select(n => Convert.ToInt64(n)).ToArray();

        public static T[][] ToMatrix<T, Y>(this IEnumerable<IEnumerable<T>> source, bool includeDiagonal = false, bool isHorizontalPattern = false, bool isVerticalPattern = false)
            where T : Cell<T, Y>, new() => LinkMatrix<T, Y>(source, includeDiagonal, isHorizontalPattern, isVerticalPattern);

        public static T[][] ToMatrix<T, Y>(this IEnumerable<string> source, Func<char, Y> getValue, bool includeDiagonal = false, bool isHorizontalPattern = false, bool isVerticalPattern = false)
            where T : Cell<T, Y>, new() => source.Select(line => line.Select(c => getValue(c))).ToMatrix<T, Y, Y>(val => val, includeDiagonal, isHorizontalPattern, isVerticalPattern);

        public static T[][] ToIntMatrix<T>(this IEnumerable<string> source, bool includeDiagonal = false, bool isHorizontalPattern = false, bool isVerticalPattern = false)
            where T : Cell<T, int>, new() => source.Select(line => line.ToIntArray()).ToMatrix<T, int, int>(val => val, includeDiagonal, isHorizontalPattern, isVerticalPattern);

        public static T[][] ToIntMatrix<T>(this IEnumerable<string> source, Func<char, int> getValue, bool includeDiagonal = false, bool isHorizontalPattern = false, bool isVerticalPattern = false)
            where T : Cell<T, int>, new() => source.Select(line => line.Select(c => getValue(c))).ToMatrix<T, int, int>(val => val, includeDiagonal, isHorizontalPattern, isVerticalPattern);

        public static T[][] ToBoolMatrix<T>(this IEnumerable<string> source, char trueChar, bool includeDiagonal = false, bool isHorizontalPattern = false, bool isVerticalPattern = false)
            where T : Cell<T, bool>, new() => source.Select(line => line.ToCharArray()).ToMatrix<T, char, bool>(val => val == trueChar, includeDiagonal, isHorizontalPattern, isVerticalPattern);

        public static T[][] ToNullableBoolMatrix<T>(this IEnumerable<string> source, char trueChar, char falseChar, bool includeDiagonal = false, bool isHorizontalPattern = false, bool isVerticalPattern = false)
            where T : Cell<T, bool?>, new() => source.Select(line => line.ToCharArray()).ToMatrix<T, char, bool?>(val => val == trueChar ? true : val == falseChar ? false : null, includeDiagonal, isHorizontalPattern, isVerticalPattern);

        public static T[][] ToMatrix<T, TSource, TValue>(this IEnumerable<IEnumerable<TSource>> source, Func<TSource, TValue> getValue, bool includeDiagonal, bool isHorizontalPattern, bool isVerticalPattern)
            where T : Cell<T, TValue>, new()
        {
            var matrix = source.Select(l => l.Select((val) =>
            {
                var cell = new T();
                cell.SetValue(getValue(val));
                return cell;
            }).ToArray()).ToArray();

            return LinkMatrix<T, TValue>(matrix, includeDiagonal, isHorizontalPattern, isVerticalPattern);
        }

        private static T[][] LinkMatrix<T, TValue>(IEnumerable<IEnumerable<T>> source, bool includeDiagonal, bool isHorizontalPattern, bool isVerticalPattern)
            where T : Cell<T, TValue>, new()
        {
            var matrix = source.Select(s => s.ToArray()).ToArray();
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
                    if (isHorizontalPattern && x == row.Length - 1)
                    {
                        matrix[y][0].SetLeft(row[x]);
                    }
                    if (isVerticalPattern && y == matrix.Length - 1)
                    {
                        matrix[0][x].SetTop(row[x], includeDiagonal);
                    }
                }
            }

            return matrix;
        }

        public static IEnumerable<int> GetNumbersWithCriteria(this IEnumerable<int> source, Func<int, int, bool> checkCriteria)
        {
            int first = 0;
            int second = 0;
            source.GetNumberWithCriteria(0, (i, f) => source.GetNumberWithCriteria(i + 1, (j, s) => checkCriteria(first = f, second = s)));
            return new[] { first, second };
        }

        public static IEnumerable<int> GetNumbersWithCriteria(this IEnumerable<int> source, Func<int, int, int, bool> checkCriteria)
        {
            int first = 0;
            int second = 0;
            int third = 0;
            source.GetNumberWithCriteria(0, (i, f) => source.GetNumberWithCriteria(i + 1, (j, s) => source.GetNumberWithCriteria(j + 1, (k, t) => checkCriteria(first = f, second = s, third = t))));
            return new[] { first, second, third };
        }

        private static bool GetNumberWithCriteria(this IEnumerable<int> source, int startIndex, Func<int, int, bool> checkCriteria)
        {
            for (int i = startIndex; i < source.Count(); ++i)
            {
                int value = source.ElementAt(i);
                if (checkCriteria(i, value))
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<IEnumerable<TSource>> Chunk<TSource>(this IEnumerable<TSource> source, int chunkSize) =>
            source
                .Select((entry, index) => new { entry, index })
                .GroupBy(l => l.index / chunkSize)
                .Select(g => g.Select(r => r.entry));
    }
}
