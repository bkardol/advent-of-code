namespace Common
{
    public abstract class PuzzleSolution
    {
        public abstract string[] Part1();
        public abstract string[] Part2();
        internal abstract void SetInput(int part);
    }

    public abstract class PuzzleSolution<TParsed> : PuzzleSolution
    {
        protected int Part { get; private set; }
        protected TParsed Input { get; private set; }

        internal override void SetInput(int part)
        {
            Part = part;
            Input = ParseInput(
#if DEBUG
                InputService.GetExample()
#else
                InputService.GetInput()
#endif
                );
        }

        public abstract TParsed ParseInput(string[] lines);
    }
}
