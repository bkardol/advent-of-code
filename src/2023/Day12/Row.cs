namespace Day12
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Row
    {
        private string fullRow;
        private int[] allDamagedSprings;
        private static readonly Dictionary<(string row, string numbers), long> cache = new();

        public Row(string row, int[] damagedSprings)
        {
            this.fullRow = row;
            this.allDamagedSprings = damagedSprings;
        }

        public void Unfold()
        {
            fullRow = string.Join("?", Enumerable.Repeat(fullRow, 5));
            allDamagedSprings = Enumerable.Repeat(allDamagedSprings, 5).SelectMany(springs => springs).ToArray();
        }

        public long GetNumberOfArrangements() => GetNumberOfArrangements(fullRow, allDamagedSprings);

        private long GetNumberOfArrangements(string springs, int[] brokenSpringGroups)
        {
            var hasAnySprings = springs.Length > 0;
            var hasAnyBrokenSpringGroups = brokenSpringGroups.Length > 0;
            if (!hasAnySprings)
            {
                return hasAnyBrokenSpringGroups ? 0 : 1;
            }
            if (!hasAnyBrokenSpringGroups)
            {
                return ContainsBrokenSpring(springs) ? 0 : 1;
            }

            var cacheKey = (springs, string.Join(";", brokenSpringGroups));
            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }

            long arrangements = 0;
            var spring = springs[0];
            if (IsPossiblyOperationalSpring(spring))
            {
                arrangements += GetNumberOfArrangements(springs[1..], brokenSpringGroups);
            }

            var damagedSpringCount = brokenSpringGroups[0];
            if (IsPossiblyBrokenSpring(spring))
            {
                if (
                    damagedSpringCount <= springs.Length &&
                    !ContainsOperationalSpring(springs[..damagedSpringCount]) &&
                    (damagedSpringCount == springs.Length || !IsBrokenSpring(springs[damagedSpringCount]))
                )
                {
                    var newSprings = damagedSpringCount == springs.Length ? "" : springs[(damagedSpringCount + 1)..];
                    arrangements += GetNumberOfArrangements(newSprings, brokenSpringGroups.Skip(1).ToArray());
                }
            }

            return cache[cacheKey] = arrangements;
        }

        private static bool ContainsBrokenSpring(string springs) => springs.Contains('#');
        private static bool ContainsOperationalSpring(string springs) => springs.Contains('.');
        private static bool IsBrokenSpring(char spring) => spring == '#';
        private static bool IsPossiblyBrokenSpring(char spring) => spring == '#' || spring == '?';
        private static bool IsPossiblyOperationalSpring(char spring) => spring == '.' || spring == '?';
    }
}
