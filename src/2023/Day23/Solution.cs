using Common;
using Common.IEnumerable;
using Day23;

internal class Solution : PuzzleSolution<Map>
{
    public override Map ParseInput(string[] lines) =>
        new(lines.ToMatrix<Tile, TileType>(c => (TileType)c), Part == 1);


    public override string[] Part1() => [Input.GetTouristicRoute().ToString()];

    public override string[] Part2() => [Input.GetTouristicRoute().ToString()];
}

