using System;
using Common;
using Common.IEnumerable;
using Common.MathExtensions;
using Day21;

internal class Solution : PuzzleSolution<GardenTile[][]>
{
    public override GardenTile[][] ParseInput(string[] lines)
    {
        var matrix = lines.ToMatrix<GardenTile, GardenType>(c => (GardenType)c, false, Part == 2, Part == 2);
        for (int i = 0; i < matrix.Length; i++)
        {
            var row = matrix[i];
            for (int j = 0; j < matrix[i].Length; j++)
            {
                row[j].SetCoords(j, i);
            }
        }
        return matrix;
    }


    public override string[] Part1()
    {
        var flatGarden = Input.SelectMany(g => g).ToDictionary(g => g.Coords, g => g);
        var start = flatGarden.Values.First(g => g.Value == GardenType.Start);
        var set = new HashSet<(int x, int y)>
        {
            start.Coords
        };

        int iterations = 64;
#if DEBUG
        iterations = 6;
#endif

        for (int i = 0; i <= iterations; i++)
        {
            var newSet = new HashSet<(int x, int y)>();
            foreach (var coords in set)
            {
                var tile = flatGarden[coords];
                foreach (var adjacent in tile.Visit(i))
                {
                    newSet.Add(adjacent.Coords);
                }
            }
            set = newSet;
        }

        var result = flatGarden.Values.Count(tile => tile.VisitedStep > 0 && tile.VisitedStep % 2 == 0).ToString();
        return [result];
    }

    public override string[] Part2()
    {
        // Don't run this thing in DEBUG; it only works with the real input where the pattern is clearly visible.
#if DEBUG
        return ["EXAMPLE NOT SUPPORTED"];
#endif
        var flatGarden = Input.SelectMany(g => g).ToDictionary(g => (g.Coords.X, g.Coords.Y, 0), g => g);
        var start = flatGarden.Values.First(g => g.Value == GardenType.Start);
        var set = new HashSet<(int x, int y, int mapX, int mapY)>
        {
            (start.Coords.X, start.Coords.Y, 0, 0)
        };

        var size = Input.Length;
        var mapCount = 26501365 / size;
        var patternSize = 26501365 % size;
        int iterations = patternSize + (2 * Input.Length) + 1;

        var gridSize = Input.Length;
        List<int> tileCount = [];
        for (int i = 0; i <= iterations; i++)
        {
            var newSet = new HashSet<(int x, int y, int mapX, int mapY)>();
            foreach (var (x, y, mapX, mapY) in set)
            {
                var tile = flatGarden[(x, y, 0)];
                foreach (var newCoords in tile.Visit(i, mapX, mapY))
                {
                    newSet.Add(newCoords);
                }
            }
            set = newSet;

            var uneven = (i % 2);
            var tiles = flatGarden.Values.Sum(tile => tile.VisitedSteps.Values.Count(v => v > 0 && v % 2 == uneven));
            if ((i + 1 + patternSize) % size == 0)
            {
                var tilesIncludingStart = tiles + (uneven == 0 ? 1 : 0);
                tileCount.Add(tilesIncludingStart);
            }
        }

        var (a, b, c) = CommonMath.CalculateQuadraticFit(tileCount[0], tileCount[1], tileCount[2]);
        var result = CommonMath.QuadraticEquation(mapCount, a, b, c);
        return [result.ToString()];
    }
}