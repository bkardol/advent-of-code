namespace Day2
{
    using System;
    using System.Linq;
    using Common;

    internal class Day2 : Puzzle<Command>
    {
        public override Command[] ParseInput(string[] lines) => lines.Select(line => line.Split(' ')).Select(commandParts => new Command(Enum.Parse<Direction>(commandParts[0], true), Convert.ToInt32(commandParts[1]))).ToArray();

        public override string[] Part1()
        {
            int horizontalPosition = Input.Sum(c => c.HorizontalValue);
            int depthPosition = Input.Sum(c => c.DepthValue);
            return new string[]
            {
                $"Horizontal: {horizontalPosition}",
                $"Depth: {depthPosition}",
                $"Horizontal and Depth multiplied: {horizontalPosition * depthPosition}"
            };
        }

        public override string[] Part2()
        {
            int horizontalPosition = Input.Sum(c => c.HorizontalValue);
            int depthPosition = Input.Aggregate(new CommandTotal(0, 0), (total, command) => command.CalculateTotal(total)).Depth;
            return new string[]
            {
                $"Horizontal: {horizontalPosition}",
                $"Depth: {depthPosition}",
                $"Horizontal and Depth multiplied: {horizontalPosition * depthPosition}"
            };
        }
    }
}
