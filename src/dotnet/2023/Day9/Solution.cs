namespace Day9
{
    using System;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Sequence[]>
    {
        public override Sequence[] ParseInput(string[] lines) => lines.Select(line => new Sequence(line.Split(' ').ToLongArray())).ToArray();

        public override string[] Part1()
        {
            var result = Input.Sum(sequence => sequence.GetExtrapolated());
            return new string[]
            {
                result.ToString()
            };
        }

        public override string[] Part2()
        {
            var result = Input.Sum(sequence => sequence.GetExtrapolatedBackwards());
            return new string[]
            {
                result.ToString()
            };
        }
    }
}
