namespace Common
{
    public abstract class PuzzleSolution
    {
        public abstract string[] Part1();
        public abstract string[] Part2();
    }

    public abstract class PuzzleSolution<TParsed> : PuzzleSolution
    {
        protected TParsed[] Input { get; private set; }

        public PuzzleSolution()
        {
            Input = ParseInput(InputService.GetInput());
        }

        public abstract TParsed[] ParseInput(string[] lines);
    }
}
