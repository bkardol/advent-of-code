namespace Day2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Game
    {
        public readonly int Number;
        private readonly Dictionary<string, int> _cubeMaximums;

        public Game(int number, Tuple<string, int>[][] cubeSets)
        {
            this.Number = number;
            this._cubeMaximums = cubeSets.SelectMany(set => set).GroupBy(cube => cube.Item1).ToDictionary((group) => group.First().Item1, (group) => group.Max(s => s.Item2));
        }

        public bool HasAtLeastCubes(string color, int amount) => this._cubeMaximums.ContainsKey(color) ? amount >= this._cubeMaximums[color] : false;

        public int PowerOfGame() =>  this._cubeMaximums.Values.Aggregate(1, (cubes1, cubes2) => cubes1 * cubes2);
    }
}
