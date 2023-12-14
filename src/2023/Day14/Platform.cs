namespace Day14
{
    using System;
    using System.Linq;
    using Common.Array;

    internal class Platform
    {
        public readonly Location[][] locations;

        public Platform(Location[][] locations)
        {
            this.locations = locations;
        }

        public string CreateKey() => string.Join(';', locations.SelectMany(row => row.Select(l => $"{l.Value}")));

        public void TiltToSlideNorth()
        {
            foreach (var row in locations)
            {
                foreach (var location in row)
                {
                    location.SlideRockNorth();
                }
            }
        }

        public void TiltToSlideWest()
        {
            for (int j = 0; j < locations[0].Length; j++)
            {
                var column = CommonArray.GetColumn(locations, j);
                foreach (var location in column)
                {
                    location.SlideRockWest();
                }
            }
        }

        public void TiltToSlideSouth()
        {
            foreach (var row in locations.Reverse())
            {
                foreach (var location in row)
                {
                    location.SlideRockSouth();
                }
            }
        }

        public void TiltToSlideEast()
        {
            for (int j = locations[0].Length - 1; j >= 0; j--)
            {
                var column = CommonArray.GetColumn(locations, j);
                foreach (var location in column)
                {
                    location.SlideRockEast();
                }
            }
        }

        public int GetTotalLoad() => locations.Reverse().Select((row, i) => row.Count(l => l.Value == LocationType.RoundedRock) * (i + 1)).Sum();
    }
}
