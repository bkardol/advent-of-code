namespace Common
{
    public abstract class Puzzle
    {
        public abstract string[] Part1();
        public abstract string[] Part2();
    }

    public abstract class Puzzle<TParsed> : Puzzle
    {
        protected TParsed[] Input { get; private set; }

        public Puzzle()
        {
            Input = ParseInput(InputService.GetInput());
        }

        public abstract TParsed[] ParseInput(string[] lines);
    }
}
