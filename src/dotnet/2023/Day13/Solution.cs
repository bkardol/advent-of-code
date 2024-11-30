namespace Day13
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Valley[]>
    {
        public override Valley[] ParseInput(string[] lines)
        {
            var valleys = new List<Valley>();
            var valley = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    valleys.Add(new Valley(valley.ToBoolMatrix<Location>('#')));
                    valley = new List<string>();
                }
                else
                {
                    valley.Add(line);
                }
            }
            valleys.Add(new Valley(valley.ToBoolMatrix<Location>('#')));
            return valleys.ToArray();
        }

        public override string[] Part1()
        {
            long totalReflectionValue = Input.Sum(valley => valley.GetReflectionValue());
            return new string[]
            {
                "Summarizing the reflection line notes gives: " + totalReflectionValue.ToString()
            };
        }

        public override string[] Part2()
        {
            long totalReflectionValue = Input.Sum(valley => valley.GetFixedReflectionValue());
            return new string[]
            {
                "Summarizing the reflection line notes gives: " + totalReflectionValue.ToString()
            };
        }
    }
}
