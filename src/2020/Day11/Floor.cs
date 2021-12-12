namespace Day11
{
    using System.Diagnostics;
    using Common.Matrix;

    [DebuggerDisplay("{Value}")]
    internal class Floor : Cell<Floor, bool?>
    {
        public bool HasSeat => Value != null;
        public bool IsSeatOccupied => Value == true;

        public void Occupy() => Value = true;
        public void Empty() => Value = false;
    }
}
