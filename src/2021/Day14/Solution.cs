namespace Day14
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.String;

    public record PolymerInsertionPair(string BetweenElements, char Element, string[] Combinations);
    public record PolymerInstructions(string Template, PolymerInsertionPair[] InsertionPairs);
    internal class Solution : PuzzleSolution<PolymerInstructions>
    {
        public override PolymerInstructions ParseInput(string[] lines) => new PolymerInstructions(lines[0], lines.Skip(2).Select(line => line.Split(" -> ")).Select(pair =>
            new PolymerInsertionPair(pair[0], pair[1][0], new[] { new[] { pair[0][0], pair[1][0] }, new[] { pair[1][0], pair[0][1] } }.Select(c => new string(c)).ToArray())).ToArray());

        public override string[] Part1()
        {
            var polymerCounts = GetPolymerOccurences(10);
            var leastCommonElement = Math.Ceiling(polymerCounts.Min(p => p.Value) / 2.0);
            var mostCommonElement = Math.Ceiling(polymerCounts.Max(p => p.Value) / 2.0);

            return new string[]
            {
                $"The least common element is: {leastCommonElement}",
                $"The most common element is: {mostCommonElement}",
                $"The most common element subtracted by the least common element is: {mostCommonElement - leastCommonElement}",
            };
        }

        public override string[] Part2()
        {
            var polymerCounts = GetPolymerOccurences(40);
            var leastCommonElement = Math.Ceiling(polymerCounts.Min(p => p.Value) / 2.0);
            var mostCommonElement = Math.Ceiling(polymerCounts.Max(p => p.Value) / 2.0);

            return new string[]
            {
                $"The least common element is: {leastCommonElement}",
                $"The most common element is: {mostCommonElement}",
                $"The most common element subtracted by the least common element is: {mostCommonElement - leastCommonElement}",
            };
        }

        private Dictionary<char, long> GetPolymerOccurences(int amountOfSteps)
        {
            string result = Input.Template;
            var polymerPairCounts = new Dictionary<string, long>();
            for (int i = 1; i < Input.Template.Length; ++i)
            {
                string polymerPair = Input.Template.Substring(i - 1, 2);
                if (polymerPairCounts.ContainsKey(polymerPair))
                    polymerPairCounts[polymerPair]++;
                else
                    polymerPairCounts[polymerPair] = 1;
            }

            for (int steps = 0; steps < amountOfSteps; ++steps)
            {
                var newPolymerPairCounts = new Dictionary<string, long>();
                foreach (var pairCount in polymerPairCounts)
                {
                    var pair = Input.InsertionPairs.First(p => p.BetweenElements == pairCount.Key);
                    foreach (var combination in pair.Combinations)
                    {
                        if (newPolymerPairCounts.ContainsKey(combination))
                            newPolymerPairCounts[combination] += pairCount.Value;
                        else
                            newPolymerPairCounts[combination] = pairCount.Value;
                    }
                }
                polymerPairCounts = newPolymerPairCounts;
            }

            var polymerCounts = new Dictionary<char, long>();
            foreach (var pairCount in polymerPairCounts)
            {
                foreach (var polymer in pairCount.Key)
                {
                    if (polymerCounts.ContainsKey(polymer))
                        polymerCounts[polymer] += pairCount.Value;
                    else
                        polymerCounts[polymer] = pairCount.Value;
                }
            }
            polymerCounts[Input.Template.Last()]++;

            return polymerCounts;
        }
    }
}
