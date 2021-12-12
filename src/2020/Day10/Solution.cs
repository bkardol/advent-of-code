namespace Day10
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<int[]>
    {
        public override int[] ParseInput(string[] lines) => lines.ToIntArray();

        public override string[] Part1()
        {
            var differences = new Dictionary<int, int>
            {
                [1] = 1,
                [3] = 1
            };
            int previousAdapterJolts = 0;
            foreach (var adapterJolts in Input.OrderBy(jolt => jolt))
            {
                ++differences[adapterJolts - previousAdapterJolts];
                previousAdapterJolts = adapterJolts;
            }

            return new string[]
            {
                $"There are {differences[1]} adapters with 1-jolt differences",
                $"There are {differences[3]} adapters with 3-jolt differences",
                $"The number of differences multiplied is: {differences[1] * differences[3]}"
            };
        }

        public override string[] Part2()
        {
            var adapters = Input.Prepend(0).Append(Input.Max() + 3).OrderBy(jolt => jolt).ToArray();
            var adapterCombinations = new Dictionary<int, long> { [0] = 1 };
            for (int i = 1; i < adapters.Length; ++i)
            {
                var currentAdapter = adapters[i];
                long combinations = 0;
                for (int j = 1; j < 4 && j < i + 1; ++j)
                {
                    var previousAdapter = adapters[i - j];
                    if (previousAdapter >= currentAdapter - 3)
                    {
                        combinations += adapterCombinations[previousAdapter];
                    }
                }
                adapterCombinations[currentAdapter] = combinations;
            }

            return new string[]
            {
                $"There are {adapterCombinations.Values.Max()} distinct arrangements",
            };
        }
    }
}
