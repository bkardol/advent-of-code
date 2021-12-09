using System.Collections.Generic;
using System.Linq;

namespace Day9
{
    internal class Location
    {
        public int Height { get; }

        private Location Left;
        private Location Right;
        private Location Top;
        private Location Bottom;

        public Location(int height) => this.Height = height;

        public void SetLeft(Location left) => Left = left;
        public void SetRight(Location right) => Right = right;
        public void SetTop(Location top) => Top = top;
        public void SetBottom(Location bottom) => Bottom = bottom;

        public IEnumerable<Location> GetAdjacentLocations() => new Location[] { Left, Right, Top, Bottom }.Where(l => l != null);
    }
}
