namespace Day2
{
    internal record CommandTotal(int Aim, int Depth);

    internal class Command
    {
        private readonly Direction direction;
        private readonly int amount;

        public int HorizontalValue { get; private set; }
        public int DepthValue { get; private set; }

        public Command(Direction direction, int amount)
        {
            this.direction = direction;
            this.amount = amount;
            HorizontalValue = direction == Direction.Forward ? amount : 0;
            DepthValue = direction == Direction.Down ? amount : direction == Direction.Up ? -amount : 0;
        }

        public CommandTotal CalculateTotal(CommandTotal previousTotal) =>
            new(direction == Direction.Forward ? previousTotal.Aim : (previousTotal.Aim + DepthValue), previousTotal.Depth + (direction == Direction.Forward ? (previousTotal.Aim * amount) : 0));
    }
}
