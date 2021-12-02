namespace Day1
{
    using System;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<int>
    {
        public override int[] ParseInput(string[] lines) => lines.Select(line => Convert.ToInt32(line)).ToArray();

        public override string[] Part1()
        {
            var increasedDepths = GetIncreasedDepths(Input);
            return new string[] { $"There are {increasedDepths} measurements that are larger than the previous measurement." };
        }

        public override string[] Part2()
        {
            var increasedWindowedDepths = GetIncreasedDepths(Input.Skip(2).Select((depth, i) => depth + Input[i] + Input[i + 1]).ToArray());
            return new string[] { $"There are {increasedWindowedDepths} sums that are larger than the previous sum." };
        }

        static int GetIncreasedDepths(int[] depths) => depths.Where((depth, i) => i > 0 && depth > depths[i - 1]).Count();
    }
}
