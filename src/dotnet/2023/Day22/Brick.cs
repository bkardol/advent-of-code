namespace Day22
{
    using System.Collections.Generic;

    internal class Brick
    {
        public Brick(int id, Coord3D start, Coord3D end)
        {
            this.Id = id;
            this.Start = start;
            this.End = end;
            RecalculateCoords();
        }

        public int Id { get; }
        public Coord3D Start { get; private set; }
        public Coord3D End { get; private set; }
        public Coord3D[] Coords { get; private set; }
        public Dictionary<int, Brick> SupportsBricks { get; } = [];
        public Dictionary<int, Brick> SupportedByBricks { get; } = [];

        public bool CanBeSafelyDesintegrated { get => SupportsBricks.Values.All(b => b.SupportedByBricks.Count > 1); }

        public void UpdateCoords(Coord3D start, Coord3D end)
        {
            Start = start;
            End = end;
            RecalculateCoords();
        }

        private void RecalculateCoords()
        {
            bool xDiff = Start.X != End.X;
            bool yDiff = Start.Y != End.Y;

            var coord = Start;
            var coords = new List<Coord3D> { Start };
            while (!coord.Equals(End))
            {
                coord = xDiff ? coord.ModifyX(1) : yDiff ? coord.ModifyY(1) : coord.ModifyZ(1);
                coords.Add(coord);
            }

            this.Coords = [.. coords];
        }

        internal void Supports(Brick brick)
        {
            SupportsBricks[brick.Id] = brick;
            brick.SupportedByBricks[Id] = this;
        }
    }
}