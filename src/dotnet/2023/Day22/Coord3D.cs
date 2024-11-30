namespace Day22
{
    internal struct Coord3D(int x, int y, int z)
    {
        public int X { get; } = x;
        public int Y { get; } = y;
        public int Z { get; } = z;

        public Coord3D ModifyX(int modifier) => new(X + modifier, Y, Z);
        public Coord3D ModifyY(int modifier) => new(X, Y + modifier, Z);
        public Coord3D ModifyZ(int modifier) => new(X, Y, Z + modifier);

    }
}
