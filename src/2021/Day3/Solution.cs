namespace Day3
{
    using System;
    using System.Collections;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<BitArray>
    {
        public override BitArray[] ParseInput(string[] lines) => lines.Select(line => new BitArray(line.Select(bit => bit == '1').Reverse().ToArray())).ToArray();

        public override string[] Part1()
        {
            int inputBitLength = Input[0].Length;
            var gammaBits = new BitArray(inputBitLength);
            var epsilonBits = new BitArray(inputBitLength);
            for (int i = inputBitLength - 1; i >= 0; --i)
            {
                var bit = GetMostPresentBit(Input, i);
                gammaBits[i] = bit;
                epsilonBits[i] = !bit;
            }

            var gamma = BinaryToInt(gammaBits);
            var epsilon = BinaryToInt(epsilonBits);

            return new string[]
            {
                $"Gamma rate: {gamma}",
                $"Epsilon rate: {epsilon}",
                $"The power consumption of the submarine is {gamma * epsilon}"
            };
        }

        public override string[] Part2()
        {
            int oxygenGeneratorRating = GetRatingWithBitCriteria(true);
            int co2ScrubberRating = GetRatingWithBitCriteria(false);

            return new string[]
            {
                $"Oxygen Generator rate: {oxygenGeneratorRating}",
                $"CO2 Scrubber rate: {co2ScrubberRating}",
                $"The life support rating of the submarine is {oxygenGeneratorRating * co2ScrubberRating}"
            };
        }

        private int GetRatingWithBitCriteria(bool mostCommon)
        {
            int inputBitLength = Input[0].Length;
            var diagnostics = Input;
            for (int i = inputBitLength - 1; i >= 0 && diagnostics.Length > 1; --i)
            {
                var bit = GetMostPresentBit(diagnostics, i);
                diagnostics = diagnostics.Where(d => mostCommon ? d[i] == bit : d[i] != bit).ToArray();
            }
            return BinaryToInt(diagnostics[0]);
        }

        private static bool GetMostPresentBit(BitArray[] bitArrays, int bitIndex) => bitArrays.Count(d => d[bitIndex]) >= bitArrays.Length / 2.0;

        private static int BinaryToInt(BitArray bits)
        {
            var results = new int[1];
            bits.CopyTo(results, 0);
            return results[0];
        }
    }
}
