namespace Day20
{
    internal class Module(string key, bool isFlipFlop, bool isConjunction, string[] destinations)
    {
        public string Key { get; } = key;
        public bool IsFlipFlop { get; } = isFlipFlop;
        public bool IsConjunction { get; } = isConjunction;
        public string[] Destinations { get; } = destinations;

        public bool IsOn { get; private set; }
        public Dictionary<string, bool> ReceivedPulses { get; private set; } = [];

        public bool? ProcessPulse(string source, bool isHighPulse)
        {
            if (Key == "roadcaster")
            {
                return isHighPulse;
            }

            if (IsFlipFlop)
            {
                if (!isHighPulse)
                {
                    IsOn = !IsOn;
                    return IsOn;
                }
            }

            if (IsConjunction)
            {
                ReceivedPulses[source] = isHighPulse;
                return !ReceivedPulses.Values.All(p => p);
            }

            return null;
        }

        internal void SetPossibleSources(string[] sources)
        {
            if (!IsConjunction)
            {
                return;
            }
            ReceivedPulses = sources.ToDictionary(s => s, s => false);
        }
    }
}
