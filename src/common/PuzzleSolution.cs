namespace Common
{
    public abstract class PuzzleSolution
    {
        public abstract string[] Part1();
        public abstract string[] Part2();
        internal abstract void SetInput();
    }

    public abstract class PuzzleSolution<TParsed> : PuzzleSolution
    {
        protected TParsed Input { get; private set; }

        internal override void SetInput()
        {
            Input = ParseInput(
#if DEBUG
                InputService.GetInput()
#else
                InputService.GetInput()
#endif
                );
        }

        public abstract TParsed ParseInput(string[] lines);
    }
}
