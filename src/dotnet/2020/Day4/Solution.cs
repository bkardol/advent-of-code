namespace Day4
{
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Passport[]>
    {
        public override Passport[] ParseInput(string[] lines) => lines
            .GroupByEmptyLine()
            .Select(lines => new Passport(
                string.Join(' ', lines)
                .Split(' ')
                .Select(kv => kv.Split(':'))
                .ToDictionary(kv => kv[0], kv => kv[1])))
            .ToArray();

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
