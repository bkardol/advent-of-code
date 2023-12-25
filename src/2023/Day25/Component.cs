namespace Day25
{
    using System;
    using System.Linq;

    internal class Component
    {
        public string Id { get; }
        public Component[] ConnectedTo { get; private set; } = [];

        private readonly string color;

        public Component(string id)
        {
            Id = id;
            var random = new Random();
            color = $"#{random.Next(0x1000000):X6}";
        }

        public void Connect(Component[] components) => ConnectedTo = components;
        internal string ToDotNotation() => ConnectedTo.Aggregate("", (dot, c) => dot + $"  \"{Id + ": " + color.Substring(1)}\" -> \"{c.Id + ": " + c.color.Substring(1)}\" [style=dashed,color=\"{color}\"]" + Environment.NewLine);

        public override int GetHashCode() => Id.GetHashCode();

        public override bool Equals(object? obj)
        {
            if (obj is Component component)
            {
                return this.Id.Equals(component.Id);
            }
            return base.Equals(obj);
        }
    }
}
