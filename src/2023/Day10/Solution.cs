namespace Day10
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Pipe[][]>
    {
        public override Pipe[][] ParseInput(string[] lines)
        {
            if (Part == 1)
            {
                return lines.ToMatrix<Pipe, PipeType>(pipe => (PipeType)pipe);
            }
            else
            {
                var newLines = lines.ToList();
                for (int i = 0; i < lines.Length; i++)
                {
                    newLines[i * 2] = lines[i].Aggregate(" ", (total, next) => $"{total}{next} ") + " ";
                    newLines.Insert(i * 2 + 1, lines[i].Aggregate(" ", (total, next) => $"{total}  ") + " ");
                }
                newLines.Insert(0, lines[0].Aggregate(" ", (total, next) => $"{total}  ") + " ");
                return newLines.ToMatrix<Pipe, PipeType>(pipe => (PipeType)pipe);
            }
        }

        public override string[] Part1()
        {
            var start = Input.SelectMany(pipes => pipes).FirstOrDefault(pipe => pipe.Value == PipeType.Start);
            int counter = 1;

            var previousPipe = start;
#if DEBUG
            var currentPipe = start.Right;
#else
            var currentPipe = start.Top;
#endif
            Console.Write((char)start.Value);
            while (currentPipe != start)
            {
                var temp = currentPipe;
                Console.Write((char)currentPipe.Value);
                switch (currentPipe.Value)
                {
                    case PipeType.Vertical:
                        currentPipe = previousPipe == currentPipe.Bottom ? currentPipe.Top : currentPipe.Bottom;
                        break;
                    case PipeType.Horizontal:
                        currentPipe = previousPipe == currentPipe.Left ? currentPipe.Right : currentPipe.Left;
                        break;
                    case PipeType.TopRight:
                        currentPipe = previousPipe == currentPipe.Left ? currentPipe.Bottom : currentPipe.Left;
                        break;
                    case PipeType.TopLeft:
                        currentPipe = previousPipe == currentPipe.Right ? currentPipe.Bottom : currentPipe.Right;
                        break;
                    case PipeType.BottomLeft:
                        currentPipe = previousPipe == currentPipe.Right ? currentPipe.Top : currentPipe.Right;
                        break;
                    case PipeType.BottomRight:
                        currentPipe = previousPipe == currentPipe.Left ? currentPipe.Top : currentPipe.Left;
                        break;
                }
                previousPipe = temp;
                ++counter;
            }

            return new string[]
            {
                (counter / 2).ToString()
            };
        }

        public override string[] Part2()
        {
            var start = Input.SelectMany(pipes => pipes).FirstOrDefault(pipe => pipe.Value == PipeType.Start);
            var pipes = new List<Pipe> { start };

            var previousPipe = start;
#if DEBUG
            var currentPipe = start.Right;
#else
            var currentPipe = start.Top;
#endif
            Console.Write((char)start.Value);
            while (currentPipe != start)
            {
                for (int i = 0; i < 2; i++)
                {
                    var temp = currentPipe;
                    Console.Write((char)currentPipe.Value);
                    switch (currentPipe.Value)
                    {
                        case PipeType.Vertical:
                            currentPipe = previousPipe == currentPipe.Bottom ? currentPipe.Top : currentPipe.Bottom;
                            break;
                        case PipeType.Horizontal:
                            currentPipe = previousPipe == currentPipe.Left ? currentPipe.Right : currentPipe.Left;
                            break;
                        case PipeType.TopRight:
                            currentPipe = previousPipe == currentPipe.Left ? currentPipe.Bottom : currentPipe.Left;
                            break;
                        case PipeType.TopLeft:
                            currentPipe = previousPipe == currentPipe.Right ? currentPipe.Bottom : currentPipe.Right;
                            break;
                        case PipeType.BottomLeft:
                            currentPipe = previousPipe == currentPipe.Right ? currentPipe.Top : currentPipe.Right;
                            break;
                        case PipeType.BottomRight:
                            currentPipe = previousPipe == currentPipe.Left ? currentPipe.Top : currentPipe.Left;
                            break;
                        case PipeType.Unknown:
                            currentPipe = previousPipe == currentPipe.Left ? currentPipe.Right :
                                previousPipe == currentPipe.Right ? currentPipe.Left :
                                previousPipe == currentPipe.Bottom ? currentPipe.Top :
                                currentPipe.Bottom;
                            break;
                    }
                    previousPipe = temp;
                    pipes.Add(previousPipe);
                }
            }

            foreach (var pipeline in Input)
            {
                foreach (var pipe in pipeline)
                {
                    if (!pipes.Contains(pipe))
                    {
                        pipe.UpdateValue(PipeType.Unknown);
                    }
                    else if (pipe.Value == PipeType.Unknown)
                    {
                        pipe.UpdateValue(PipeType.Enclosure);
                    }
                }
            }

            Input[0][0].FillToBottomRight(PipeType.Outside);
            Input[0][Input[0].Length - 1].FillToBottomLeft(PipeType.Outside);
            Input[Input.Length - 1][Input[0].Length - 1].FillToTopLeft(PipeType.Outside);
            Input[Input.Length - 1][0].FillToTopRight(PipeType.Outside);
            Console.WriteLine("CORNER FILLING DONE");
            bool anyUpdated = true;
            while (anyUpdated)
            {
                anyUpdated = false;
                foreach (var pipeline in Input)
                {
                    foreach (var pipe in pipeline)
                    {
                        if (pipe.Value == PipeType.Unknown && pipe.GetAdjacent().Any(p => p.Value == PipeType.Outside))
                        {
                            anyUpdated = true;
                            pipe.UpdateValue(PipeType.Outside);
                        }
                    }
                }
            }
            Console.WriteLine("REMAINING FILLING DONE");

            foreach (var pipeline in Input)
            {
                foreach (var pipe in pipeline.Where(pipe => pipe.Value == PipeType.Unknown))
                {
                    pipe.UpdateValue(PipeType.Inside);
                }
            }

            var shrinked = new List<List<Pipe>>();
            for (int i = 1; i < Input.Length; i += 2)
            {
                var pipeline = Input[i];
                var shrinkedPipeline = new List<Pipe>();
                for (int j = 1; j < pipeline.Length; j += 2)
                {
                    shrinkedPipeline.Add(pipeline[j]);
                }
                shrinked.Add(shrinkedPipeline);
            }
            File.WriteAllLines("output-expanded.aoc", Input.Select(i => i.Aggregate("", (total, next) => $"{total}{(char)next.Value}")));
            File.WriteAllLines("output.aoc", shrinked.Select(i => i.Aggregate("", (total, next) => $"{total}{(char)next.Value}")));

            return new string[]
            {
                shrinked.SelectMany(pipes => pipes).Count(pipe => pipe.Value == PipeType.Inside).ToString()
            };
        }
    }
}
