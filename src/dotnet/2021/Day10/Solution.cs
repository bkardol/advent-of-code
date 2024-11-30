namespace Day10
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<string[]>
    {
        private readonly int autoCompleteMultiplier = 5;
        private readonly char[] startingCharacters = new[] { '(', '[', '{', '<' };
        private readonly Dictionary<char, char> startEndCharacters = new()
        {
            ['('] = ')',
            ['['] = ']',
            ['{'] = '}',
            ['<'] = '>',
        };
        private readonly Dictionary<char, char> endStartCharacters = new()
        {
            [')'] = '(',
            [']'] = '[',
            ['}'] = '{',
            ['>'] = '<',
        };
        private readonly Dictionary<char, int> illegalCharacterScore = new()
        {
            [')'] = 3,
            [']'] = 57,
            ['}'] = 1197,
            ['>'] = 25137,
        };
        private readonly Dictionary<char, int> autoCompleteCharacterScore = new()
        {
            [')'] = 1,
            [']'] = 2,
            ['}'] = 3,
            ['>'] = 4,
        };

        public override string[] ParseInput(string[] lines) => lines.ToArray();

        public override string[] Part1()
        {
            int syntaxErrorScore = Input.Sum(line => CheckLineForErrors(line).SyntaxErrorScore);

            return new string[]
            {
                $"The total syntax error score is: {syntaxErrorScore}"
            };
        }

        public override string[] Part2()
        {
            var autoCompleteScores = Input.Select(line =>
            {
                long autoCompleteScore = 0;
                (int SyntaxErrorScore, Stack<char> RemainingCharacterStack) = CheckLineForErrors(line);
                if (SyntaxErrorScore == 0)
                {
                    foreach (char character in RemainingCharacterStack)
                    {
                        autoCompleteScore *= autoCompleteMultiplier;
                        autoCompleteScore += autoCompleteCharacterScore[startEndCharacters[character]];
                    }
                }
                return autoCompleteScore;
            }).Where(score => score > 0).OrderBy(score => score).ToArray();
            var middleAutoCompleteScore = autoCompleteScores[autoCompleteScores.Length / 2];

            return new string[]
            {
                $"The middle auto complete score is: {middleAutoCompleteScore}"
            };
        }

        private (int SyntaxErrorScore, Stack<char> RemainingCharacterStack) CheckLineForErrors(string line)
        {
            int syntaxErrorScore = 0;
            var characterStack = new Stack<char>();
            foreach (char character in line)
            {
                if (startingCharacters.Contains(character))
                {
                    characterStack.Push(character);
                }
                else
                {
                    char startCharacter = endStartCharacters[character];
                    char endCharacter = characterStack.Pop();
                    if (endCharacter != startCharacter)
                    {
                        syntaxErrorScore += illegalCharacterScore[character];
                    }
                }
            }
            return (SyntaxErrorScore: syntaxErrorScore, RemainingCharacterStack: characterStack );
        }
    }
}
