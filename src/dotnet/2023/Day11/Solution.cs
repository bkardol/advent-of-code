namespace Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<SpaceCoord[][]>
    {
        List<int> emptyRowIndexes;
        List<int> emptyColumnIndexes;

        public override SpaceCoord[][] ParseInput(string[] lines)
        {
            emptyRowIndexes = new List<int>();
            emptyColumnIndexes = new List<int>();
            var rows = lines.Select(line => line.ToCharArray()).ToArray();
            var rowLength = rows[0].Length;
            for (int i = 0; i < rows.Length || i < rowLength; i++)
            {
                if (i < rows.Length && rows[i].All(r => r == '.'))
                    emptyRowIndexes.Add(i);
                if (i < rowLength && rows.Select((line) => line[i]).All(r => r == '.'))
                    emptyColumnIndexes.Add(i);
            }

            int galaxyCount = 0;
            return lines
                .Select((line, i) => line
                    .Select((c, j) => new SpaceCoord(c == '#' ? ++galaxyCount : 0, j, i))
                    .ToArray())
                .ToArray();
        }

        public override string[] Part1()
        {
            var sumOfGalaxyDistances = getSumOfDistancesBetweenGalaxies(1);
            return new string[]
            {
                "The sum of distance between all galaxies is: " + sumOfGalaxyDistances.ToString()
            };
        }

        public override string[] Part2()
        {
            var sumOfGalaxyDistances = getSumOfDistancesBetweenGalaxies(999999);
            return new string[]
            {
                "The sum of distance between all galaxies is: " + sumOfGalaxyDistances.ToString()
            };
        }

        private long getSumOfDistancesBetweenGalaxies(int factor)
        {
            var galaxies = Input.SelectMany(g => g.Where(p => p.IsGalaxy)).ToList();
            long sumOfGalaxyDistances = 0;
            var processedGalaxies = new HashSet<Tuple<int, int, int, int>>();
            foreach (var g1 in galaxies)
            {
                foreach (var g2 in galaxies)
                {
                    if (!processedGalaxies.Contains(new Tuple<int, int, int, int>(g2.X, g2.Y, g1.X, g1.Y)))
                    {
                        processedGalaxies.Add(new Tuple<int, int, int, int>(g1.X, g1.Y, g2.X, g2.Y));
                        if (g1 != g2)
                        {
                            long xMin = Math.Min(g1.X, g2.X);
                            long xMax = Math.Max(g1.X, g2.X);
                            long yMin = Math.Min(g1.Y, g2.Y);
                            long yMax = Math.Max(g1.Y, g2.Y);
                            xMin += (emptyColumnIndexes.Count(c => c < xMin) * factor);
                            xMax += (emptyColumnIndexes.Count(c => c < xMax) * factor);
                            yMin += (emptyRowIndexes.Count(c => c < yMin) * factor);
                            yMax += (emptyRowIndexes.Count(c => c < yMax) * factor);
                            sumOfGalaxyDistances +=
                                ((xMax - xMin) +
                                (yMax - yMin));
                        }
                    }
                }
            }
            return sumOfGalaxyDistances;
        }
    }
}
