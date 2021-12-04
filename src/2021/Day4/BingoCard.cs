namespace Day4
{
    using System.Linq;

    public class BingoCard
    {
        private readonly BingoField[][] fields;

        public bool HasWon { get; private set; }

        public BingoCard(int[][] numbers)
        {
            this.fields = numbers.Select(r => r.Select(f => new BingoField(f)).ToArray()).ToArray();
        }

        public bool MarkAndCheckForWin(int number)
        {
            for (int i = 0; i < fields.Length; ++i)
            {
                var column = fields[i];
                for (int j = 0; j < fields[i].Length; ++j)
                {
                    var field = fields[i][j];
                    if (field.Mark(number))
                    {
                        if (!HasWon)
                        {
                            HasWon = fields.Any(r => r.All(f => f.IsMarked)) ||
                                Enumerable.Range(0, fields[i].Length).All(k => fields[k][j].IsMarked);
                        }
                        return HasWon;
                    }
                }
            }
            return false;
        }

        public int GetSumOfAllUnmarked() => fields.SelectMany(r => r.Where(f => !f.IsMarked)).Sum(f => f.Number);
    }
}
