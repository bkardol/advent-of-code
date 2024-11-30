namespace Day4
{
    internal class BingoField
    {
        public int Number { get; }

        public bool IsMarked { get; private set; }

        public BingoField(int number)
        {
            this.Number = number;
        }

        public bool Mark(int number) => Number == number && (IsMarked = true);
    }
}
