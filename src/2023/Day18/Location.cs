namespace Day18
{
    using System.Collections.Generic;
    using System.Linq;
    using Common.Matrix;

    internal class Location : Cell<Location, int>
    {
        public void Dig() => ++Value;

        public IEnumerable<Location> GetAdjacentToFlood() => GetAdjacent().Where(l => l.Value == 0);
    }
}
