namespace Day_19
{
    using System;
    using System.Collections.Generic;

    internal class Workflow
    {
        private readonly string name;
        private readonly Rule[] rules;

        public Workflow(string name, Rule[] rules)
        {
            this.name = name;
            this.rules = rules;
        }

        internal string Process(IDictionary<char, int> ratings)
        {
            foreach (var rule in rules)
            {
                if (rule.ValueFrom == default(char))
                {
                    return rule.ProcessValue(0);
                }
                var outcome = rule.ProcessValue(ratings[rule.ValueFrom]);
                if (!string.IsNullOrEmpty(outcome))
                {
                    return outcome;
                }
            }
            throw new InvalidOperationException("Workflow incorrect.");
        }

        internal void Process(Stack<RatingRanges> toProcess, RatingRanges current)
        {
            var ratingRangesDict = current.GetDictionary();
            foreach (var rule in rules)
            {
                if(rule.ProcessValue(toProcess, ratingRangesDict))
                {
                    break;
                }
            }
        }
    }
}
