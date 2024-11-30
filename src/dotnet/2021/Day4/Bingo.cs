namespace Day4
{
    using System.Linq;

    public class Bingo
    {
        private readonly int[] numbersToDraw;
        private readonly BingoCard[] bingoCards;

        public BingoCard FirstWinningCard { get; private set; }
        public BingoCard LastWinningCard { get; private set; }

        public Bingo(int[] numbersToDraw, BingoCard[] bingoCards)
        {
            this.numbersToDraw = numbersToDraw;
            this.bingoCards = bingoCards;
        }

        public int DrawUntilFirstWin()
        {
            foreach (int number in numbersToDraw)
            {
                foreach (var card in bingoCards)
                {
                    if (card.MarkAndCheckForWin(number))
                    {
                        FirstWinningCard = card;
                        return number;
                    }
                }
            }
            return 0;
        }

        public int DrawUntilLastWin()
        {
            foreach (int number in numbersToDraw)
            {
                foreach (var card in bingoCards)
                {
                    if (card.MarkAndCheckForWin(number) && bingoCards.All(c => c.HasWon))
                    {
                        LastWinningCard = card;
                        return number;
                    }
                }
            }
            return 0;
        }
    }
}
