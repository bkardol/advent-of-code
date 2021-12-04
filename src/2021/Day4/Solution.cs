namespace Day4
{
    using System.Linq;
    using Common;
    using Common.Extensions;

    internal class Solution : PuzzleSolution<Bingo>
    {
        public override Bingo ParseInput(string[] lines) =>
            new(
                lines[0].ToIntArray(','),
                lines.Skip(2)
                    .Chunk(6)
                    .Select(cardLines => new BingoCard(cardLines
                            .Select(line => line
                            .ToIntArray(' '))
                        .Where(r => r.Length > 0).ToArray()))
                    .ToArray());

        public override string[] Part1()
        {
            int lastDraw = Input.DrawUntilFirstWin();
            int sumOfUnmarked = Input.FirstWinningCard.GetSumOfAllUnmarked();
            return new string[]
            {
                $"Sum of all unmarked fields: {sumOfUnmarked}",
                $"Last draw: {lastDraw}",
                $"Sum of all unmarked fields and Last draw multiplied: {sumOfUnmarked * lastDraw}"
            };
        }

        public override string[] Part2()
        {
            int lastDraw = Input.DrawUntilLastWin();
            int sumOfUnmarked = Input.LastWinningCard.GetSumOfAllUnmarked();
            return new string[]
            {
                $"Sum of all unmarked fields: {sumOfUnmarked}",
                $"Last draw: {lastDraw}",
                $"Sum of all unmarked fields and Last draw multiplied: {sumOfUnmarked * lastDraw}"
            };
        }
    }
}
