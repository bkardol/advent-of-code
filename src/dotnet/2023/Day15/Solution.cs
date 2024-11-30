namespace Day15
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<string[]>
    {
        public override string[] ParseInput(string[] lines) => lines[0].Split(',', StringSplitOptions.RemoveEmptyEntries);

        public override string[] Part1()
        {
            long total = Input.Sum(value => value.Aggregate(0, (total, character) => GetHashValue(total, character)));
            return new string[]
            {
                total.ToString()
            };
        }

        public override string[] Part2()
        {
            Hashtable boxes = new();
            foreach (var value in Input)
            {
                var containsDash = value.Contains("-");
                var splitted = value.Split(new char[] { '-', '=' });
                var label = splitted[0];
                var hashValue = label.Aggregate(0, (total, character) => GetHashValue(total, character));

                if (containsDash)
                {
                    if (boxes[hashValue] is List<Lens> box)
                    {
                        box.RemoveAll(lens => lens.Label == label);
                    }
                }
                else
                {
                    var focalLength = int.Parse(splitted[1]);
                    if (boxes[hashValue] is not List<Lens> box)
                    {
                        box = new List<Lens>();
                        boxes.Add(hashValue, box);
                    }
                    else
                    {
                        var lens = box.FirstOrDefault(value => value.Label == label);
                        if (lens != null)
                        {
                            lens.FocalLength = focalLength;
                            continue;
                        }
                    }
                    box.Add(new Lens(label, focalLength));
                }
            }

            int total = 0;
            foreach(int hashKey in boxes.Keys)
            {
                var box = boxes[hashKey] as List<Lens>;
                for(int i = 0; i < box.Count; i++)
                {
                    total += (hashKey + 1) * (i + 1) * box[i].FocalLength;
                }
            }

            return new string[]
            {
                total.ToString(),
            };
        }

        private static int GetHashValue(int currentValue, char character)
        {
            int asciiValue = character.GetHashCode();
            currentValue += asciiValue;
            currentValue *= 17;
            return currentValue % 256;
        }
    }
}
