namespace Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.Extensions;

    internal class Solution : PuzzleSolution<Octopus[][]>
    {
        public override Octopus[][] ParseInput(string[] lines)
        {
            var octopuses = lines.Select(l => l.ToIntArray().Select((energyLevel) => new Octopus(energyLevel)).ToArray()).ToArray();
            for (int y = 0; y < octopuses.Length; ++y)
            {
                var row = octopuses[y];
                for (int x = 0; x < row.Length; ++x)
                {
                    if (x > 0)
                    {
                        octopuses[y][x].SetLeft(row[x - 1]);
                    }
                    if (x < row.Length - 1)
                    {
                        octopuses[y][x].SetRight(row[x + 1]);
                    }
                    if (y > 0)
                    {
                        octopuses[y][x].SetTop(octopuses[y - 1][x]);
                    }
                    if (y < octopuses.Length - 1)
                    {
                        octopuses[y][x].SetBottom(octopuses[y + 1][x]);
                    }
                }
            }

            return octopuses;
        }


        public override string[] Part1()
        {
            int amountOfFlashes = 0;
            for (int i = 0; i < 100; ++i)
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
                    foreach (var adjacent in flashingOctopuses.ElementAt(j).GetAdjacentOctopuses(true))
                    {
                        if (adjacent.IncreaseEnergyLevel() > 9)
                        {
                            flashingOctopuses.Add(adjacent);
                        }
                    }
                }

                foreach (var octopuses in Input)
                {
                    foreach (var octopus in octopuses)
                    {
                        if (octopus.EnergyLevel > 9)
                        {
                            octopus.ResetEnergyLevel();
                        }
                    }
                }
            }

            return new string[]
            {
                $"There have been {amountOfFlashes} flashes after 100 steps."
            };
        }

        public override string[] Part2()
        {
            int stepAllOctopusesFlash = 0;
            while(stepAllOctopusesFlash < int.MaxValue)
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
                    foreach (var adjacent in flashingOctopuses.ElementAt(j).GetAdjacentOctopuses(true))
                    {
                        if (adjacent.IncreaseEnergyLevel() > 9)
                        {
                            flashingOctopuses.Add(adjacent);
                        }
                    }
                }

                ++stepAllOctopusesFlash;
                if (flashingOctopuses.Count == Input.Sum(o => o.Length))
                {
                    break;
                }

                foreach (var octopuses in Input)
                {
                    foreach (var octopus in octopuses)
                    {
                        if (octopus.EnergyLevel > 9)
                        {
                            octopus.ResetEnergyLevel();
                        }
                    }
                }
            }

            return new string[]
            {
                $"The first step during which all octopuses flash is: {stepAllOctopusesFlash + 100}."
            };
        }
    }
}
