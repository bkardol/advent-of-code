namespace Day16
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Contraption
    {
        private readonly Tile[][] tiles;

        public Contraption(Tile[][] tiles)
        {
            this.tiles = tiles;
        }

        public int GetAmountEnergized(int row, int column, Direction sourceDirection)
        {
            tiles[row][column].ProcessBeam(sourceDirection);
            return GetEnergized();
        }

        public int GetMaximumEnergized()
        {
            Dictionary<(int row, int col), int> cache = new();

            var flattenedInput = tiles.SelectMany(tiles => tiles).ToArray();
            for (int row = 0; row < tiles.Length; row++)
            {
                ProcessBeam(row, 0, Direction.Left, cache);
                ProcessBeam(row, tiles[row].Length - 1, Direction.Right, cache);
            }
            for (int col = 0; col < tiles[0].Length; col++)
            {
                ProcessBeam(0, col, Direction.Top, cache);
                ProcessBeam(tiles.Length - 1, col, Direction.Bottom, cache);
            }

            return cache.Values.Max();
        }

        private void ProcessBeam(int row, int column, Direction sourceDirection, Dictionary<(int row, int col), int> cache)
        {
            var cacheKey = (row, column);
            if (!cache.ContainsKey(cacheKey))
            {
                tiles[row][column].ProcessBeam(sourceDirection);
                cache.Add(cacheKey, GetEnergized());
                ClearInput();
            }
        }

        private int GetEnergized() => tiles.Sum(tiles => tiles.Sum(tile => tile.Energized.Count > 0 ? 1 : 0));

        private void ClearInput()
        {
            foreach (var tile in tiles.SelectMany(tiles => tiles))
            {
                tile.Clear();
            }
        }
    }
}
