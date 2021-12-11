﻿namespace Common.IEnumerable
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

        public static T[][] ToIntMatrix<T>(this IEnumerable<string> source, bool includeDiagonal = false, bool isHorizontalPattern = false, bool isVerticalPattern = false)
            where T : Cell<T, int>, new() => source.Select(line => line.ToIntArray()).ToMatrix<T, int, int>(val => val, includeDiagonal, isHorizontalPattern, isVerticalPattern);

        public static T[][] ToBoolMatrix<T>(this IEnumerable<string> source, char trueChar, bool includeDiagonal = false, bool isHorizontalPattern = false, bool isVerticalPattern = false)
            where T : Cell<T, bool>, new() => source.Select(line => line.ToCharArray()).ToMatrix<T, char, bool>(val => val == trueChar, includeDiagonal, isHorizontalPattern, isVerticalPattern);

        private static T[][] ToMatrix<T, TSource, TValue>(this IEnumerable<IEnumerable<TSource>> source, Func<TSource, TValue> getValue, bool includeDiagonal, bool isHorizontalPattern, bool isVerticalPattern)
            where T : Cell<T, TValue>, new()
        {
            var matrix = source.Select(l => l.Select((val) =>
            {
                var cell = new T();
                cell.SetValue(getValue(val));
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