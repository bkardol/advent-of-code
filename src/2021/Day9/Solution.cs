namespace Day9
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.Extensions;

    internal class Solution : PuzzleSolution<Location[][]>
    {
        public override Location[][] ParseInput(string[] lines)
        {

            var cave = lines.Select(l => l.ToIntArray().Select((height) => new Location(height)).ToArray()).ToArray();

            for (int y = 0; y < cave.Length; ++y)
            {
                var row = cave[y];
                for (int x = 0; x < row.Length; ++x)
                {
                    if(x > 0)
                    {
                        cave[y][x].SetLeft(row[x - 1]);
                    }
                    if (x < row.Length - 1)
                    {
                        cave[y][x].SetRight(row[x + 1]);
                    }
                    if (y > 0)
                    {
                        cave[y][x].SetTop(cave[y - 1][x]);
                    }
                    if (y < cave.Length - 1)
                    {
                        cave[y][x].SetBottom(cave[y + 1][x]);
                    }
                }
            }

            return cave;
        }

        public override string[] Part1()
        {
            int sumOfLowestPoints = GetCaveLowestPoints().Sum(location => location.Height + 1);

            return new string[]
            {
                $"The sum of the lowest point in the cave is: {sumOfLowestPoints}"
            };
        }

        public override string[] Part2()
        {
            int result = GetCaveLowestPoints().Select(location =>
            {
                var basin = new Dictionary<Location, int> { [location] = location.Height };
                GetAdjacentHigherPoints(location, basin);
                return basin.Count;
            }).OrderByDescending(size => size).Take(3).Aggregate(1, (a, b) => a * b);

            return new string[]
            {
                $"The size of the 3 largest basins multiplied is: {result}"
            };
        }

        public IEnumerable<Location> GetCaveLowestPoints() => Input.SelectMany(r => r.Where(location => location.GetAdjacentLocations().All(adjacent => location.Height < adjacent.Height)));

        public void GetAdjacentHigherPoint(Location currentLocation, Location adjacentLocation, Dictionary<Location, int> basin)
        {
            if (adjacentLocation.Height > currentLocation.Height &&
                adjacentLocation.Height != 9 &&
                !basin.ContainsKey(adjacentLocation))
            {
                GetAdjacentHigherPoints(adjacentLocation, basin);
            }
        }

        public void GetAdjacentHigherPoints(Location location, Dictionary<Location, int> basin)
        {
            basin[location] = location.Height;
            foreach (var adjacentLocation in location.GetAdjacentLocations())
            {
                GetAdjacentHigherPoint(location, adjacentLocation, basin);
            }
        }
    }
}
