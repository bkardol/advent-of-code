namespace Day11
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerDisplay("{EnergyLevel}")]
    internal class Octopus
    {
        public int EnergyLevel { get; private set; }

        private Octopus Left;
        private Octopus Right;
        private Octopus Top;
        private Octopus Bottom;

        private Octopus TopLeft;
        private Octopus TopRight;
        private Octopus BottomRight;
        private Octopus BottomLeft;

        public Octopus(int energyLevel) => this.EnergyLevel = energyLevel;

        public void SetLeft(Octopus left) => Left = left;
        public void SetRight(Octopus right) => Right = right;
        public void SetTop(Octopus top)
        {
            Top = top;
            TopLeft = top.Left;
            TopRight = top.Right;
            if (TopLeft != null)
                TopLeft.BottomRight = this;
            if (TopRight != null)
                TopRight.BottomLeft = this;
        }
        public void SetBottom(Octopus bottom)
        {
            Bottom = bottom;
            BottomLeft = bottom.Left;
            BottomRight = bottom.Right;
        }

        public int IncreaseEnergyLevel() => ++EnergyLevel;
        public int ResetEnergyLevel() => EnergyLevel = 0;

        public IEnumerable<Octopus> GetAdjacentOctopuses(bool includeDiagonal = false) =>
            new Octopus[] { Left, Right, Top, Bottom }
            .Concat(includeDiagonal ? new[] { TopLeft, TopRight, BottomLeft, BottomRight } : Array.Empty<Octopus>())
            .Where(l => l != null);
    }
}
