using Common;
using Common.IEnumerable;
using Day24;

internal class Solution : PuzzleSolution<Hailstone[]>
{
    private static readonly string[] seperators = [", ", " @ "];

    public override Hailstone[] ParseInput(string[] lines) => lines.Select(line =>
    {
        var splitted = line.Split(seperators, StringSplitOptions.RemoveEmptyEntries).ToLongArray();
        return new Hailstone(new Coord3D(splitted[0], splitted[1], splitted[2]), Convert.ToInt32(splitted[3]), Convert.ToInt32(splitted[4]), Convert.ToInt32(splitted[5]));
    }).ToArray();


    public override string[] Part1()
    {
        long minPosition = 200000000000000;
        long maxPosition = 400000000000000;
#if DEBUG
        minPosition = 7;
        maxPosition = 27;
#endif
        foreach (var hailstone in Input)
        {
            hailstone.Travel(maxPosition);
        }

        var intersections = new List<(float x, float y)>();
        for (int i = 0; i < Input.Length; i++)
        {
            var hailstone1 = Input[i];
            for (int j = i + 1; j < Input.Length; j++)
            {
                var hailstone2 = Input[j];
                var result = hailstone1.Intersect2D(hailstone2);
                if (result is (float x, float y) && x >= minPosition && x <= maxPosition && y >= minPosition && y <= maxPosition)
                {
                    intersections.Add((x, y));
                }
            }
        }
        return [intersections.Count.ToString()];
    }

    public override string[] Part2()
    {
        return [];
    }
}

