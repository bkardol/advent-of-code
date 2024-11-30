namespace Day6
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Race
    {
        private readonly bool isPartOneRace;
        private readonly long time;
        private readonly long distance;

        public Race(long time, long distance, bool isPartOneRace)
        {
            this.time = time;
            this.distance = distance;
            this.isPartOneRace = isPartOneRace; 
        }

        internal long GetNumberOfWaysToBeatRecord(bool partOne)
        {
            if (partOne != isPartOneRace) return 1;

            long numberOfWaysToBeatRecord = 0;
            for(long i = 1; i < time; i++)
            {
                if(i * (time - i) > distance)
                    numberOfWaysToBeatRecord++;
            }
            Console.WriteLine($"- Race with a time of {time} and distance of {distance} has {numberOfWaysToBeatRecord} number of ways to beat the record");
            return numberOfWaysToBeatRecord;
        }
    }
}
