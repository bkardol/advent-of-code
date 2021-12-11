namespace Day1
{
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<int[]>
    {
        public override int[] ParseInput(string[] lines) => lines.ToIntArray();

        public override string[] Part1()
        {
            var numbers = Input.GetNumbersWithCriteria((first, second) => first + second == 2020).ToArray();

            return new string[]
            {
                $"The first entry is: {numbers[0]}",
                $"The second entry is: {numbers[1]}",
                $"The first and second entry multiplied is: {numbers[0] * numbers[1]}"
            };
        }

        public override string[] Part2()
        {
            var numbers = Input.GetNumbersWithCriteria((first, second, third) => first + second + third == 2020).ToArray();

            return new string[]
            {
                $"The first entry is: {numbers[0]}",
                $"The second entry is: {numbers[1]}",
                $"The third entry is: {numbers[1]}",
                $"The first and second entry multiplied is: {numbers[0] * numbers[1] * numbers[2]}"
            };
        }
    }
}
