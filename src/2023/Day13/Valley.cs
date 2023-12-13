namespace Day13
{
    using Common.Matrix;

    internal class Location : Cell<Location, bool>
    {
        public void FixSmudge() => this.Value = !this.Value;
    }
}
