namespace Day2
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Common;

    internal class Solution : PuzzleSolution<Game[]>
    {
        public override Game[] ParseInput(string[] lines) => lines.Select(line =>
        {
            var gameAndSets = line.Split(": ");
            int gameNumber = int.Parse(Regex.Replace(gameAndSets[0], @"[^0-9]", ""));
            var sets = gameAndSets[1].Split("; ").Select(set => set.Split(", ").Select(cubeString => cubeString.Split(' ')).Select(cubeData => Tuple.Create(cubeData[1], int.Parse(cubeData[0]))).ToArray()).ToArray();
            return new Game(gameNumber, sets);
        }).ToArray();

        public override string[] Part1()
        {
            var sumOfMatchingGames = Input.Where(game => game.HasAtLeastCubes("red", 12) && game.HasAtLeastCubes("green", 13) && game.HasAtLeastCubes("blue", 14)).Sum(game => game.Number);
            return new string[]
            {
                @"The sum of game numbers with only 12 red cubes, 13 green cubes, and 14 blue cubes is: " + sumOfMatchingGames.ToString()
            };
        }

        public override string[] Part2()
        {
            var powerOfGames = Input.Sum(game => game.PowerOfGame());
            return new string[]
            {
                "The sum of the power of the minimum viable sets is: " + powerOfGames.ToString()
            };
        }
    }
}
