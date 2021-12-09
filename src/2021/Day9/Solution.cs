namespace Day9
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.Extensions;

    internal record Location(int I, int J);

    internal class Solution : PuzzleSolution<int[][]>
    {
        public override int[][] ParseInput(string[] lines) => lines.Select(l => l.ToIntArray()).ToArray();

        public override string[] Part1()
        {
            int sumOfLowestPoints = GetLowestPointForBasin().Sum(point => Input[point.I][point.J] + 1);

            return new string[]
            {
                $"{sumOfLowestPoints}"
            };
        }

        public override string[] Part2()
        {
            int result = GetLowestPointForBasin().Select(point =>
            {
                var current = Input[point.I][point.J];
                var basin = new Dictionary<Location, int> { [point] = current };
                GetAdjacentHigherPoints(point.I, point.J, current, basin);
                return basin.Count;
            }).OrderByDescending(size => size).Take(3).Aggregate(1, (a, b) => a * b);

            return new string[]
            {
                $"{result}"
            };
        }

        public List<Location> GetLowestPointForBasin()
        {
            var locations = new List<Location>();
            for (int i = 0; i < Input.Length; ++i)
            {
                for (int j = 0; j < Input[i].Length; ++j)
                {
                    var current = Input[i][j];
                    var left = j > 0 ? Input[i][j - 1] : int.MaxValue;
                    var right = j < Input[i].Length - 1 ? Input[i][j + 1] : int.MaxValue;
                    var top = i > 0 ? Input[i - 1][j] : int.MaxValue;
                    var bottom = i < Input.Length - 1 ? Input[i + 1][j] : int.MaxValue;
                    if (current < left && current < right && current < top && current < bottom)
                    {
                        locations.Add(new Location(i, j));
                    }
                }
            }
            return locations;
        }

        public void GetAdjacentHigherPoints(int i, int j, int current, Dictionary<Location, int> basin)
        {
            var left = j > 0 ? Input[i][j - 1] : int.MinValue;
            var right = j < Input[i].Length - 1 ? Input[i][j + 1] : int.MinValue;
            var top = i > 0 ? Input[i - 1][j] : int.MinValue;
            var bottom = i < Input.Length - 1 ? Input[i + 1][j] : int.MinValue;
            if (left > current && left != 9 && !basin.ContainsKey(new Location(i, j - 1)))
            {
                basin[new Location(i, j - 1)] = left;
                GetAdjacentHigherPoints(i, j - 1, left, basin);
            }
            if (right > current && right != 9 && !basin.ContainsKey(new Location(i, j + 1)))
            {
                basin[new Location(i, j + 1)] = right;
                GetAdjacentHigherPoints(i, j + 1, right, basin);
            }
            if (top > current && top != 9 && !basin.ContainsKey(new Location(i - 1, j)))
            {
                basin[new Location(i - 1, j)] = top;
                GetAdjacentHigherPoints(i - 1, j, top, basin);
            }
            if (bottom > current && bottom != 9 && !basin.ContainsKey(new Location(i + 1, j)))
            {
                basin[new Location(i + 1, j)] = bottom;
                GetAdjacentHigherPoints(i + 1, j, bottom, basin);
            }
        }
    }
}
