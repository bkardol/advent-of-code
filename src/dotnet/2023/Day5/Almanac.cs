namespace Day5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Almanac
    {
        private readonly long[] seeds;
        private readonly Map[] maps;

        public Almanac(long[] seeds, Map[] maps)
        {
            this.seeds = seeds;
            this.maps = maps;
        }

        public long GetLowestDestinationForSeeds()
        {
            var results = seeds.Select(seed => maps.Aggregate(seed, (prev, next) => next.GetLowestLocationNumber(prev)));
            return results.Min();
        }

        public async Task<long> GetLowestDestinationForSeedRange()
        {
            var lowest = long.MaxValue;
            var tasks = new List<Task>();

            for (int i = 0; i < seeds.Length; i += 2)
            {
                var seedIndex = i;
                var length = seeds[i + 1];
                tasks.Add(Task.Run(() =>
                {
                    for (int j = 0; j < length; j++)
                    {
                        var seed = seeds[seedIndex] + j;
                        foreach (var map in maps)
                        {
                            seed = map.GetLowestLocationNumber(seed);
                        }
                        lowest = Math.Min(seed, lowest);
                    }
                    Console.WriteLine($"Done processing seed {seedIndex / 2} of {seeds.Length / 2}");
                }));
            }

            await Task.WhenAll(tasks);

            return lowest;
        }
    }
}
