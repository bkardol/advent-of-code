namespace Day11
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using Common.IEnumerable;

    internal class Solution : PuzzleSolution<Floor[][]>
    {
        public override Floor[][] ParseInput(string[] lines) => lines.ToNullableBoolMatrix<Floor>('#', 'L', true);

        public override string[] Part1()
        {
            int occupiedSeats = GetSeatsEndUpOccupied(false, 4);
            return new string[]
            {
                $"{occupiedSeats} seats end up occupied"
            };
        }

        public override string[] Part2()
        {
            int occupiedSeats = GetSeatsEndUpOccupied(true, 5);
            return new string[]
            {
                $"{occupiedSeats} seats end up occupied"
            };
        }

        private int GetSeatsEndUpOccupied(bool firstSeatOnSight, int maxAmountOfOccupiedSeats)
        {
            IEnumerable<Floor> seatsToOccupy;
            IEnumerable<Floor> seatsToEmpty;
            do
            {
                seatsToOccupy = GetSeatsToOccupy(firstSeatOnSight);
                seatsToEmpty = GetSeatsToEmpty(firstSeatOnSight, maxAmountOfOccupiedSeats);
                foreach (var seat in seatsToOccupy)
                {
                    seat.Occupy();
                }
                foreach (var seat in seatsToEmpty)
                {
                    seat.Empty();
                }
            }
            while (seatsToOccupy.Any() || seatsToEmpty.Any());

            return Input.Sum(floors => floors.Count(floor => floor.IsSeatOccupied));
        }

        private static IEnumerable<Floor> GetAdjacentSeats(Floor floor, bool firstSeatOnSight) => firstSeatOnSight ? floor.FindAdjacent(f => f.HasSeat) : floor.GetAdjacent().Where(f => f.HasSeat);
        private IEnumerable<Floor> GetSeats(Func<Floor, bool> predicate) => Input.SelectMany((floors, j) => floors.Where((floor, i) => floor.HasSeat && predicate(floor))).ToArray();
        private IEnumerable<Floor> GetSeatsToOccupy(bool firstSeatOnSight) => GetSeats(floor => !floor.IsSeatOccupied && GetAdjacentSeats(floor, firstSeatOnSight).All(f => !f.IsSeatOccupied));
        private IEnumerable<Floor> GetSeatsToEmpty(bool firstSeatOnSight, int maxAmountOfAdjacentOccupiedSeats) => GetSeats(floor => floor.IsSeatOccupied && GetAdjacentSeats(floor, firstSeatOnSight).Count(f => f.IsSeatOccupied) >= maxAmountOfAdjacentOccupiedSeats);
    }
}
