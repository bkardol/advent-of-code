namespace Day8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal record Instruction(int Id, string Operation, int Argument);

    internal class Solution : PuzzleSolution<Instruction[]>
    {
        private const string NOP = "nop";
        private const string JMP = "jmp";
        private const string ACC = "acc";

        public override Instruction[] ParseInput(string[] lines) => lines
            .Select(line => line.Split(new char[] { ' ', '+' }, StringSplitOptions.RemoveEmptyEntries))
            .Select((splitted, i) => new Instruction(i, splitted[0], Convert.ToInt32(splitted[1])))
            .ToArray();

        public override string[] Part1()
        {
            (int accumulator, _) = ExecuteUntilItLoops(Input);
            return new string[]
            {
                $"The value in the accumulator is {accumulator}"
            };
        }

        public override string[] Part2()
        {
            int expectedLastInstructionId = Input.Last().Id;
            int accumulatorResult = 0;
            foreach (var instruction in Input)
            {
                var instructions = Input.ToArray();
                switch (instruction.Operation)
                {
                    case JMP:
                        instructions[instruction.Id] = new Instruction(instruction.Id, NOP, instruction.Argument);
                        break;
                    case NOP:
                        instructions[instruction.Id] = new Instruction(instruction.Id, JMP, instruction.Argument);
                        break;
                    default:
                        continue;
                }
                (int accumulator, int lastInstructionId) = ExecuteUntilItLoops(instructions);
                if(lastInstructionId == expectedLastInstructionId)
                {
                    accumulatorResult = accumulator;
                    break;
                }
            }
            return new string[]
            {
                $"The value in the accumulator is {accumulatorResult}"
            };
        }

        private (int accumulator, int lastInstructionId) ExecuteUntilItLoops(Instruction[] instructions)
        {
            var ranInstructions = new HashSet<int>();
            var accumulator = 0;
            int lastInstructionId = 0;
            for (int i = 0; i < instructions.Length; ++i)
            {
                var instruction = instructions[i];
                if (ranInstructions.Contains(instruction.Id))
                {
                    break;
                }
                switch (instruction.Operation)
                {
                    case ACC:
                        accumulator += instruction.Argument;
                        break;
                    case JMP:
                        i += instruction.Argument - 1;
                        break;
                }
                ranInstructions.Add(instruction.Id);
                lastInstructionId = instruction.Id;
            }
            return (accumulator, lastInstructionId);
        }
    }
}
