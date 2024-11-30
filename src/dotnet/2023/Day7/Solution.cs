namespace Day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<List<Hand>>
    {
        public override List<Hand> ParseInput(string[] lines) => lines.Select(line =>
        {
            var splitted = line.Split(' ');
            return new Hand(splitted[0], splitted[1], this.Part == 1);
        }).ToList();

        public override string[] Part1() => GetTotalWinnings();

        public override string[] Part2() => GetTotalWinnings();

        private string[] GetTotalWinnings()
        {
            var sorted = this.Input.OrderBy(hand => hand).ToList();
            var totalWinnings = sorted.Select((value, i) => value.Bid * (i + 1)).Sum();
            return new string[]
            {
                totalWinnings.ToString()
            };
        }
    }
}
