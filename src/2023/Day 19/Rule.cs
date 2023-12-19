namespace Day_19
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class Rule
    {
        public char ValueFrom { get; }
        private readonly char op;
        private readonly int value;
        private readonly string nextRule;

        public Rule(char valueFrom, char op, int value, string nextRule)
        {
            this.ValueFrom = valueFrom;
            this.op = op;
            this.value = value;
            this.nextRule = nextRule;
        }

        public Rule(string otherwise)
        {
            nextRule = otherwise;
        }

        public string? ProcessValue(int value)
        {
            string? nextRule = this.nextRule;
            switch (op)
            {
                case '>':
                    nextRule = value > this.value ? this.nextRule : null;
                    break;
                case '<':
                    nextRule = value < this.value ? this.nextRule : null;
                    break;
            }
            return nextRule;
        }

        public (string? nextRule, int min, int max) ProcessValue(int min, int max)
        {
            string? nextRule = this.nextRule;
            switch (op)
            {
                case '>':
                    if (min > this.value)
                    {
                        // Batch match
                        nextRule = this.nextRule;
                    }
                    else if (max > this.value)
                    {
                        // Partial match
                        min = this.value + 1;
                    }
                    else
                    {
                        nextRule = null;
                    }
                    break;
                case '<':
                    if (max < this.value)
                    {
                        // Batch match
                        nextRule = this.nextRule;
                    }
                    else if (min < this.value)
                    {
                        // Partial match
                        max = this.value - 1;
                    }
                    else
                    {
                        nextRule = null;
                    }
                    break;
            }
            return (nextRule, min, max);
        }
    }
}
