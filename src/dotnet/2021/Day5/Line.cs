using System;
using System.Collections.Generic;
using System.Linq;

namespace Day5
{
    internal record Coordinates(int X, int Y);

    internal class Line
    {
        private readonly Coordinates start;
        private readonly Coordinates end;

        private readonly int minX;
        private readonly int minY;
        private readonly int maxX;
        private readonly int maxY;

        private bool IsHorizontal => minX == maxX;
        private bool IsVertical => minY == maxY;
        private bool IsDiagonal => maxX - minX == maxY - minY;

        private IEnumerable<Coordinates> containedCoordinates;

        public Line(Coordinates start, Coordinates end)
        {
            this.start = start;
            this.end = end;

            minX = Math.Min(start.X, end.X);
            minY = Math.Min(start.Y, end.Y);
            maxX = Math.Max(start.X, end.X);
            maxY = Math.Max(start.Y, end.Y);

            InitContainedCoordinates();
        }

        public void PlotOnMap(ref double[][] map, bool includeDiagonal)
        {
            if (!includeDiagonal && IsDiagonal)
            {
                return;
            }

            ResizeMap(ref map);
            foreach (var coord in containedCoordinates)
            {
                ++map[coord.Y][coord.X];
            }
        }

        private void InitContainedCoordinates()
        {
            if (IsHorizontal)
            {
                containedCoordinates = Enumerable.Range(minY, maxY - minY + 1).Select(y => new Coordinates(minX, y)).ToArray();
            }
            else if (IsVertical)
            {
                containedCoordinates = Enumerable.Range(minX, maxX - minX + 1).Select(x => new Coordinates(x, minY)).ToArray();
            }
            else if (IsDiagonal)
            {
                var coords = new List<Coordinates>();
                var first = minY == start.Y ? start : end;
                var last = minY == start.Y ? end : start;
                for (int y = first.Y, i = 0; y < last.Y + 1; ++y, ++i)
                {
                    int x = first.X == minX ? first.X + i : first.X - i;
                    coords.Add(new Coordinates(x, y));
                }
                containedCoordinates = coords;
            }
        }

        private void ResizeMap(ref double[][] map)
        {
            int sizeY = map.Length;
            if (map.Length <= maxY)
            {
                Array.Resize(ref map, maxY + 1);
                for (int y = sizeY; y < map.Length; y++)
                {
                    if (map[y] == null)
                    {
                        map[y] = new double[maxX + 1];
                    }
                }
            }
            for (int y = 0; y < map.Length; y++)
            {
                if (map[y].Length <= maxX)
                {
                    Array.Resize(ref map[y], maxX + 1);
                }
            }
        }
    }
}
