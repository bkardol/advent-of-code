namespace Day22
{
    using Common.Matrix;

    internal class Cube : Cell<Cube, Coord2D>
    {
        public Cube(Coord2D coord, bool unknown = false)
        {
            this.Value = coord;
            if (unknown)
            {
                this.Brick = new Brick(-1, default, default);
            }
        }

        public Brick? Brick { get; set; }
        public List<Brick> Bricks { get; set; } = new List<Brick>();
    }
}
