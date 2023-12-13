namespace Day13
{
    using System.Collections.Generic;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Location[][][]>
    {
        private readonly Dictionary<int, (string type, int index)> originalReflections = new();

        public override Location[][][] ParseInput(string[] lines)
        {
            var valleys = new List<Location[][]>();
            var valley = new List<string>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    valleys.Add(valley.ToBoolMatrix<Location>('#'));
                    valley = new List<string>();
                }
                else
                {
                    valley.Add(line);
                }
            }
            valleys.Add(valley.ToBoolMatrix<Location>('#'));
            return valleys.ToArray();
        }

        public override string[] Part1()
        {
            long totalReflectionValue = 0;
            for (int valleyIndex = 0; valleyIndex < Input.Length; valleyIndex++)
            {
                var valley = Input[valleyIndex];
                var reflectionValue = 0;
                // ROWS
                for (int i = 1; i < valley.Length; i++)
                {
                    int matchIndex = -1;
                    for (int j = 0; i - j - 1 >= 0 && i + j < valley.Length; j++)
                    {
                        var firstRow = valley[i - j - 1];
                        var secondRow = valley[i + j];
                        bool matches = true;
                        for (int k = 0; k < firstRow.Length; k++)
                        {
                            if (firstRow[k].Value != secondRow[k].Value)
                            {
                                matches = false;
                                break;
                            }
                        }
                        if (matches)
                        {
                            if (matchIndex == -1)
                                matchIndex = i;
                        }
                        else
                        {
                            matchIndex = -1;
                            break;
                        }
                    }
                    if (matchIndex != -1)
                    {
                        originalReflections.Add(valleyIndex, ("ROW", matchIndex));
                        reflectionValue = (matchIndex * 100);
                        break;
                    }
                }
                if (reflectionValue == 0)
                {
                    // COLS
                    for (int i = 1; i < valley[0].Length; i++)
                    {
                        int matchIndex = -1;
                        for (int j = 0; i - j - 1 >= 0 && i + j < valley[0].Length; j++)
                        {
                            var firstColumn = new List<Location>();
                            var secondColumn = new List<Location>();
                            for (int k = 0; k < valley.Length; k++)
                            {
                                firstColumn.Add(valley[k][i - j - 1]);
                                secondColumn.Add(valley[k][i + j]);
                            }

                            bool matches = true;
                            for (int k = 0; k < firstColumn.Count; k++)
                            {
                                if (firstColumn[k].Value != secondColumn[k].Value)
                                {
                                    matches = false;
                                    break;
                                }
                            }
                            if (matches)
                            {
                                if (matchIndex == -1)
                                    matchIndex = i;
                            }
                            else
                            {
                                matchIndex = -1;
                                break;
                            }
                        }
                        if (matchIndex != -1)
                        {
                            originalReflections.Add(valleyIndex, ("COLUMN", matchIndex));
                            reflectionValue = matchIndex;
                            break;
                        }
                    }
                }
                totalReflectionValue += reflectionValue;
            }
            return new string[]
            {
                totalReflectionValue.ToString()
            };
        }

        public override string[] Part2()
        {
            long totalReflectionValue = 0;

            for (int valleyIndex = 0; valleyIndex < Input.Length; valleyIndex++)
            {
                int matchIndex = -1;
                var reflectionValue = 0;
                var valley = Input[valleyIndex];
                for (int fixSmudgeI = 0; fixSmudgeI < Input.Length; fixSmudgeI++)
                {
                    var fixSmudgeRow = valley[fixSmudgeI];
                    for (int fixSmudgeJ = 0; fixSmudgeJ < fixSmudgeRow.Length; fixSmudgeJ++)
                    {
                        var fixSmudgeLocation = fixSmudgeRow[fixSmudgeJ];
                        fixSmudgeLocation.FixSmudge();

                        // ROWS
                        for (int i = 1; i < valley.Length; i++)
                        {
                            for (int j = 0; i - j - 1 >= 0 && i + j < valley.Length; j++)
                            {
                                var firstRow = valley[i - j - 1];
                                var secondRow = valley[i + j];
                                bool matches = true;
                                for (int k = 0; k < firstRow.Length; k++)
                                {
                                    if (firstRow[k].Value != secondRow[k].Value)
                                    {
                                        matches = false;
                                        break;
                                    }
                                }
                                if (matches)
                                {
                                    if (matchIndex == -1)
                                        matchIndex = i;
                                }
                                else
                                {
                                    matchIndex = -1;
                                    break;
                                }
                            }
                            if (matchIndex != -1)
                            {
                                var originalReflection = originalReflections[valleyIndex];
                                if (originalReflection.type == "ROW" && originalReflection.index == matchIndex)
                                {
                                    matchIndex = -1;
                                }
                                else
                                {
                                    reflectionValue = (matchIndex * 100);
                                    break;
                                }
                            }
                        }
                        if (reflectionValue == 0)
                        {
                            // COLS
                            for (int i = 1; i < valley[0].Length; i++)
                            {
                                for (int j = 0; i - j - 1 >= 0 && i + j < valley[0].Length; j++)
                                {
                                    var firstColumn = new List<Location>();
                                    var secondColumn = new List<Location>();
                                    for (int k = 0; k < valley.Length; k++)
                                    {
                                        firstColumn.Add(valley[k][i - j - 1]);
                                        secondColumn.Add(valley[k][i + j]);
                                    }

                                    bool matches = true;
                                    for (int k = 0; k < firstColumn.Count; k++)
                                    {
                                        if (firstColumn[k].Value != secondColumn[k].Value)
                                        {
                                            matches = false;
                                            break;
                                        }
                                    }
                                    if (matches)
                                    {
                                        if (matchIndex == -1)
                                            matchIndex = i;
                                    }
                                    else
                                    {
                                        matchIndex = -1;
                                        break;
                                    }
                                }
                                if (matchIndex != -1)
                                {
                                    var originalReflection = originalReflections[valleyIndex];
                                    if (originalReflection.type == "COLUMN" && originalReflection.index == matchIndex)
                                    {
                                        matchIndex = -1;
                                    }
                                    else
                                    {
                                        reflectionValue = matchIndex;
                                        break;
                                    }
                                }
                            }
                        }

                        if (reflectionValue > 0)
                        {
                            totalReflectionValue += reflectionValue;
                            break;
                        }
                        else
                        {
                            fixSmudgeLocation.FixSmudge();
                        }
                    }
                    if (reflectionValue > 0)
                    {
                        break;
                    }
                }
            }
            return new string[]
            {
                totalReflectionValue.ToString()
            };
        }
    }
}
