namespace Day1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<string[]>
    {
        private readonly Dictionary<string, int> numberTable = new Dictionary<string, int>{
            {"zero",0},{"one",1},{"two",2},{"three",3},{"four",4},{"five",5},{"six",6},{"seven",7},{"eight",8},{"nine",9}
        };

        public override string[] ParseInput(string[] lines) => lines;

        public override string[] Part1()
        {
            var values = Input.Select(value => Regex.Replace(value, @"[^0-9]", "")).Select(value => value.Length > 0 ? $"{value[0]}{value[value.Length - 1]}" : "0");
            return new string[] { values.Sum(val => Convert.ToInt32(val)).ToString() };
        }

        public override string[] Part2()
        {
            var values = Input.Select(value =>
            {
                var firstNumber = -1;
                var lastNumber = -1;
                for (int i = 1; i <= value.Length; i++)
                {
                    if (firstNumber == -1)
                    {
                        var stringPart = value.Substring(0, i);
                        var lastChar = stringPart.Last();
                        if (Char.IsNumber(lastChar))
                        {
                            firstNumber = lastChar - '0';
                        }
                        else
                        {
                            var key = numberTable.Keys.FirstOrDefault(key => stringPart.Contains(key));
                            if (key != null)
                                firstNumber = numberTable[key];
                        }
                    }
                    if (lastNumber == -1)
                    {
                        var stringPart = value.Substring(value.Length - i, i);
                        var lastChar = stringPart.First();
                        if (Char.IsNumber(lastChar))
                        {
                            lastNumber = lastChar - '0';
                        }
                        else
                        {
                            var key = numberTable.Keys.FirstOrDefault(key => stringPart.Contains(key));
                            if (key != null)
                                lastNumber = numberTable[key];
                        }
                    }
                }
                return $"{firstNumber}{lastNumber}";
            });
            return new string[] { values.Sum(val => Convert.ToInt32(val)).ToString() };
        }
    }
}
