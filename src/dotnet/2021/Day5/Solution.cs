namespace Day5
{
    using System.Linq;
    using Common;
    using Common.IEnumerable;
    using Common.String;

    internal class Solution : PuzzleSolution<Line[]>
    {
        private static double[][] NewMap => new double[][] { new double[] { 0 } };

        public override Line[] ParseInput(string[] lines) => lines
            .Select(line => line
                .Split(" -> ")
                .Select(coords => coords.ToIntArray(','))
                .Select(coords => new Coordinates(coords[0], coords[1]))
            )
            .Select(line => new Line(line.ElementAt(0), line.ElementAt(1)))
            .ToArray();

        public override string[] Part1()
        {
            var map = NewMap;
            foreach(var line in Input)
            {
                line.PlotOnMap(ref map, false);
            }

            var crossingLines = map.Sum(y => y.Count(x => x > 1));

            return new string[]
            {
                $"Number of crossing horizontal and vertical lines: {crossingLines}"
            };
        }

        public override string[] Part2()
        {
            var map = NewMap;
            foreach (var line in Input)
            {
                line.PlotOnMap(ref map, true);
            }

            var crossingLines = map.Sum(y => y.Count(x => x > 1));

            return new string[]
            {
                $"Number of crossing horizontal, vertical and diagonal lines: {crossingLines}"
            };
        }
    }
}
