namespace Day_19
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
                    // Fallback rule
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

        internal (int xMin, int xMax, int mMin, int mMax, int aMin, int aMax, int sMin, int sMax, string workflow)[] ProcessRanges(IDictionary<char, int> ratings)
        {
            foreach (var rule in rules)
            {
                if (rule.ValueFrom == default(char))
                {
                    // Fallback rule
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
    }
}
