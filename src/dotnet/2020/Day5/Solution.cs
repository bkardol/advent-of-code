namespace Day5
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    internal class Solution : PuzzleSolution<string[]>
    {
        private const int NumberOfRows = 128;
        private const int NumberOfSeats = 8;

        public override string[] ParseInput(string[] lines) => lines.ToArray();

        public override string[] Part1()
        {
            var maxSeatId = GetBoardingPassSeatIds().Max();

            return new string[]
            {
                $"The highest seat ID on a boarding pass is {maxSeatId}"
            };
        }

        public override string[] Part2()
        {
            var seatIds = GetBoardingPassSeatIds();
            int seatIdStart = seatIds[0];
            int freeSeatId = seatIds.Where((s,i) => s != i + seatIdStart).First() - 1;

            return new string[]
            {
                $"{freeSeatId} is the ID of the seat"
            };
        }

        private int[] GetBoardingPassSeatIds()
        {
            return Input.Select(boardingPass =>
            {
                var rows = Enumerable.Range(0, NumberOfRows);
                for (int i = 0; i < 7; ++i)
                {
                    rows = boardingPass[i] == 'F' ? rows.Take(rows.Count() / 2) : rows.TakeLast(rows.Count() / 2);
                }

                var seats = Enumerable.Range(0, NumberOfSeats);
                for (int i = 7; i < 10; ++i)
                {
                    seats = boardingPass[i] == 'L' ? seats.Take(seats.Count() / 2) : seats.TakeLast(seats.Count() / 2);
                }

                int row = rows.ElementAt(0);
                int seat = seats.ElementAt(0);
                int seatId = row * NumberOfSeats + seat;
                return seatId;
            }).OrderBy(seatId => seatId).ToArray();
        }
    }
}
