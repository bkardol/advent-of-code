namespace Day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.Extensions;

    internal class Solution : PuzzleSolution<int[]>
    {
        public override int[] ParseInput(string[] lines) => lines[0].ToIntArray(',');

        public override string[] Part1()
        {
            var fuelCosts = Enumerable.Range(0, Input.Max(c => c) - Input.Min(c => c) + 1).ToDictionary(f => f);
            return GetFuelCost(fuelCosts);
        }

        public override string[] Part2()
        {
            int fuelCost = 0;
            var fuelCosts = Enumerable.Range(0, Input.Max(c => c) - Input.Min(c => c) + 1).ToDictionary(f => f, f => fuelCost += f);
            return GetFuelCost(fuelCosts);
        }

        public string[] GetFuelCost(Dictionary<int, int> stepFuelCosts)
        {
            int leastFuel = int.MaxValue;
            for (int i = Input.Min(c => c); i < Input.Max(c => c); ++i)
            {
                var fuel = Input.Select(c => stepFuelCosts[Math.Abs(c - i)]).Sum();
                if (fuel < leastFuel)
                {
                    leastFuel = fuel;
                }
            }

            return new string[]
            {
                $"The crabs must spend {leastFuel} of fuel to align the position."
            };
        }
    }
}
