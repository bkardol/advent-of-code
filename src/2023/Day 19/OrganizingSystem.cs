namespace Day_19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class OrganizingSystem
    {
        private readonly IDictionary<string, Workflow> workflows;
        private readonly IDictionary<char, int>[] ratings;

        public OrganizingSystem(IDictionary<string, Workflow> workflows, IDictionary<char, int>[] ratings)
        {
            this.workflows = workflows;
            this.ratings = ratings;
        }

        internal int GetSumOfAllAcceptedRatings()
        {
            var sum = 0;
            foreach (var r in ratings)
            {
                var workflow = workflows["in"];
                while (true)
                {
                    var result = workflow.Process(r);
                    if (result == "A")
                    {
                        sum += r.Values.Sum();
                        break;
                    }
                    else if (result == "R")
                    {
                        break;
                    }
                    else
                    {
                        workflow = workflows[result];
                    }
                }
            }
            return sum;
        }

        internal long GetNumberOfPossibleRanges()
        {
            Stack<RatingRanges> matches = new();
            Stack<RatingRanges> ratingsRanges = new();
            ratingsRanges.Push(new RatingRanges(1, 4000, 1, 4000, 1, 4000, 1, 4000, "in"));
            while (ratingsRanges.TryPop(out RatingRanges ratingRanges))
            {
                switch (ratingRanges.Workflow)
                {
                    case "A":
                        matches.Push(ratingRanges);
                        break;
                    case "R":
                        break;
                    default:
                        workflows[ratingRanges.Workflow].Process(ratingsRanges, ratingRanges);
                        break;
                }
            }
            return matches.Aggregate((long)0, (sum, match) => sum + match.GetNumberOfPossibleRanges() );
        }
    }
}
