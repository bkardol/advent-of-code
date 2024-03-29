﻿namespace Common.MathExtensions
{
    using System;
    using System.Linq;

    public static class CommonMath
    {
        /// <summary>
        /// Gets the Least Common Multiple amongst two numbers
        /// With other words: it returns the smallest numbers that is divisible by both a & b.
        /// For example: for 21 & 30 this will result in 210 as this is the first number that is divisible by 21 and 30.
        /// </summary>
        public static long LeastCommonMultiple(long a, long b) => (a / GreatestCommonFactor(a, b)) * b;

        /// <summary>
        /// Gets the Greatest Common Factor amongst two numbers.
        /// With other words: it returns the greatest number that is divisible for both a & b.
        /// For example: for 21 & 30 this will result in 3 as both numbers are divisible by 3 and nothing higher than that.
        /// </summary>
        public static long GreatestCommonFactor(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Calculates the surface area by using the Shoelace algorithm.
        /// </summary>
        /// <param name="locations">The coordinates of each surface "corner" and locations travelled in counter-clockwise direction.</param>
        /// <returns>The size of the area defined by the passed locations.</returns>
        public static long CalculateSurfaceAreaWithShoelace((long x, long y, long distance)[] locations)
        {
            long totalSurface = 0;
            long totalDistance = 0;
            var finalLocations = locations.ToList();
            if (finalLocations[0] != finalLocations[^1])
            {
                finalLocations.Insert(0, locations[^1]);
            }
            for (int i = 0; i < finalLocations.Count - 1; i++)
            {
                totalSurface += finalLocations[i].x * finalLocations[i + 1].y - finalLocations[i + 1].x * finalLocations[i].y;
                totalDistance += finalLocations[i].distance;
            }

            return (totalDistance / 2) + Math.Abs(totalSurface / 2) + 1;
        }

        /// <summary>
        /// Get the quadratic fit for 3 outputs from a repeating (quadratic) pattern.
        /// </summary>
        /// <param name="patternFirst">First output of the quadratic pattern.</param>
        /// <param name="patternSecond">Second output of the quadratic pattern.</param>
        /// <param name="patternThird">Third output of the quadratic pattern.</param>
        /// <returns>Quadratic fit, which in turn can be used to calculate the root.</returns>
        public static (int A, int B, int C) CalculateQuadraticFit(int patternFirst, int patternSecond, int patternThird)
        {
            var c = patternFirst;
            var ba = patternSecond - c;
            var aa = patternThird - c - (2 * ba);
            var a = aa / 2;
            var b = ba - a;
            return (a, b, c);
        }

        /// <summary>
        /// Solves a quadratic equation for a pattern with the use of 3 known quadratic fits.
        /// </summary>
        /// <param name="iterations">The number of iterations of the pattern to get the result for.</param>
        /// <param name="quadraticFitA">First quadratic fit.</param>
        /// <param name="quadraticFitB">Second quadratic fit.</param>
        /// <param name="quadraticFitC">Third quadratic fit.</param>
        /// <returns>The result of the quadratic equation.</returns>
        public static long QuadraticEquation(long iterations, int quadraticFitA, int quadraticFitB, int quadraticFitC) =>
            quadraticFitA * (iterations * iterations) + (quadraticFitB * iterations) + quadraticFitC;
    }
}
