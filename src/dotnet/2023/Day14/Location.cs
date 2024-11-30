namespace Day14
{
    using System;
    using Common.Matrix;

    internal class Location : Cell<Location, LocationType>
    {
        public void SlideRock(Func<Location, Location> nextLocation)
        {
            if (this.Value == LocationType.RoundedRock)
            {
                var newSpot = this;
                while (nextLocation(newSpot) != null && nextLocation(newSpot).Value == LocationType.EmptySpace)
                {
                    newSpot = nextLocation(newSpot);
                }
                if (newSpot != this)
                {
                    this.SwapPosition(newSpot);
                }
            }
        }
        public void SlideRockNorth() => SlideRock((location) => location.Top);
        public void SlideRockEast() => SlideRock((location) => location.Right);
        public void SlideRockSouth() => SlideRock((location) => location.Bottom);
        public void SlideRockWest() => SlideRock((location) => location.Left);

        private void SwapPosition(Location location) => (location.Value, this.Value) = (this.Value, location.Value);
    }
}
