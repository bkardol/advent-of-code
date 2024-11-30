using Common;
using Day22;

internal class Solution : PuzzleSolution<Brick[]>
{
    private static readonly char[] coordsSeperators = [',', '~'];

    public override Brick[] ParseInput(string[] lines)
    {
        return lines.Select((line, i) =>
        {
            var coords = line.Split(coordsSeperators).Select(i => int.Parse(i)).ToArray();
            var start = new Coord3D(coords[0], coords[1], coords[2]);
            var end = new Coord3D(coords[3], coords[4], coords[5]);
            return new Brick(i + 1, start, end);
        }).ToArray();
    }


    public override string[] Part1()
    {
        PlaySomeTetris();
        var result = Input.Count(brick => brick.CanBeSafelyDesintegrated);
        return ["Amount of blocks that can be desintegrated: " + result];
    }

    public override string[] Part2()
    {
        PlaySomeTetris();

        int totalFallingBricks = 0;
        foreach (var brick in Input)
        {
            var removedBricks = new List<Brick> { brick };
            bool moreTetris = true;
            while (moreTetris)
            {
                moreTetris = false;
                for(int i = 0; i < removedBricks.Count; i++)
                {
                    var removedBrick = removedBricks[i];
                    foreach (var fallingBrick in removedBrick.SupportsBricks.Values.Where(b1 => !b1.SupportedByBricks.Values.Any(b2 => !removedBricks.Contains(b2))))
                    {
                        if (!removedBricks.Contains(fallingBrick))
                        {
                            moreTetris = true;
                            removedBricks.Add(fallingBrick);
                        }
                    }
                }
            }
            totalFallingBricks += (removedBricks.Count - 1);
        }

        return ["Sum of blocks falling: " + totalFallingBricks];
    }

    private void PlaySomeTetris()
    {
        var flatCoords = new HashSet<Coord3D>(Input.SelectMany(brick => brick.Coords));

        bool tetris = true;
        while (tetris)
        {
            tetris = false;
            foreach (var brick in Input.Where(brick =>
                                        !brick.Coords.Any(coord =>
                                            coord.Z <= 1 ||
                                            (flatCoords.Contains(coord.ModifyZ(-1)) && !brick.Coords.Contains(coord.ModifyZ(-1)))
            )))
            {
                brick.UpdateCoords(brick.Start.ModifyZ(-1), brick.End.ModifyZ(-1));
                tetris = true;
            }
            flatCoords = new HashSet<Coord3D>(Input.SelectMany(brick => brick.Coords));
        }

        foreach (var brick1 in Input)
        {
            foreach (var brick2 in brick1.Coords
                .Select(coord => coord.ModifyZ(1))
                .SelectMany(coord => Input.Where(brick2 => brick2 != brick1 && brick2.Coords.Contains(coord))))
            {
                brick1.Supports(brick2);
            }
        }
    }
}