namespace Day12
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<Instruction[]>
    {
        private readonly IDictionary<char, int> MoveNorth = new Dictionary<char, int> { ['N'] = 1, ['S'] = -1 };
        private readonly IDictionary<char, int> MoveEast = new Dictionary<char, int> { ['E'] = 1, ['W'] = -1 };
        private readonly IDictionary<char, char> NinetyDegreesRight = new Dictionary<char, char> { ['N'] = 'E', ['E'] = 'S', ['S'] = 'W', ['W'] = 'N' };
        private readonly IDictionary<char, char> NinetyDegreesLeft = new Dictionary<char, char> { ['N'] = 'W', ['W'] = 'S', ['S'] = 'E', ['E'] = 'N' };

        public override Instruction[] ParseInput(string[] lines) => lines.Select(line => new Instruction(line[0], Convert.ToInt32(line[1..]))).ToArray();

        public override string[] Part1()
        {
            char direction = 'E';
            int north = 0;
            int east = 0;

            foreach (var instruction in Input)
            {
                switch (instruction.Action)
                {
                    case 'N':
                    case 'S':
                    case 'F' when direction == 'N' || direction == 'S':
                        north += MoveNorth[instruction.Action == 'F' ? direction : instruction.Action] * instruction.Value;
                        break;
                    case 'E':
                    case 'W':
                    case 'F' when direction == 'E' || direction == 'W':
                        east += MoveEast[instruction.Action == 'F' ? direction : instruction.Action] * instruction.Value;
                        break;
                    case 'R':
                    case 'L':
                        for (int i = instruction.Value; i > 0; i -= 90)
                        {
                            direction = (instruction.Action == 'R' ? NinetyDegreesRight : NinetyDegreesLeft)[direction];
                        }
                        break;
                }
            }

            return new string[]
            {
                $"{(north >= 0 ? "North" : "South")} distance is: {Math.Abs(north)}",
                $"{(east >= 0 ? "East" : "West")} distance is: {Math.Abs(east)}",
                $"Manhattan distance is: {Math.Abs(north) + Math.Abs(east)}"
            };
        }

        public override string[] Part2()
        {
            int waypointNorth = 1;
            int waypointEast = 10;
            int north = 0;
            int east = 0;

            foreach (var instruction in Input)
            {
                switch (instruction.Action)
                {
                    case 'N':
                    case 'S':
                        waypointNorth += MoveNorth[instruction.Action] * instruction.Value;
                        break;
                    case 'E':
                    case 'W':
                        waypointEast += MoveEast[instruction.Action] * instruction.Value;
                        break;
                    case 'F':
                        north += waypointNorth * instruction.Value;
                        east += waypointEast * instruction.Value;
                        break;
                    case 'R':
                        for (int i = instruction.Value; i > 0; i -= 90)
                        {
                            (waypointNorth, waypointEast) = (-waypointEast, waypointNorth);
                        }
                        break;
                    case 'L':
                        for (int i = instruction.Value; i > 0; i -= 90)
                        {
                            (waypointNorth, waypointEast) = (waypointEast, -waypointNorth);
                        }
                        break;
                }
            }

            return new string[]
            {
                $"{(north >= 0 ? "North" : "South")} distance is: {Math.Abs(north)}",
                $"{(east >= 0 ? "East" : "West")} distance is: {Math.Abs(east)}",
                $"Manhattan distance is: {Math.Abs(north) + Math.Abs(east)}"
            };
        }
    }
}
