namespace Day23
{
    using Common.Matrix;

    internal class Tile : Cell<Tile, TileType>
    {
        private static int IdIncrementer;

        private readonly int Id;
        private bool IsCrossRoads;
        private Tile[] WalkableAdjacent = [];
        private readonly Dictionary<Tile, Tile[]> WalkableAdjacentCrossroads = [];

        public bool IsFirst { get; set; }

        public Tile() => Id = ++IdIncrementer;

        public void SetWalkableAdjacent(bool onlyDownhill)
        {
            WalkableAdjacent = GetAdjacent().Where(t => t.Value != TileType.Forest && (t.Value == TileType.Path || (!onlyDownhill || t.IsDownhill(this)))).ToArray();
            IsCrossRoads = Value == TileType.Path && GetAdjacent().All(t => t.Value != TileType.Path);
        }

        public void SetWalkableAdjacentCrossroads()
        {
            if (IsCrossRoads || IsFirst)
            {
                foreach (var adjacent in WalkableAdjacent)
                {
                    var tilesToCrossroad = new List<Tile>();
                    Tile? prev = this;
                    Tile? next = adjacent;
                    while (next != null && !next.IsCrossRoads)
                    {
                        tilesToCrossroad.Add(next);
                        var n = next.Walk().FirstOrDefault(t => t != prev);
                        prev = next;
                        next = n;
                    }
                    if(next != null)
                    {
                        tilesToCrossroad.Add(next);
                    }
                    WalkableAdjacentCrossroads.Add(tilesToCrossroad.Last(), tilesToCrossroad.ToArray());
                }
            }
        }

        public Tile[] Walk() => WalkableAdjacent;

        public Dictionary<Tile, Tile[]> Jump() => WalkableAdjacentCrossroads;

        private bool IsDownhill(Tile from)
        {
            return (from.Right == this && Value == TileType.SlopeRight) ||
                (from.Left == this && Value == TileType.SlopeLeft) ||
                (from.Top == this && Value == TileType.SlopeUp) ||
                (from.Bottom == this && Value == TileType.SlopeDown);
        }

        public override int GetHashCode() => Id;

        public override bool Equals(object? obj)
        {
            if (obj is Tile tile)
            {
                return this.Id.Equals(tile.Id);
            }
            return base.Equals(obj);
        }
    }
}
