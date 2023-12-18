namespace Day18
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Common;
    using Common.IEnumerable;
    using Common.MathExtensions;

    internal class Solution : PuzzleSolution<DigInstruction[]>
    {
        public override DigInstruction[] ParseInput(string[] lines) => lines.Select(line =>
        {
            var splitted = line.Split(new[] { ' ', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            return new DigInstruction((Direction)splitted[0][0], int.Parse(splitted[1]), splitted[2]);
        }).ToArray();

        public override string[] Part1()
        {
            var firstDig = new Location();
            var digsite = new List<List<Location>> { new List<Location> { firstDig } };

            var currentLocation = (0, 0);
            foreach (var digInstruction in Input)
            {
                currentLocation = digInstruction.CompleteInstruction(digsite, currentLocation.Item1, currentLocation.Item2);
            }

            var matrixDigsite = digsite.ToMatrix<Location, int>();
            var flattenedDigsite = digsite.SelectMany(l => l).ToArray();

            var middleLocation = flattenedDigsite[flattenedDigsite.Length / 2];
            Stack<Location> floodLocations = new Stack<Location>();
            floodLocations.Push(middleLocation);
            while (floodLocations.Count > 0)
            {
                var location = floodLocations.Pop();
                location.Dig();
                foreach (var l in location.GetAdjacentToFlood())
                {
                    floodLocations.Push(l);
                }
            }

            File.WriteAllLines("output.aoc", matrixDigsite.Select(i => i.Aggregate("", (total, next) => $"{total}{(next.Value == 0 ? " " : next.Value.ToString())}")));
            return new string[]
            {
                flattenedDigsite.Count(l => l.Value > 0).ToString()
            };
        }

        public override string[] Part2()
        {
            var coordinates = new List<(long X, long Y, long Distance)>();
            (long X, long Y, long Distance) currentCoordinate = (0, 0, 0);
            foreach (var digInstruction in Input)
            {
                currentCoordinate = digInstruction.CompleteInstruction(currentCoordinate.X, currentCoordinate.Y);
                coordinates.Add(currentCoordinate);
            }
            var result = CommonMath.CalculateSurfaceAreaWithShoelace(coordinates.ToArray());
            return new string[]
            {
                result.ToString()
            };
        }
    }
}
