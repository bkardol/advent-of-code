namespace Day25
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<Component[]>
    {
        private static readonly char[] componentSeperator = [':', ' '];
        private static readonly IDictionary<string, string> GroupConnections = new Dictionary<string, string>
        {
#if DEBUG
            ["jqt"] = "nvd",
            ["bvb"] = "cmg",
            ["hfx"] = "pzl"
#else
            ["jpn"] = "vgf",
            ["mnl"] = "nmz",
            ["txm"] = "fdb"
#endif
        };

        public override Component[] ParseInput(string[] lines)
        {
            var componentLinks = lines.Select<string, (Component component, string[] connected)>(line =>
            {
                var splitted = line.Split(componentSeperator, StringSplitOptions.RemoveEmptyEntries);
                return (new Component(splitted[0]), splitted.Skip(1).ToArray());
            }).ToArray();

            var components = componentLinks.Select(c => c.component).ToDictionary(c => c.Id, c => c);
            foreach (var (component, connected) in componentLinks)
            {
                component.Connect(connected.Select(c =>
                {
                    if (!components.TryGetValue(c, out var matchingComponent))
                    {
                        components.Add(c, matchingComponent = new Component(c));
                    }
                    return matchingComponent;
                }).ToArray());
            }

            return components.Values.ToArray();
        }

        public override string[] Part1()
        {
            string dotGraph = Input.Aggregate("digraph {" + Environment.NewLine, (dot, component) => dot + component.ToDotNotation()) + "}";
            File.WriteAllText("output.dot", dotGraph);
            Console.WriteLine("dot graph outputted to output.dot");
            Console.WriteLine("dot -Tsvg \"C:\\Users\\bkard\\Personal Projects\\advent-of-code\\src\\2023\\Day25\\bin\\Release\\net8.0\\output.dot\" > \"C:\\Users\\bkard\\Personal Projects\\advent-of-code\\src\\2023\\Day25\\bin\\Release\\net8.0\\output.svg\"");

            HashSet<Component> group1 = new();
            HashSet<Component> group2 = new();
            Queue<Component> queue = new();

            foreach (var componentId in GroupConnections.Keys)
            {
                var component = Input.First(c => c.Id == componentId);
                group1.Add(component);
                queue.Enqueue(component);
            }

            while (queue.TryDequeue(out var component))
            {
                foreach (var c in component.ConnectedTo)
                {
                    if (!GroupConnections.ContainsKey(c.Id) &&
                        !GroupConnections.Values.Contains(c.Id) &&
                        !group1.Contains(c))
                    {
                        group1.Add(c);
                        queue.Enqueue(c);
                    }
                }
                foreach (var c in Input.Where(c => c.ConnectedTo.Contains(component)))
                {
                    if (!GroupConnections.ContainsKey(c.Id) && 
                        !GroupConnections.Values.Contains(c.Id) && 
                        !group1.Contains(c))
                    {
                        group1.Add(c);
                        queue.Enqueue(c);
                    }
                }
            }

            foreach (var componentId in GroupConnections.Values)
            {
                var component = Input.First(c => c.Id == componentId);
                group2.Add(component);
                queue.Enqueue(component);
            }

            while (queue.TryDequeue(out var component))
            {
                foreach (var c in component.ConnectedTo)
                {
                    if (!GroupConnections.ContainsKey(c.Id) &&
                        !GroupConnections.Values.Contains(c.Id) &&
                        !group2.Contains(c))
                    {
                        group2.Add(c);
                        queue.Enqueue(c);
                    }
                }
                foreach (var c in Input.Where(c => c.ConnectedTo.Contains(component)))
                {
                    if (!GroupConnections.ContainsKey(c.Id) &&
                        !GroupConnections.Values.Contains(c.Id) &&
                        !group2.Contains(c))
                    {
                        group2.Add(c);
                        queue.Enqueue(c);
                    }
                }
            }

            return [
                "Total amount of components should be: " + Input.Length,
                "Group 1 has " + group1.Count + " components.",
                "Group 2 has " + group2.Count + " components.",
                "Making a total of " + (group1.Count + group2.Count) + " components.",
                "Answer of part 1 is: " + group1.Count * group2.Count
            ];
        }

        public override string[] Part2()
        {
            return [];
        }
    }
}
