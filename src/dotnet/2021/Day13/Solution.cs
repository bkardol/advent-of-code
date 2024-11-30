namespace Day13
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    public record Fold(bool alongY, int value);
    public record Manual(bool[][] paper, Fold[] folds);

    internal class Solution : PuzzleSolution<Manual>
    {
        public override Manual ParseInput(string[] lines)
        {
            var paper = new List<List<bool>>();
            var folds = new List<Fold>();
            foreach (var line in lines)
            {
                if (line.StartsWith("fold"))
                {
                    var splitted = line.Split(new char[] { ' ', '=' });
                    folds.Add(new Fold(splitted[^2] == "y", Convert.ToInt32(splitted[^1])));
                }
                else if(!string.IsNullOrEmpty(line))
                {
                    var xy = line.Split(',');
                    int x = Convert.ToInt32(xy[0]);
                    int y = Convert.ToInt32(xy[1]);
                    paper.AddRange(Enumerable.Range(0, Math.Max(0, y - paper.Count + 1)).Select(i => new List<bool>()));
                    paper[y].AddRange(Enumerable.Range(0, Math.Max(0, x - paper[y].Count + 1)).Select(i => false));
                    paper[y][x] = true;
                }
            }

            int maxDots = paper.Max(d => d.Count);
            foreach (var dotsRow in paper)
            {
                dotsRow.AddRange(Enumerable.Range(0, maxDots - dotsRow.Count).Select(i => false));
            }

            return new Manual(paper.Select(d => d.ToArray()).ToArray(), folds.ToArray());
        }

        public override string[] Part1()
        {
            int dotsVisible = FoldPaper(Input.paper, Input.folds[0]).Sum(dots => dots.Count(d => d));

            return new string[]
            {
                $"{dotsVisible} dots are visible after one fold"
            };
        }

        public override string[] Part2()
        {
            var paper = Input.folds.Aggregate(Input.paper, (paper, fold) => FoldPaper(paper, fold));

            PrintPaper(paper);

            return new string[]
            {
                "^^^^^ The code is printed above this line ^^^^^"
            };
        }

        private static T YX<T>(Fold fold, T y, T x) => fold.alongY ? y : x;

        private static bool[][] FoldPaper(bool[][] paper, Fold fold)
        {
            for (int y = YX(fold, fold.value, 0); y < paper.Length; ++y)
            {
                for (int x = YX(fold, 0, fold.value); x < paper[y].Length; ++x)
                {
                    if (paper[y][x])
                    {
                        paper[YX(fold, fold.value - (y - fold.value), y)][YX(fold, x, fold.value - (x - fold.value))] = paper[y][x];
                    }
                }
            }
            return YX(fold, paper.Take(fold.value).ToArray(), paper.Select(dots => dots.Take(fold.value).ToArray()).ToArray());
        }

        private static void PrintPaper(bool[][] paper)
        {
            foreach (var dots in paper)
            {
                foreach (var dot in dots)
                {
                    Console.Write(dot ? '#' : '.');
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}