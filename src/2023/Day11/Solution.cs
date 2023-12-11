namespace Day11
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<List<List<SpaceCoord>>>
    {
        readonly List<int> emptyRowIndexes = new List<int>();
        readonly List<int> emptyColumnIndexes = new List<int>();

        public override List<List<SpaceCoord>> ParseInput(string[] lines)
        {
            var rows = lines.Select(line => line.ToCharArray()).ToArray();
            var rowLength = rows[0].Length;
            for (int i = 0; i < rows.Length || i < rowLength; i++)
            {
                if (i < rows.Length)
                {
                    var row = rows[i];
                    bool expand = row.All(r => r == '.');
                    if (expand)
                    {
                        emptyRowIndexes.Add(i);
                    }
                }
                if (i < rowLength)
                {
                    var column = rows.Select((line) => line[i]).ToArray();
                    bool expand = column.All(r => r == '.');
                    if (expand)
                    {
                        emptyColumnIndexes.Add(i);
                    }
                }
            }

            //var newSpace = rows.Select(row => row.ToList()).ToList();

            //for (int i = rowIndexesToAdd.Count - 1; i >= 0; i--)
            //    newSpace.Insert(rowIndexesToAdd[i], Enumerable.Range(0, rowLength).Select(i => '.').ToList());

            //for (int i = columnIndexesToAdd.Count - 1; i >= 0; i--)
            //    foreach (var row in newSpace)
            //        row.Insert(columnIndexesToAdd[i], '.');

            int galaxyCount = 0;
            var galaxy = new List<List<SpaceCoord>>();
            for (int i = 0; i < lines.Length; i++)
            {
                var row = new List<SpaceCoord>();
                for (int j = 0; j < lines[i].Length; j++)
                {
                    row.Add(new SpaceCoord(lines[i][j] == '#' ? ++galaxyCount : 0, j, i));
                }
                galaxy.Add(row);
            }

            return galaxy;
        }

        public override string[] Part1()
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
                            long factor = 999999;
                            xMin = xMin + (emptyColumnIndexes.Count(c => c < xMin) * factor);
                            xMax = xMax + (emptyColumnIndexes.Count(c => c < xMax) * factor);
                            yMin = yMin + (emptyRowIndexes.Count(c => c < yMin) * factor);
                            yMax = yMax + (emptyRowIndexes.Count(c => c < yMax) * factor);
                            sumOfGalaxyDistances +=
                                ((xMax - xMin) +
                                (yMax - yMin));
                        }
                    }
                }
            }
            return new string[]
            {
                sumOfGalaxyDistances.ToString()
            };
        }

        public override string[] Part2()
        {
            return new string[]
            {
            };
        }
    }
}
