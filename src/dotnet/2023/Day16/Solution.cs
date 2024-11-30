namespace Day16
{
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Contraption>
    {
        public override Contraption ParseInput(string[] lines) => new Contraption(lines.ToMatrix<Tile, TileType>(pipe => (TileType)pipe));

        public override string[] Part1()
        {
            var energized = Input.GetAmountEnergized(0, 0, Direction.Left);

            return new string[]
            {
                "Amount of tiles energized: " + energized.ToString()
            };
        }

        public override string[] Part2()
        {
            var maximumEnergized = Input.GetMaximumEnergized();

            return new string[]
            {
                "Maximum amount of tiles energized: " + maximumEnergized.ToString()
            };
        }
    }
}
