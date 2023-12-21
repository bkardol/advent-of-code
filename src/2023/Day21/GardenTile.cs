namespace Day21
{
    using System.Linq;
    using Common.Matrix;

    internal class GardenTile : Cell<GardenTile, GardenType>
    {
        public int VisitedStep { get; private set; }
        public (int X, int Y) Coords { get; private set; }

        public GardenTile[] Visit(int step)
        {
            VisitedStep = step;
            return GetAdjacent().Where(tile => tile.VisitedStep == 0 && tile.Value != GardenType.Rock).ToArray();
        }

        internal void SetCoords(int x, int y)
        {
            Coords = (x, y);
        }
    }
}
