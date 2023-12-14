namespace Day14
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Common;
    using Common.Array;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Location[][]>
    {
        public override Location[][] ParseInput(string[] lines) => lines.ToMatrix<Location, LocationType>(pipe => (LocationType)pipe);

        public override string[] Part1()
        {
            var sum = 0;
            foreach (var row in Input)
            {
                foreach (var location in row)
                {
                    location.SlideRockNorth();
                }
            }

            for (int i = 0; i < Input.Length; i++)
            {
                var row = Input[i];
                foreach (var location in row.Where(l => l.Value == LocationType.RoundedRock))
                {
                    sum += (Input.Length - i);
                }
            }

            return new string[]
            {
                sum.ToString()
            };
        }

        public override string[] Part2()
        {
            var runs = 1000000000;
            var sum = 0;
            bool lastRuns = false;
            var originalIteration = new Dictionary<string, int>();
            var valueIteration = new Dictionary<string, int>();
            for (int i = 0; i < runs; i++)
            {
                var key = string.Join(';', Input.SelectMany(row => row.Select(l => $"{l.Value}")));
                if (!lastRuns && valueIteration.ContainsKey(key))
                {
                    lastRuns = true;
                    i = runs - ((runs - i) % valueIteration.Count);
                }
                if (originalIteration.ContainsKey(key))
                {
                    Console.WriteLine($"{i} Looks a lot like {originalIteration[key]}!!!");
                    valueIteration[key] = i - originalIteration[key];
                }
                else
                {
                    originalIteration.Add(key, i);
                }

                foreach (var row in Input)
                {
                    foreach (var location in row)
                    {
                        location.SlideRockNorth();
                    }
                }
                File.WriteAllLines("output.aoc", Input.Select(i => i.Aggregate("", (total, next) => $"{total}{(char)next.Value}")));

                for (int j = 0; j < Input[0].Length; j++)
                {
                    var column = CommonArray.GetColumn(Input, j);
                    foreach (var location in column)
                    {
                        location.SlideRockWest();
                    }
                }
                File.WriteAllLines("output.aoc", Input.Select(i => i.Aggregate("", (total, next) => $"{total}{(char)next.Value}")));

                foreach (var row in Input.Reverse())
                {
                    foreach (var location in row)
                    {
                        location.SlideRockSouth();
                    }
                }
                File.WriteAllLines("output.aoc", Input.Select(i => i.Aggregate("", (total, next) => $"{total}{(char)next.Value}")));

                for (int j = Input[0].Length - 1; j >= 0; j--)
                {
                    var column = CommonArray.GetColumn(Input, j);
                    foreach (var location in column)
                    {
                        location.SlideRockEast();
                    }
                }
                File.WriteAllLines("output.aoc", Input.Select(i => i.Aggregate("", (total, next) => $"{total}{(char)next.Value}")));
            }

            for (int i = 0; i < Input.Length; i++)
            {
                var row = Input[i];
                foreach (var location in row.Where(l => l.Value == LocationType.RoundedRock))
                {
                    sum += (Input.Length - i);
                }
            }

            return new string[]
            {
                sum.ToString()
            };
        }
    }
}
