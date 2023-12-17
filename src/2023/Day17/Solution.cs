namespace Day17
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Timers;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Node[][]>
    {
        public override Node[][] ParseInput(string[] lines) => lines.ToIntMatrix<Node>();

        public override string[] Part1()
        {
            var input = Input;
            var startNode = input[0][0];
            startNode.VisitPart1(Direction.Bottom, new List<Node> { }, 0, 0);
            var endNode = input[input.Length - 1][input[0].Length - 1];

            var pathDict = new Dictionary<Node, int>();

            var nextOptions = new PriorityQueue<(Node, Direction, List<Node>, int, int), int>();
            nextOptions.Enqueue((startNode.Bottom, Direction.Top, new List<Node> { startNode.Bottom }, startNode.Bottom.Value, 1), 0);
            nextOptions.Enqueue((startNode.Right, Direction.Left, new List<Node> { startNode.Right }, startNode.Right.Value, 1), 0);

            var current = this;
            var result = 0;
            while (nextOptions.Count > 0)
            {
                var toVisit = nextOptions.Dequeue();
                foreach (var newOptions in toVisit.Item1.VisitPart1(toVisit.Item2, toVisit.Item3, toVisit.Item4, toVisit.Item5))
                {
                    nextOptions.Enqueue(newOptions, newOptions.Item4);
                }

                if (toVisit.Item1 == endNode)
                {
                    foreach (var node in toVisit.Item3)
                    {
                        node.Character = 'X';
                    }
                    File.WriteAllLines("output.aoc", input.Select(i => i.Aggregate("", (total, next) => $"{total}{next.Character}")));
                    result = toVisit.Item4;
                    break;
                }
            }

            return new string[]
            {
                result.ToString()
            };
        }

        public override string[] Part2()
        {
            var input = Input;
            var startNode = input[0][0];
            startNode.VisitPart2(Direction.Bottom, new List<Node> { }, 0, 0);
            var endNode = input[input.Length - 1][input[0].Length - 1];

            var pathDict = new Dictionary<Node, int>();

            var nextOptions = new PriorityQueue<(Node, Direction, List<Node>, int, int), int>();
            nextOptions.Enqueue((startNode.Bottom, Direction.Top, new List<Node> { startNode.Bottom }, startNode.Bottom.Value, 1), 0);
            nextOptions.Enqueue((startNode.Right, Direction.Left, new List<Node> { startNode.Right }, startNode.Right.Value, 1), 0);

            var current = this;
            var result = 0;
            while (nextOptions.Count > 0)
            {
                var toVisit = nextOptions.Dequeue();
                foreach (var newOptions in toVisit.Item1.VisitPart2(toVisit.Item2, toVisit.Item3, toVisit.Item4, toVisit.Item5))
                {
                    nextOptions.Enqueue(newOptions, newOptions.Item4);
                }

                if (toVisit.Item1 == endNode)
                {
                    foreach (var node in toVisit.Item3)
                    {
                        node.Character = 'X';
                    }
                    File.WriteAllLines("output.aoc", input.Select(i => i.Aggregate("", (total, next) => $"{total}{next.Character}")));
                    result = toVisit.Item4;
                    break;
                }
            }

            return new string[]
            {
                result.ToString()
            };
        }
    }
}
