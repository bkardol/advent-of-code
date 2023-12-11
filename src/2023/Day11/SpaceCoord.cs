namespace Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class SpaceCoord
    {
        public int GalaxyId { get; }
        public bool IsGalaxy { get; }
        public int X { get; }
        public int Y { get; }

        public SpaceCoord(int galaxyId, int x, int y)
        {
            this.GalaxyId = galaxyId;
            this.IsGalaxy = galaxyId != 0;
            this.X = x;
            this.Y = y;
        }
    }
}
