namespace Day11
{
    using System.Diagnostics;
    using Common.Matrix;

    [DebuggerDisplay("{EnergyLevel}")]
    internal class Octopus : Cell<Octopus, int>
    {
        public int IncreaseEnergyLevel() => ++Value;
        public int ResetEnergyLevel() => Value = 0;
    }
}
