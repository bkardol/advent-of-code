using System.Linq;
using Common.Matrix;

namespace Day9
{
    internal class Location : Cell<Location, int>
    {
        public bool IsLowerThanAdjacent() => GetAdjacent().All(adjacent => Value < adjacent.Value);
    }
}
