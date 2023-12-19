namespace Day_19
{
    using System.Collections.Generic;
    using System.Linq;

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

        public bool ProcessValue(Stack<RatingRanges> toProcess, Dictionary<char, (int min, int max)> ratings)
        {
            if (ValueFrom == default)
            {
                toProcess.Push(new RatingRanges(ratings, nextRule));
                return true;
            }

            var (min, max) = ratings[ValueFrom];
            var dictCopy = ratings.ToDictionary(entry => entry.Key, entry => entry.Value);
            switch (op)
            {
                case '>':
                    if (min > this.value)
                    {
                        toProcess.Push(new RatingRanges(dictCopy, nextRule));
                        return true;
                    }
                    else if (max > this.value)
                    {
                        ratings[ValueFrom] = (min, this.value);
                        dictCopy[ValueFrom] = (this.value + 1, max);
                        toProcess.Push(new RatingRanges(dictCopy, nextRule));
                    }
                    break;
                case '<':
                    if (max < this.value)
                    {
                        toProcess.Push(new RatingRanges(dictCopy, nextRule));
                        return true;
                    }
                    else if (min < this.value)
                    {
                        ratings[ValueFrom] = (this.value, max);
                        dictCopy[ValueFrom] = (min, this.value - 1);
                        toProcess.Push(new RatingRanges(dictCopy, nextRule));
                    }
                    break;
            }
            return false;
        }
    }
}
