namespace Day5
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Map
    {
        private readonly string from;
        private readonly string to;
        private Range[] ranges;

        public Map(string from, string to, Range[] ranges)
        {
            this.from = from;
            this.to = to;
            this.ranges = ranges;
        }

        public long GetLowestLocationNumber(long seedNumber)
        {
            foreach (var range in ranges)
            {
                var match = range.GetDestinationNumber(seedNumber);
                if (match != 0)
                {
                    return match;
                }
            }
            return seedNumber;
        }
    }
}
