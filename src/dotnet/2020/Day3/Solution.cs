namespace Day3
{
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Ground[][]>
    {
        public override Ground[][] ParseInput(string[] lines) => lines.ToBoolMatrix<Ground>('#', isHorizontalPattern: true);

        public override string[] Part1()
        {
            var ground = Input[0][0];
            int treeEncounters = 0;
            while ((ground = MoveRightBottom(ground, 3, 1)) != null)
            {
                if (ground.Value)
                {
                    ++treeEncounters;
                }
            }

            return new string[]
            {
                $"We encounter {treeEncounters} trees"
            };
        }

        public override string[] Part2()
        {
            var allTreeEncounters = new[]
            {
                    (right: 1, bottom: 1),
                    (right: 3, bottom: 1),
                    (right: 5, bottom: 1),
                    (right: 7, bottom: 1),
                    (right: 1, bottom: 2)
                }.Select(rb =>
                {
                    var ground = Input[0][0];
                    long treeEncounters = 0;
                    while (ground != null)
                    {
                        if ((ground = MoveRightBottom(ground, rb.right, rb.bottom)) == null)
                        {
                            break;
                        }
                        if (ground.Value)
                        {
                            ++treeEncounters;
                        }
                    }
                    return treeEncounters;
                });

            return allTreeEncounters.Select(e => $"We encounter {e} trees").ToArray().Concat(new[]
            {
                $"The encountered trees multiplied gives: {allTreeEncounters.Aggregate((prev, curr) => prev * curr)}"
            }).ToArray();
        }

        private static Ground MoveRightBottom(Ground ground, int right, int bottom) => ground.PositionsRight(right).PositionsBottom(bottom);
    }
}
