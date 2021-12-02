namespace Common
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class PuzzleProgram
    {
        public static void Main()
        {
            var puzzleType = Assembly.GetCallingAssembly().GetTypes()
                         .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(PuzzleSolution<>))
                         .First();

            PuzzleSolution puzzle = Activator.CreateInstance(puzzleType) as PuzzleSolution;

            Part1(puzzle);
            Part2(puzzle);

            Console.ReadLine();
        }

        static void Part1(PuzzleSolution puzzle) => Part(1, puzzle.Part1);

        static void Part2(PuzzleSolution puzzle) => Part(2, puzzle.Part2);

        static void Part(int puzzleNumber, Func<string[]> puzzleDelegate)
        {
            Console.WriteLine();
            Console.WriteLine($"PART {puzzleNumber}");

            var results = puzzleDelegate();
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
