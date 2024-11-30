namespace Day12
{
    internal class Instruction
    {
        public char Action { get; }
        public int Value { get; }

        public Instruction(char action, int value)
        {
            this.Action = action;
            this.Value = value;
        }
    }
}
