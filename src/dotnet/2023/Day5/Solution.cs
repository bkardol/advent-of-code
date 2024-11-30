namespace Day5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<Almanac>
    {
        public override Almanac ParseInput(string[] lines)
        {
            long[] seeds = null;
            var maps = new List<Map>();
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (seeds == null)
                {
                    seeds = line.Split(" ").Skip(1).Select(value => long.Parse(value)).ToArray();
                    ++i;
                }
                else
                {
                    var nameSplit = line.Split(new[] { '-', ' ' });
                    var from = nameSplit[0];
                    var to = nameSplit[2];
                    var ranges = new List<Range>();

                    ++i;
                    while (i < lines.Length && lines[i] != "")
                    {
                        var rangeNumbers = lines[i].Split(" ").Select(value => long.Parse(value)).ToArray();
                        ranges.Add(new Range(rangeNumbers[0], rangeNumbers[1], rangeNumbers[2]));
                        ++i;
                    }
                    maps.Add(new Map(from, to, ranges.ToArray()));
                }
            }
            return new Almanac(seeds, maps.ToArray());
        }

        public override string[] Part1()
        {
            var input = Input.GetLowestDestinationForSeeds();
            return new string[]
            {
                input.ToString()
            };
        }

        public override string[] Part2()
        {
            var input = Input.GetLowestDestinationForSeedRange().Result;
            return new string[]
            {
                input.ToString()
            };
        }
    }
}
