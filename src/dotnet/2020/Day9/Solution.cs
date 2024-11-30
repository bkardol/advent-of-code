namespace Day9
{
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<long[]>
    {
        public override long[] ParseInput(string[] lines) => lines.ToLongArray();

        public override string[] Part1()
        {
            long invalidNumber = Input[GetIndexOfFirstInvalidNumber()];

            return new string[]
            {
                $"{invalidNumber} is the first number that is not the sum of any previous numbers"
            };
        }

        public override string[] Part2()
        {
            int indexOfInvalidNumber = GetIndexOfFirstInvalidNumber();
            long invalidNumber = Input[indexOfInvalidNumber];
            var previousNumbers = Input.Take(indexOfInvalidNumber).ToArray();
            long smallestContiguousNumber = 0;
            long largestContiguousNumber = 0;
            for(int i = 0; i < previousNumbers.Length; ++i)
            {
                int j = i;
                long sumOfRange = 0;
                for(; j < previousNumbers.Length && sumOfRange < invalidNumber; ++j)
                {
                    sumOfRange += previousNumbers[j];
                }

                if(sumOfRange == invalidNumber)
                {
                    var contiguousRange = Input.Skip(i).Take(j - i);
                    smallestContiguousNumber = contiguousRange.Min();
                    largestContiguousNumber = contiguousRange.Max();
                    break;
                }
            }

            return new string[]
            {
                $"{smallestContiguousNumber} is the smallest number in the contiguous range",
                $"{largestContiguousNumber} is the largest number in the contiguous range",
                $"{smallestContiguousNumber + largestContiguousNumber} is the encryption weakness"
            };
        }

        public int GetIndexOfFirstInvalidNumber()
        {
            int preamble = 25;
#if DEBUG
            preamble = 5;
#endif
            for (int i = preamble; i < Input.Length; ++i)
            {
                var previousNumbers = Input.Skip(i - preamble).Take(i);
                if (!previousNumbers.Any(n1 => previousNumbers.Any(n2 => n1 + n2 == Input[i])))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
