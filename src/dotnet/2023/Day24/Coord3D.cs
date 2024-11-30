namespace Day24
{
    internal readonly struct Coord3D(long x, long y, long z)
    {
        public long X { get; } = x;
        public long Y { get; } = y;
        public long Z { get; } = z;

        public Coord3D ModifyX(long modifier) => new(X + modifier, Y, Z);
        public Coord3D ModifyY(long modifier) => new(X, Y + modifier, Z);
        public Coord3D ModifyZ(long modifier) => new(X, Y, Z + modifier);
        public Coord3D ModifyXYZ(long mX, long mY, long mZ) => new(X + mX, Y + mY, Z + mZ);
    }
}
