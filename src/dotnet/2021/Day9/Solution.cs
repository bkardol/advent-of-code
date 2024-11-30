namespace Day9
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Location[][]>
    {
        public override Location[][] ParseInput(string[] lines) => lines.ToIntMatrix<Location>();

        public override string[] Part1()
        {
            int sumOfLowestPoints = GetCaveLowestPoints().Sum(location => location.Value + 1);

            return new string[]
            {
                $"The sum of the lowest point in the cave is: {sumOfLowestPoints}"
            };
        }

        public override string[] Part2()
        {
            int result = GetCaveLowestPoints().Select(location =>
            {
                var basin = new Dictionary<Location, int>();
                GetAdjacentHigherPoints(location, basin);
                return basin.Count;
            }).OrderByDescending(size => size).Take(3).Aggregate(1, (a, b) => a * b);

            return new string[]
            {
                $"The size of the 3 largest basins multiplied is: {result}"
            };
        }

        public IEnumerable<Location> GetCaveLowestPoints() => Input.SelectMany(r => r.Where(location => location.IsLowerThanAdjacent()));

        public void GetAdjacentHigherPoint(Location currentLocation, Location adjacentLocation, Dictionary<Location, int> basin)
        {
            if (adjacentLocation.Value > currentLocation.Value &&
                adjacentLocation.Value != 9 &&
                !basin.ContainsKey(adjacentLocation))
            {
                GetAdjacentHigherPoints(adjacentLocation, basin);
            }
        }

        public void GetAdjacentHigherPoints(Location location, Dictionary<Location, int> basin)
        {
            basin[location] = location.Value;
            foreach (var adjacentLocation in location.GetAdjacent())
            {
                GetAdjacentHigherPoint(location, adjacentLocation, basin);
            }
        }
    }
}
