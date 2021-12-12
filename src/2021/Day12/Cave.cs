namespace Day12
{
    using System.Collections.Generic;

    public class Cave
    {
        public bool IsStartCave { get; }
        public bool IsEndCave { get; }
        public bool IsBigCave { get; }
        public string Name { get; }
        public HashSet<Cave> ConnectedCaves { get; } = new HashSet<Cave>();

        public Cave(string name)
        {
            Name = name;
            IsStartCave = name == "start";
            IsEndCave = name == "end";
            IsBigCave = Name == Name.ToUpper();
        }
    }
}
