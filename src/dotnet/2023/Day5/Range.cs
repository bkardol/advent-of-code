namespace Day5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Range
    {
        private readonly long destinationRangeStart;
        private readonly long destinationRangeEnd;
        private readonly long sourceRangeStart;
        private readonly long sourceRangeEnd;
        private readonly long rangeLength;

        public Range(long destinationRangeStart, long sourceRangeStart, long rangeLength)
        {
            this.destinationRangeStart = destinationRangeStart;
            this.destinationRangeEnd = destinationRangeStart + rangeLength;
            this.sourceRangeStart = sourceRangeStart;
            this.sourceRangeEnd = sourceRangeStart + rangeLength;
            this.rangeLength = rangeLength;
        }

        public long GetDestinationNumber(long number)
        {
            if (number >= sourceRangeStart && number < sourceRangeEnd)
            {
                return number - sourceRangeStart + destinationRangeStart;
            }
            return 0;
        }

        //public List<Range> GetRangeMatch(long start, long length)
        //{
        //    var newRanges = new List<Range>();
        //    var end = start + length;
        //    if (start < Math.Min(end, sourceRangeStart))
        //    {
        //        newRanges.Add(new Range(start, start))
        //    }

        //    var matchingStart = Math.Max(start, sourceRangeStart);
        //    var matchingEnd = Math.Min(start + length, sourceRangeEnd);
        //    if ((matchingStart > sourceRangeStart || matchingEnd < sourceRangeEnd) && matchingStart < matchingEnd)
        //    {
        //        return new Range(GetDestinationNumber(matchingStart), matchingStart, matchingEnd - matchingStart);
        //    }
        //    else return null;
        //}
    }
}
