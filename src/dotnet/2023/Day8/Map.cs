namespace Day8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.MathExtensions;

    internal class Map
    {
        private readonly string instructions;
        private readonly Node[] nodes;

        public Map(string instructions, Node[] nodes)
        {
            this.instructions = instructions;
            this.nodes = nodes;
        }

        internal int WalkThrough()
        {
            var node = nodes.FirstOrDefault(n => n.Name.EndsWith("A"));
            int count = 0;
            for (int i = 0; node.Name.EndsWith("Z"); i++, count++)
            {
                if (i == instructions.Length)
                    i = 0;

                var instruction = instructions[i];
                if (instruction == 'L')
                    node = node.Left;
                else
                    node = node.Right;
            }
            return count;
        }

        internal long GhostWalkThrough()
        {
            var nds = nodes.Where(n => n.Name.EndsWith("A")).ToArray();
            Console.WriteLine("Starting with " + nds.Length + " nodes");

            var nodeCounts = new Dictionary<string, long>();
            long count = 0;
            for (int i = 0; nds.Any(n => !n.Name.EndsWith("Z")); i++, count++)
            {
                if (i == instructions.Length)
                    i = 0;

                nds = nds.Select(n =>
                {
                    var instruction = instructions[i];

                    var containsCount = nodeCounts.ContainsKey(n.Name);
                    if (containsCount) 
                        return n;

                    var nextNode = instruction == 'L' ? n.Left : n.Right;
                    if (nextNode.Name.EndsWith("Z"))
                    {
                        nodeCounts.Add(nextNode.Name, count + 1);
                        Console.WriteLine("Done with 1 node");
                        return nextNode;
                    }
                    return nextNode;
                }).ToArray();
            }
            return nodeCounts.Values.Aggregate(CommonMath.LeastCommonMultiple);
        }
    }
}
