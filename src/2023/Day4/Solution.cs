namespace Day4
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Common;
    using Common.String;

    internal class Solution : PuzzleSolution<Card[]>
    {
        public override Card[] ParseInput(string[] lines) => lines.Select(line =>
        {
            var cardSplitted = line.Split(": ");
            var cardId = cardSplitted[0];
            var numbersSplitted = cardSplitted[1].Split(" | ");
            var winningNumbers = numbersSplitted[0];
            var cardNumbers = numbersSplitted[1];
            return new Card(int.Parse(Regex.Replace(cardId, @"[^0-9]", "")), winningNumbers.ToIntArray(' '), cardNumbers.ToIntArray(' '));
        }).ToArray();

        public override string[] Part1()
        {
            var totalPoints = Input.Sum(card => card.Points);
            return new string[]
            {
                totalPoints.ToString()
            };
        }

        public override string[] Part2()
        {
            var cards = Input.ToArray();
            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                for (int j = 1; j <= card.AmountOfWinningNumbers && i + j < cards.Length; j++)
                {
                    cards[i + j].AddCopies(card.AmountOfCards);
                }
            }
            return new string[]
            {
                cards.Sum(card => card.AmountOfCards).ToString()
            };
        }
    }
}
