namespace Day1
{
    using System;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main()
        {
            var depths = InputService.GetInput().Select(line => Convert.ToInt32(line)).ToArray();

            Part1(depths);
            Part2(depths);

            Console.ReadLine();
        }

        static void Part1(int[] depths)
        {
            Console.WriteLine();
            Console.WriteLine("PART 1");
            var increasedDepths = GetIncreasedDepths(depths);
            Console.WriteLine($"There are {increasedDepths} measurements that are larger than the previous measurement.");
        }

        static void Part2(int[] depths)
        {
            Console.WriteLine();
            Console.WriteLine("PART 2");
            var increasedWindowedDepths = GetIncreasedDepths(depths.Skip(2).Select((depth, i) => depth + depths[i] + depths[i + 1]).ToArray());
            Console.WriteLine($"There are {increasedWindowedDepths} sums that are larger than the previous sum.");
        }

        static int GetIncreasedDepths(int[] depths) => depths.Where((depth, i) => i > 0 && depth > depths[i - 1]).Count();
    }
}
