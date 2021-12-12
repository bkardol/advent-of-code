namespace Day12
{
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<IDictionary<string, Cave>>
    {
        public override Dictionary<string, Cave> ParseInput(string[] lines)
        {
            var caveInput = lines.Select(line => line.Split('-')).ToArray();
            var caveDict = caveInput.SelectMany(i => i).Distinct().ToDictionary(name => name, name => new Cave(name));
            foreach (var caveConnection in caveInput)
            {
                var firstCave = caveDict[caveConnection[0]];
                var secondCave = caveDict[caveConnection[1]];
                firstCave.ConnectedCaves.Add(secondCave);
                secondCave.ConnectedCaves.Add(firstCave);
            }
            return caveDict;
        }

        public override string[] Part1()
        {
             var (_, paths) = GoThroughCaves(new List<string>(), Input["start"], "", 1);
            return new string[]
            {
                $"There are {paths.Count()} through the cave system that visit small caves at most once"
            };
        }

        public override string[] Part2()
        {
            var (_, paths) = GoThroughCaves(new List<string>(), Input["start"], "", 2);
            return new string[]
            {
                $"There are {paths.Count()} through the cave system that visits at most one small cave twice"
            };
        }

        public (bool success, IEnumerable<string> paths) GoThroughCaves(List<string> previousCaves, Cave currentCave, string path, int maxSmallCaveVisits)
        {
            var pathResults = new List<string>();
            var isStartOrEnd = currentCave.IsStartCave || currentCave.IsEndCave;
            var alreadyVisited = previousCaves.Contains(currentCave.Name);
            var caveVisitedMultipleTimes = previousCaves.GroupBy(c => c).FirstOrDefault(c => c.Count() > 1);
            var mayVisitThisCaveAgain = maxSmallCaveVisits > 1 && (caveVisitedMultipleTimes == null || (caveVisitedMultipleTimes.Key == currentCave.Name && caveVisitedMultipleTimes.Count() < maxSmallCaveVisits));

            if (currentCave.IsBigCave ||
                (isStartOrEnd && !alreadyVisited) ||
                (!isStartOrEnd && (!alreadyVisited || mayVisitThisCaveAgain)))
            {
                if (!currentCave.IsBigCave)
                {
                    previousCaves.Add(currentCave.Name);
                }
                path = string.IsNullOrEmpty(path) ? currentCave.Name : (path + "," + currentCave.Name);

                if (currentCave.IsEndCave)
                {
                    pathResults.Add(path);
                    return (true, pathResults);
                }

                foreach (var cave in currentCave.ConnectedCaves)
                {
                    var (success, paths) = GoThroughCaves(previousCaves.ToList(), cave, path, maxSmallCaveVisits);
                    if (success)
                    {
                        pathResults.AddRange(paths);
                    }
                }
            }


            return (pathResults.Count > 0, pathResults);
        }
    }
}
