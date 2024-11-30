namespace Day2
{
    using System;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<PasswordPolicy[]>
    {
        public override PasswordPolicy[] ParseInput(string[] lines) => lines
            .Select(line => line.Split(new [] { ' ', ':', '-' }, StringSplitOptions.RemoveEmptyEntries))
            .Select(splitted => new PasswordPolicy(Convert.ToInt32(splitted[0]), Convert.ToInt32(splitted[1]), splitted[2][0], splitted[3])).ToArray();

        public override string[] Part1()
        {
            int validPasswords = Input.Count(p => p.IsValidOldPolicy);
            return new string[]
            {
                $"{validPasswords} passwords are valid"
            };
        }

        public override string[] Part2()
        {
            int validPasswords = Input.Count(p => p.IsValidNewPolicy);
            return new string[]
            {
                $"{validPasswords} passwords are valid"
            };
        }
    }
}
