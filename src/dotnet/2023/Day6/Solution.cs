namespace Day6
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Race[]>
    {
        public override Race[] ParseInput(string[] lines)
        {
            var times = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1);
            var distance = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1);

            var races = times.ToIntArray().Select((time, i) => new Race(time, distance.ToIntArray()[i], true)).ToList();
            races.Add(new Race(long.Parse(Regex.Replace(lines[0], @"[^0-9]", "")), long.Parse(Regex.Replace(lines[1], @"[^0-9]", "")), false));

            return races.ToArray();
        }

        public override string[] Part1() => getNumberOfWaysToWinMultiplied(true);

        public override string[] Part2() => getNumberOfWaysToWinMultiplied(false);

        private string[] getNumberOfWaysToWinMultiplied(bool partOne)
        {
            long waysToWinMultiplied = this.Input
                .Select(race => race.GetNumberOfWaysToBeatRecord(partOne))
                .Aggregate((long)1, (multiplied, next) => multiplied * next);
            return new string[]
            {
                $"This means that the answer is: {waysToWinMultiplied}"
            };
        }
    }
}
