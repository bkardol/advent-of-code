namespace Day11
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Octopus[][]>
    {
        public override Octopus[][] ParseInput(string[] lines) => lines.ToIntMatrix<Octopus>(true);

        public override string[] Part1()
        {
            (_, int amountOfFlashes) = OctopusDisco(100, false);

            return new string[]
            {
                $"There have been {amountOfFlashes} flashes after 100 steps."
            };
        }

        public override string[] Part2()
        {
            (int stepAllOctopusesFlash, _) = OctopusDisco(int.MaxValue, true);

            return new string[]
            {
                $"The first step during which all octopuses flash is: {stepAllOctopusesFlash}."
            };
        }

        private (int Steps, int AmountOfFlashes) OctopusDisco(int maxSteps, bool stopWhenAllFlash)
        {
            int steps = 1;
            int amountOfFlashes = 0;
            for (; steps <= maxSteps; ++steps)
            {
                var flashingOctopuses = new HashSet<Octopus>();
                foreach (var octopuses in Input)
                {
                    foreach (var octopus in octopuses)
                    {
                        if (octopus.IncreaseEnergyLevel() > 9)
                        {
                            flashingOctopuses.Add(octopus);
                        }
                    }
                }

                for (int j = 0; j < flashingOctopuses.Count; ++j)
                {
                    ++amountOfFlashes;
                    foreach (var adjacent in flashingOctopuses.ElementAt(j).GetAdjacent())
                    {
                        if (adjacent.IncreaseEnergyLevel() > 9)
                        {
                            flashingOctopuses.Add(adjacent);
                        }
                    }
                }

                if (stopWhenAllFlash && flashingOctopuses.Count == Input.Sum(o => o.Length))
                {
                    break;
                }

                foreach (var octopuses in Input)
                {
                    foreach (var octopus in octopuses)
                    {
                        if (octopus.Value > 9)
                        {
                            octopus.ResetEnergyLevel();
                        }
                    }
                }
            }

            return (Steps: steps, AmountOfFlashes: amountOfFlashes);
        }
    }
}
