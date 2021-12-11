namespace Day7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<BagRule[]>
    {
        public override BagRule[] ParseInput(string[] lines) => lines
            .Select(line => line.Split(" contain "))
            .Select(splitted => new BagRule(
                splitted[0].Replace("bags", "bag"),
                splitted[1]
                    .Split(", ")
                    .Select(containsBag => containsBag.Split(' ', 2))
                    .Select(containsBag => new ContainBags(containsBag[0] == "no" ? 0 : Convert.ToInt32(containsBag[0]), containsBag[1].Replace("bags", "bag").Replace(".", "")))
                    .ToArray()))
            .ToArray();

        public override string[] Part1()
        {
            var bagRules = Input.Where(br => br.ContainsBags.Any(cb => cb.Amount > 0 && cb.Bag == "shiny gold bag")).ToArray();
            HashSet<BagRule> bagsToContainShinyGoldBag = bagRules.ToHashSet();
            while (bagRules.Length > 0)
            {
                bagRules = Input.Where(br => br.ContainsBags.Any(cb => cb.Amount > 0 && bagRules.Any(b => cb.Bag == b.SubjectBag))).ToArray();
                bagsToContainShinyGoldBag.UnionWith(bagRules);
            }
            return new string[]
            {
                $"{bagsToContainShinyGoldBag.Count} bags can eventually contain at least one shiny gold bag"
            };
        }

        public override string[] Part2()
        {
            var bagRules = Input.First(br => br.SubjectBag == "shiny gold bag").ContainsBags.ToArray();
            List<ContainBags> allBagsRequired = bagRules.ToList();
            while (bagRules.Length > 0)
            {
                bagRules = bagRules.SelectMany(b => Input.Where(br => br.SubjectBag == b.Bag).SelectMany(br => br.ContainsBags).Select(cb => new ContainBags(cb.Amount * b.Amount, cb.Bag))).ToArray();
                allBagsRequired.AddRange(bagRules);
            }
            return new string[]
            {
                $"{allBagsRequired.Sum(b => b.Amount)} bags are required inside the single shiny gold bag"
            };
        }
    }
}
