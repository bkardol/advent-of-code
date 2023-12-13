namespace Day13
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Common.Matrix;

    internal class Location : Cell<Location, bool>, IEqualityComparer<Location>
    {
        public void FixSmudge() => this.Value = !this.Value;
        public bool Equals(Location x, Location y) => x.Value.Equals(y.Value);
        public int GetHashCode([DisallowNull] Location obj) => obj.Value.GetHashCode();
    }
}
