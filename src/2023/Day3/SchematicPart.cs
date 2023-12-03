namespace Day3
{
    using System.Diagnostics;
    using System.Linq;
    using Common.Matrix;

    [DebuggerDisplay("{Value}")]
    internal class SchematicPart : Cell<SchematicPart, int>
    {
        public bool IsNumber { get => this.Value > PartConstants.Nothing; }
        public bool IsSymbol { get => this.Value <= PartConstants.Symbol; }
        public bool IsGear { get => this.Value == PartConstants.Gear; }

        public int PartNumber
        {
            get
            {
                if (!IsNumber)
                {
                    return 0;
                }

                if (this.Left?.IsNumber ?? false)
                {
                    return 0;
                }

                string val = "";
                var partOfNumber = this;
                bool hasAnySymbol = false;
                while (partOfNumber?.IsNumber ?? false)
                {
                    val = $"{val}{partOfNumber.Value}";
                    hasAnySymbol = hasAnySymbol || partOfNumber.HasAnyAdjacentSymbol();
                    partOfNumber = partOfNumber.Right;
                }
                return hasAnySymbol ? int.Parse(val) : 0;
            }
        }

        public int PartNumberFromStart
        {
            get
            {
                if (!IsNumber)
                {
                    return 0;
                }

                var part = this;
                while (part.Left?.IsNumber ?? false)
                {
                    part = part.Left;
                }
                return part.PartNumber;
            }
        }

        public int GearRatio
        {
            get
            {
                if (!this.IsGear)
                {
                    return 0;
                }

                var adjacentGearNumbers = this.GetAdjacent().Select(part => part.PartNumberFromStart).Distinct().Where(part => part != 0).ToArray();
                if (adjacentGearNumbers.Length != 2)
                {
                    return 0;
                }
                var gearRatio = adjacentGearNumbers.Aggregate(1, (curr, next) => curr * next);
                return gearRatio;
            }
        }

        private bool HasAnyAdjacentSymbol() => this.GetAdjacent().Any(part => part.IsSymbol);
    }
}
