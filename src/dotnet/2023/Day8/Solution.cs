namespace Day8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<Map>
    {
        public override Map ParseInput(string[] lines)
        {
            var instructions = lines[0];
            var nodes = lines.Skip(2).Select(line => line.Split(new[] { " = (", ", ", ")" }, StringSplitOptions.RemoveEmptyEntries));
            var dict = new Dictionary<string, Node>();
            foreach (var node in nodes)
            {
                dict.Add(node[0], new Node(node[0], node[1], node[2]));
            }
            foreach(var node in dict.Values)
            {
                var left = dict[node.LeftName];
                var right = dict[node.RightName];
                node.LinkNodes(left, right);
            }
            return new Map(instructions, dict.Values.ToArray());
        }
        public override string[] Part1()
        {
            var result = Input.WalkThrough();
            return new string[]
            {
                result.ToString()
            };
        }

        public override string[] Part2()
        {
            var result = Input.GhostWalkThrough();
            return new string[]
            {
                result.ToString()
            };
        }
    }
}
