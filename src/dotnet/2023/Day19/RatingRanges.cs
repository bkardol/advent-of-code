namespace Day_19
{
    internal readonly struct RatingRanges(int xMin, int xMax, int mMin, int mMax, int aMin, int aMax, int sMin, int sMax, string workflow)
    {
        private readonly int xMin = xMin;
        private readonly int xMax = xMax;
        private readonly int mMin = mMin;
        private readonly int mMax = mMax;
        private readonly int aMin = aMin;
        private readonly int aMax = aMax;
        private readonly int sMin = sMin;
        private readonly int sMax = sMax;
        public string Workflow { get; } = workflow;

        public RatingRanges(Dictionary<char, (int min, int max)> dictionary, string workflow) : this(
            dictionary['x'].min,
            dictionary['x'].max,
            dictionary['m'].min,
            dictionary['m'].max,
            dictionary['a'].min,
            dictionary['a'].max,
            dictionary['s'].min,
            dictionary['s'].max,
            workflow
            )
        { }

        public readonly Dictionary<char, (int min, int max)> GetDictionary() => new()
        {
            ['x'] = (xMin, xMax),
            ['m'] = (mMin, mMax),
            ['a'] = (aMin, aMax),
            ['s'] = (sMin, sMax),
        };
        internal long GetNumberOfPossibleRanges() => (long)(xMax - xMin + 1) * (long)(mMax - mMin + 1) * (long)(aMax - aMin + 1) * (long)(sMax - sMin + 1);
    }
}