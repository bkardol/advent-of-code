namespace Day9
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Sequence
    {
        private readonly long[] numbers;
        private readonly List<long[]> runs = new List<long[]>();

        public Sequence(long[] numbers)
        {
            this.numbers = numbers;
            runs.Add(numbers);
            while (runs.Count == 0 || runs.Last().Any(v => v != 0))
            {
                var increases = new List<long>();
                var prevRun = runs.Last();
                for (int i = 1; i < prevRun.Length; i++)
                {
                    increases.Add(prevRun[i] - prevRun[i - 1]);
                }
                runs.Add(increases.ToArray());
            }
        }

        public long GetExtrapolated()
        {
            return runs.AsEnumerable().Reverse().Aggregate((long)0, (sum, run) => sum + run.Last());
        }

        public long GetExtrapolatedBackwards()
        {
            return runs.AsEnumerable().Reverse().Aggregate((long)0, (sum, run) => run.First() - sum);
        }
    }
}
