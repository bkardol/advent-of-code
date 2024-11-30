namespace Day18
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.MathExtensions;

    internal class Solution : PuzzleSolution<DigInstruction[]>
    {
        public override DigInstruction[] ParseInput(string[] lines) => lines.Select(line =>
        {
            var splitted = line.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if(Part == 1)
            {
                return new DigInstruction((Direction)splitted[0][0], int.Parse(splitted[1]));
            }
            else
            {
                return new DigInstruction(splitted[2]);
            }
        }).ToArray();

        public override string[] Part1() => new string[] { "The lagoon can hold this many cubic meters: " + GetLagoonSurfaceArea() };

        public override string[] Part2() => new string[] {"The lagoon can hold this many cubic meters: " + GetLagoonSurfaceArea() };

        private long GetLagoonSurfaceArea()
        {
            var coordinates = new List<(long X, long Y, long Distance)>();
            (long X, long Y, long Distance) currentCoordinate = (0, 0, 0);
            foreach (var digInstruction in Input)
            {
                currentCoordinate = digInstruction.CompleteInstruction(currentCoordinate.X, currentCoordinate.Y);
                coordinates.Add(currentCoordinate);
            }

            return CommonMath.CalculateSurfaceAreaWithShoelace(coordinates.ToArray());

        }
    }
}
