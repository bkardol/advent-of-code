namespace Day4
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Card
    {
        private readonly int id;
        private readonly int[] winningNumbers;
        private readonly int[] cardNumbers;

        public Card(int id, int[] winningNumbers, int[] cardNumbers)
        {
            this.id = id;
            this.winningNumbers = winningNumbers;
            this.cardNumbers = cardNumbers;
        }

        public int Points
        {
            get
            {
                var set = new HashSet<int>(cardNumbers);
                set.IntersectWith(winningNumbers);
                var points = set.Aggregate(0, (prev, curr) => prev == 0 ? 1 : prev * 2);
                return points;
            }
        }

        public int AmountOfWinningNumbers
        {
            get
            {
                var set = new HashSet<int>(cardNumbers);
                set.IntersectWith(winningNumbers);
                return set.Count;
            }
        }

        public int AmountOfCards { get; private set; } = 1;

        public void AddCopies(int amount) => AmountOfCards += amount;
    }
}
