namespace Day6
{
    using System;
    using System.Linq;
    using Common;
    using Common.Extensions;

    internal class Solution : PuzzleSolution<int[]>
    {
        public override int[] ParseInput(string[] lines) => lines.ElementAt(0).ToIntArray(',');

        public override string[] Part1() => GetLanternFishesAfterDays(80);

        public override string[] Part2() => GetLanternFishesAfterDays(256);

        private string[] GetLanternFishesAfterDays(int days)
        {
            var fishes = new long[9];
            foreach (var fishGroup in Input.GroupBy(f => f))
            {
                fishes[fishGroup.Key] = fishGroup.Count();
            }

            for (int i = 0; i < days; ++i)
            {
                long givingBirth = fishes[0];
                Array.Copy(fishes, 1, fishes, 0, fishes.Length - 1);
                fishes[6] += givingBirth;
                fishes[8] = givingBirth;
            }

            return new string[]
            {
                $"There are {fishes.Sum(f => f)} lanternfishes after {days} days"
            };
        }
    }
}
