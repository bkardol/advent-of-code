namespace Day8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal record Digits(string[] Input, string[] Output);

    internal class Solution : PuzzleSolution<Digits[]>
    {
        public override Digits[] ParseInput(string[] lines) => lines.Select(line =>
        {
            var splitted = line.Split(" | ").Select(i => i.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(i => string.Concat(i.OrderBy(c => c))).ToArray()).ToArray();
            return new Digits(splitted[0], splitted[1]);
        }).ToArray();

        public override string[] Part1()
        {
            var appearances = Input.Select(allDigits =>
            {
                var one = allDigits.Input.First(i => i.Length == 2);
                var four = allDigits.Input.First(i => i.Length == 4);
                var seven = allDigits.Input.First(i => i.Length == 3);
                var eight = allDigits.Input.First(i => i.Length == 7);
                var digitsToFind = new string[] { one, four, seven, eight };
                return allDigits.Output.Count(o => digitsToFind.Contains(o));
            }).Sum();

            return new string[]
            {
                $"Number 1,4,7,8 appear {appearances} times in the output"
            };
        }

        public override string[] Part2()
        {
            var sumOfOutput = Input.Select(allDigits =>
            {
                var digits = CreateDigitDictionary(allDigits);
                return Convert.ToInt32(string.Concat(allDigits.Output.Select(o => digits[o].ToString())));
            }).Sum();

            return new string[]
            {
                $"The sum of all output numbers is: {sumOfOutput}"
            };
        }

        private Dictionary<string, int> CreateDigitDictionary(Digits allDigits)
        {
            var one = allDigits.Input.First(i => i.Length == 2);
            var four = allDigits.Input.First(i => i.Length == 4);
            var seven = allDigits.Input.First(i => i.Length == 3);
            var eight = allDigits.Input.First(i => i.Length == 7);

            var three = allDigits.Input.First(i => seven.ToCharArray().All(c => i.Contains(c)) && i.Length == 5);

            var nine = allDigits.Input.First(i => four.ToCharArray().All(c => i.Contains(c)) && i.Length == 6);
            var zero = allDigits.Input.First(i => seven.ToCharArray().All(c => i.Contains(c)) && i.Length == 6 && i != nine);
            var six = allDigits.Input.First(i => i.Length == 6 && i != nine && i != zero);
            var five = allDigits.Input.First(i => i.ToCharArray().All(c => six.Contains(c)) && i.Length == 5 && i != three);
            var two = allDigits.Input.First(i => i.Length == 5 && i != three && i != five);


            return new Dictionary<string, int>
            {
                [zero] = 0,
                [one] = 1,
                [two] = 2,
                [three] = 3,
                [four] = 4,
                [five] = 5,
                [six] = 6,
                [seven] = 7,
                [eight] = 8,
                [nine] = 9,
            };
        }
    }
}
