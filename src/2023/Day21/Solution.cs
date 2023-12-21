using Common;
using Common.IEnumerable;
using Day21;

internal class Solution : PuzzleSolution<GardenTile[][]>
{
    public override GardenTile[][] ParseInput(string[] lines)
    {
        var matrix = lines.ToMatrix<GardenTile, GardenType>(c => (GardenType)c);
        for(int i = 0; i < matrix.Length; i++)
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
            foreach(var coords in set)
            {
                var tile = flatGarden[coords];
                foreach (var adjacent in tile.Visit(i))
                {
                    newSet.Add(adjacent.Coords);
                }
            }
            set = newSet;
            Console.WriteLine(flatGarden.Values.Count(tile => tile.VisitedStep > 0 && tile.VisitedStep % 2 == 0));
        }

        var result = flatGarden.Values.Count(tile => tile.VisitedStep > 0 && tile.VisitedStep % 2 == 0).ToString();
        return [result];
    }

    public override string[] Part2()
    {
        return [];
    }
}