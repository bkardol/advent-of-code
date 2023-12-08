namespace Common.Math
{
    public static class CommonMath
    {
        /// <summary>
        /// Gets the Least Common Multiple amongst two numbers
        /// With other words: it returns the smallest numbers that is divisible by both a & b.
        /// For example: for 21 & 30 this will result in 210 as this is the first number that is divisible by 21 and 30.
        /// </summary>
        public static long LeastCommonMultiple(long a, long b)
        {
            return (a / GreatestCommonFactor(a, b)) * b;
        }

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
    }
}
