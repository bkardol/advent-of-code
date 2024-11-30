namespace Day3
{
    using System;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<SchematicPart[][]>
    {
        public override SchematicPart[][] ParseInput(string[] lines) => 
            lines.ToIntMatrix<SchematicPart>((character) =>
            {
                int value = PartConstants.Symbol;
                switch (character)
                {
                    case '.':
                        value = PartConstants.Nothing;
                        break;
                    case '*':
                        value = PartConstants.Gear;
                        break;
                    default:
                        if (!int.TryParse(character.ToString(), out value))
                        {
                            value = PartConstants.Symbol;
                        }
                        break;
                }
                return value;
            }, true);

        public override string[] Part1()
        {
            var sum = Input.Sum(parts => parts.Sum(part => part.PartNumber));

            return new string[]
            {
                "Sum of all part numbers: " + sum.ToString()
            };
        }

        public override string[] Part2()
        {
            var sum = Input.Sum(parts => parts.Sum(part => part.GearRatio));
            return new string[]
            {
                "Sum of all gear ratios: " + sum.ToString()
            };
        }
    }
}
