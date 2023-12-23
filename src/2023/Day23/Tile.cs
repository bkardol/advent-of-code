namespace Day23
{
    using Common.Matrix;

    internal class Tile : Cell<Tile, TileType>
    {
        private static int IdIncrementer;

        private readonly int Id;
        private Tile[] WalkableAdjacent = [];

        public bool Visited { get; set; }
        public bool IsCrossRoads { get; set; }


        public Tile() => Id = ++IdIncrementer;

        public void Prepare(bool onlyDownhill)
        {
            WalkableAdjacent = GetAdjacent().Where(t => t.Value != TileType.Forest && (t.Value == TileType.Path || (!onlyDownhill || t.IsDownhill(this)))).ToArray();
            IsCrossRoads = Value == TileType.Path && GetAdjacent().All(t => t.Value != TileType.Path);
        }

        public Tile[] Walk() => WalkableAdjacent;

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
