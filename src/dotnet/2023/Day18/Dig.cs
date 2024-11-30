namespace Day18
{
    using System;
    using System.Collections.Generic;

    internal readonly struct DigInstruction
    {
        private readonly Direction direction;
        private readonly int cubes;

        public DigInstruction(Direction direction, int cubes)
        {
            this.direction = direction;
            this.cubes = cubes;
        }

        public DigInstruction(string color)
        {
            this.direction = GetTrueDirection(color);
            this.cubes = GetTrueCubes(color);
        }

        public (long X, long Y, long distance) CompleteInstruction(long currentX, long currentY)
        {
            return direction switch
            {
                Direction.Up => (currentX, currentY - cubes, cubes),
                Direction.Down => (currentX, currentY + cubes, cubes),
                Direction.Left => (currentX - cubes, currentY, cubes),
                Direction.Right => (currentX + cubes, currentY, cubes),
                _ => throw new ArgumentException("Invalid direction."),
            };
        }

        private static Direction GetTrueDirection(string color)
        {
            return color[^1] switch
            {
                '0' => Direction.Right,
                '1' => Direction.Down,
                '2' => Direction.Left,
                '3' => Direction.Up,
                _ => throw new ArgumentException("Expected valid digits for true direction.", nameof(color)),
            };
        }

        private static int GetTrueCubes(string color) => int.Parse(color[1..^1], System.Globalization.NumberStyles.HexNumber);

    }
}
