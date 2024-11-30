namespace Day13
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Common.Array;

    internal class Valley
    {
        private int originalReflectionIndex = -1;
        private ReflectionType originalReflectionType;
        private readonly Location[][] locations;

        public Valley(Location[][] locations)
        {
            this.locations = locations;
        }

        private static bool IsReflection(Location[] first, Location[] second) => Enumerable.SequenceEqual(first, second, new Location());

        private int FindReflection(ReflectionType reflectionType, int directionSize, Func<int, Location[]> directionSelector)
        {
            int reflectionIndex = -1;
            for (int i = 1; i < directionSize; i++)
            {
                for (int j = 0; i - j - 1 >= 0 && i + j < directionSize; j++)
                {
                    var first = directionSelector(i - j - 1);
                    var second = directionSelector(i + j);
                    bool isPartialReflection = IsReflection(first, second);
                    if (isPartialReflection)
                    {
                        if (reflectionIndex == -1)
                            reflectionIndex = i;
                    }
                    else
                    {
                        reflectionIndex = -1;
                        break;
                    }
                }
                if (reflectionIndex != -1)
                {
                    if (originalReflectionType == reflectionType && originalReflectionIndex == reflectionIndex)
                    {
                        reflectionIndex = -1;
                    }
                    else
                    {
                        return reflectionIndex;
                    }
                }
            }
            return reflectionIndex;
        }

        private (int index, ReflectionType type) FindReflection(bool findOriginalReflection)
        {
            if (findOriginalReflection && originalReflectionIndex != -1)
            {
                return (originalReflectionIndex, originalReflectionType);
            }

            ReflectionType reflectionType = ReflectionType.ROW;
            int reflectionIndex = FindReflection(ReflectionType.ROW, locations.Length, locationIndex => locations[locationIndex]);

            if (reflectionIndex == -1)
            {
                reflectionType = ReflectionType.COLUMN;
                reflectionIndex = FindReflection(ReflectionType.COLUMN, locations[0].Length, locationIndex => CommonArray.GetColumn(locations, locationIndex));
            }

            if (findOriginalReflection)
            {
                originalReflectionIndex = reflectionIndex;
                originalReflectionType = reflectionType;
            }
            return (reflectionIndex, reflectionType);
        }

        public int GetReflectionValue()
        {
            var (index, type) = FindReflection(true);
            return index * (type == ReflectionType.COLUMN ? 1 : 100);
        }

        public int GetFixedReflectionValue()
        {
            FindReflection(true);
            foreach(var fixSmudgeRow in locations)
            {
                foreach(var fixSmudgeLocation in fixSmudgeRow)
                {
                    fixSmudgeLocation.FixSmudge();

                    var reflection = FindReflection(false);
                    if (reflection.index != -1)
                    {
                        return reflection.index * (reflection.type == ReflectionType.COLUMN ? 1 : 100);
                    }
                    else
                    {
                        fixSmudgeLocation.FixSmudge();
                    }
                }
            }
            return 0;
        }
    }
}
