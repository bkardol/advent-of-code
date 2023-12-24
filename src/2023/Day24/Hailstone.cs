namespace Day24
{
    internal class Hailstone(Coord3D position, int velocityX, int velocityY, int velocityZ)
    {
        public Coord3D Start { get; } = position;
        public Coord3D End { get; private set; } = position;

        public int VelocityX { get; } = velocityX;
        public int VelocityY { get; } = velocityY;
        public int VelocityZ { get; } = velocityZ;

        public void Travel(long travel) => 
            End = Start.ModifyXYZ(travel * VelocityX, travel * VelocityY, travel * VelocityZ);

        public (float x, float y)? Intersect2D(Hailstone other)
        {
            float deltaX = End.X - Start.X;
            float deltaY = End.Y - Start.Y;
            float otherDeltaX = other.End.X - other.Start.X;
            float otherDeltaY = other.End.Y - other.Start.Y;

            if (otherDeltaX * deltaY - otherDeltaY * deltaX == 0)
            {
                return null;
            }

            var intersect = (deltaX * (other.Start.Y - Start.Y) + deltaY * (Start.X - other.Start.X)) / (otherDeltaX * deltaY - otherDeltaY * deltaX);
            var otherIntersect = (otherDeltaX * (Start.Y - other.Start.Y) + otherDeltaY * (other.Start.X - Start.X)) / (otherDeltaY * deltaX - otherDeltaX * deltaY);

            if ((intersect >= 0) & (intersect <= 1) & (otherIntersect >= 0) & (otherIntersect <= 1))
            {
                return new (Start.X + otherIntersect * deltaX, Start.Y + otherIntersect * deltaY);
            }
            else
            {
                return null;
            }
        }
    }
}
