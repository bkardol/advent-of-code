namespace Day18
{
    using System;
    using System.Collections.Generic;

    internal struct DigInstruction
    {
        private readonly Direction direction;
        private readonly Direction trueDirection;
        private readonly int cubes;
        private readonly int trueCubes;

        public DigInstruction(Direction direction, int cubes, string color)
        {
            this.direction = direction;
            this.trueDirection = GetTrueDirection(color);
            this.cubes = cubes;
            this.trueCubes = GetTrueCubes(color);
        }

        public (long X, long Y, long distance) CompleteInstruction(long currentX, long currentY)
        {
            return trueDirection switch
            {
                Direction.Up => (currentX, currentY - trueCubes, trueCubes),
                Direction.Down => (currentX, currentY + trueCubes, trueCubes),
                Direction.Left => (currentX - trueCubes, currentY, trueCubes),
                Direction.Right => (currentX + trueCubes, currentY, trueCubes),
                _ => throw new ArgumentException("Invalid direction."),
            };
        }

        public (int x, int y) CompleteInstruction(List<List<Location>> digsite, int currentX, int currentY)
        {
            for (int digs = 0; digs < cubes; digs++)
            {
                List<Location> row;
                switch (direction)
                {
                    case Direction.Up:
                        if (currentY > 0)
                        {
                            row = digsite[currentY - 1];
                            row[currentX].Dig();
                            currentY--;
                        }
                        else
                        {
                            row = new List<Location>();
                            for (int i = 0; i < digsite[0].Count; i++)
                            {
                                var location = new Location();
                                if (i == currentX)
                                {
                                    location.Dig();
                                }
                                row.Add(location);
                            }
                            digsite.Insert(currentY, row);
                        }
                        break;
                    case Direction.Down:
                        if (currentY < digsite.Count - 1)
                        {
                            row = digsite[currentY + 1];
                            row[currentX].Dig();
                        }
                        else
                        {
                            row = new List<Location>();
                            for (int i = 0; i < digsite[0].Count; i++)
                            {
                                var location = new Location();
                                if (i == currentX)
                                {
                                    location.Dig();
                                }
                                row.Add(location);
                            }
                            digsite.Add(row);
                        }
                        currentY++;
                        break;
                    case Direction.Left:
                        row = digsite[currentY];
                        if (currentX > 0)
                        {
                            row[currentX - 1].Dig();
                            currentX--;
                        }
                        else
                        {
                            for (int i = 0; i < digsite.Count; i++)
                            {
                                var location = new Location();
                                if (i == currentY)
                                {
                                    location.Dig();
                                }
                                digsite[i].Insert(0, location);
                            }
                        }
                        break;
                    case Direction.Right:
                        row = digsite[currentY];
                        if (currentX < digsite[0].Count - 1)
                        {
                            row[currentX + 1].Dig();
                        }
                        else
                        {
                            for (int i = 0; i < digsite.Count; i++)
                            {
                                var location = new Location();
                                if (i == currentY)
                                {
                                    location.Dig();
                                }
                                digsite[i].Add(location);
                            }
                        }
                        currentX++;
                        break;
                }
            }

            return (currentX, currentY);
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
