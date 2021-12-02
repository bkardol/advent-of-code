namespace Day2
{
    using System;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main()
        {
            var input = ParseInput(InputService.GetInput());

            Part1(input);
            Part2(input);

            Console.ReadLine();
        }

        static Command[] ParseInput(string[] lines) => lines.Select(line => line.Split(' ')).Select(commandParts => new Command(Enum.Parse<Direction>(commandParts[0], true), Convert.ToInt32(commandParts[1]))).ToArray();

        static void Part1(Command[] commands)
        {
            Console.WriteLine();
            Console.WriteLine("PART 1");
            int horizontalPosition = commands.Sum(c => c.HorizontalValue);
            int depthPosition = commands.Sum(c => c.DepthValue);
            Console.WriteLine($"Horizontal: {horizontalPosition}");
            Console.WriteLine($"Depth: {depthPosition}");
            Console.WriteLine($"Horizontal and Depth multiplied: {horizontalPosition * depthPosition}");
        }


        static void Part2(Command[] commands)
        {
            Console.WriteLine();
            Console.WriteLine("PART 2");
            int horizontalPosition = commands.Sum(c => c.HorizontalValue);
            int depthPosition = commands.Aggregate(new CommandTotal(0,0), (total, command) => command.CalculateTotal(total)).Depth;
            Console.WriteLine($"Horizontal: {horizontalPosition}");
            Console.WriteLine($"Depth: {depthPosition}");
            Console.WriteLine($"Horizontal and Depth multiplied: {horizontalPosition * depthPosition}");
        }
    }
}
