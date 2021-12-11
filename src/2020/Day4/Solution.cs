namespace Day4
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<Passport[]>
    {
        public override Passport[] ParseInput(string[] lines) => lines.Aggregate(new List<Passport> { new Passport() }, (passports, line) =>
        {
            if (string.IsNullOrEmpty(line))
            {
                passports.Add(new Passport());
            }
            else
            {
                passports.Last().AddFields(line.Split(' ').Select(kv => kv.Split(':')).ToDictionary(kv => kv[0], kv => kv[1]));
            }
            return passports;
        }).ToArray();

        public override string[] Part1()
        {
            int validPassports = Input.Count(p => p.IsValidWithoutValidations);
            return new string[]
            {
                $"There are {validPassports} valid \"passports\""
            };
        }

        public override string[] Part2()
        {
            int validPassports = Input.Count(p => p.IsValidWithValidations);
            return new string[]
            {
                $"There are {validPassports} valid \"passports\""
            };
        }
    }
}
