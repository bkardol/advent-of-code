namespace Day21
{
    using System.Linq;
    using Common.Matrix;

    internal class GardenTile : Cell<GardenTile, GardenType>
    {
        public int VisitedStep { get; private set; }
        public Dictionary<(int, int), int> VisitedSteps { get; } = [];
        public (int X, int Y) Coords { get; private set; }

        public GardenTile[] Visit(int step)
        {
            VisitedStep = step;
            return GetAdjacent().Where(tile => tile.VisitedStep == 0 && tile.Value != GardenType.Rock).ToArray();
        }
        public (int X, int Y, int MapX, int MapY)[] Visit(int step, int mapX, int mapY)
        {
            VisitedSteps[(mapX, mapY)] = step;
            return GetAdjacent()
                .Select(tile =>
                {
                    if (tile.Value == GardenType.Rock)
                    {
                        return (0, 0, 0, 0, false);
                    }

                    int mx = mapX;
                    int my = mapY;
                    if (Math.Abs(Coords.X - tile.Coords.X) > 1)
                    {
                        mx += (Coords.X > tile.Coords.X ? 1 : -1);
                    }
                    else if (Math.Abs(Coords.Y - tile.Coords.Y) > 1)
                    {
                        my += (Coords.Y > tile.Coords.Y ? 1 : -1);
                    }

                    if (!tile.VisitedSteps.ContainsKey((mx, my)))
                    {
                        return (tile.Coords.X, tile.Coords.Y, mx, my, true);
                    }

                    return (0, 0, 0, 0, false);
                })
                .Where(coords => coords.Item5)
                .Select(coords => (coords.Item1, coords.Item2, coords.Item3, coords.Item4)).ToArray();
        }

        internal void SetCoords(int x, int y) => Coords = (x, y);
    }
}
