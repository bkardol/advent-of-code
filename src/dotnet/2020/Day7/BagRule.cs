namespace Day7
{
    using System.Collections.Generic;

    internal record ContainBags(int Amount, string Bag);

    internal class BagRule
    {
        public string SubjectBag { get; }
        public IEnumerable<ContainBags> ContainsBags { get; }

        public BagRule(string subjectBag, IEnumerable<ContainBags> containsBags)
        {
            SubjectBag = subjectBag;
            ContainsBags = containsBags;
        }
    }
}
