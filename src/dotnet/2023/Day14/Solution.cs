namespace Day14
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Platform>
    {
        public override Platform ParseInput(string[] lines) => new Platform(lines.ToMatrix<Location, LocationType>(pipe => (LocationType)pipe));

        public override string[] Part1()
        {
            Input.TiltToSlideNorth();

            var sum = Input.GetTotalLoad();

            return new string[]
            {
                sum.ToString()
            };
        }

        public override string[] Part2()
        {
            var runs = 1000000000;
            bool lastRuns = false;
            var pattern = new Dictionary<string, bool>();
            for (int i = 0; i < runs; i++)
            {
                var key = Input.CreateKey();
                if (!lastRuns && pattern.ContainsKey(key) && pattern[key])
                {
                    lastRuns = true;
                    i = runs - (runs - i) % pattern.Values.Count(p => p);
                }
                if (pattern.ContainsKey(key))
                {
                    pattern[key] = true;
                }
                else
                {
                    pattern.Add(key, false);
                }

                Input.TiltToSlideNorth();
                Input.TiltToSlideWest();
                Input.TiltToSlideSouth();
                Input.TiltToSlideEast();
            }

            var sum = Input.GetTotalLoad();
            return new string[]
            {
                sum.ToString()
            };
        }
    }
}
