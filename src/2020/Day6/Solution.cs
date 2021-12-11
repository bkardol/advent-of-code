namespace Day6
{
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<string[][]>
    {
        public override string[][] ParseInput(string[] lines) => lines.GroupByEmptyLine();

        public override string[] Part1()
        {
            int sumOfYesCounts = Input.Sum(personAnswers => personAnswers.SelectMany(answer => answer.ToCharArray()).Distinct().Count());

            return new string[]
            {
                $"The sum of the questions answered with \"yes\" is: {sumOfYesCounts}"
            };
        }

        public override string[] Part2()
        {
            int sumOfYesCounts = Input.Sum(personAnswers => personAnswers.Aggregate((prev, curr) => string.Concat(prev.Where(c => curr.Contains(c)))).Length);

            return new string[]
            {
                $"The sum of the questions answered with \"yes\" by everyone is: {sumOfYesCounts}"
            };
        }
    }
}
