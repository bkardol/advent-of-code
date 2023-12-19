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

        internal int GetSumOfAllPossibleRanges()
        {
            var sum = 0;
            Stack<(int xMin, int xMax, int mMin, int mMax, int aMin, int aMax, int sMin, int sMax, string workflow)> ratingsRanges = new();
            ratingsRanges.Push((1, 4000, 1, 4000, 1, 4000, 1, 4000, "in"));
            var ranges = ratingsRanges.Pop();
            while (ranges != default)
            {
                var workflow = workflows[ranges.workflow];
                foreach(var range in workflow.Process(ranges)
                if (result == "A")
                {
                    sum += maxRatings.Values.Sum();
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
                ranges = ratingsRanges.Pop();
            }
            return sum;
        }
    }
}
