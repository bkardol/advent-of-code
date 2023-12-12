namespace Day12
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Row[]>
    {
        public override Row[] ParseInput(string[] lines) => lines.Select(line =>
        {
            var splitted = line.Split(' ');
            return new Row(splitted[0], splitted[1].Split(',').ToIntArray());
        }).ToArray();

        public override string[] Part1()
        {
            var results = Input.Select(i => i.GetNumberOfArrangements());
            return new string[]
            {
                results.Sum().ToString()
            };
        }

        public override string[] Part2()
        {
            foreach (var input in Input)
            {
                input.Unfold();
            }

            var results = Input.Select(i => i.GetNumberOfArrangements()).ToArray();
            return new string[]
            {
                results.Sum().ToString()
            };
        }
    }
}
