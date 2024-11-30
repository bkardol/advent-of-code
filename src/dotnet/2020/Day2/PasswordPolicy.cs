namespace Day2
{
    using System.Linq;
    using Common.String;

    internal class PasswordPolicy
    {
        public readonly bool IsValidOldPolicy;
        public readonly bool IsValidNewPolicy;

        public PasswordPolicy(int firstNumber, int secondNumber, char letter, string password)
        {
            int letterOccurs = password.Where(c => c == letter).Count();
            IsValidOldPolicy = letterOccurs >= firstNumber && letterOccurs <= secondNumber;

            IsValidNewPolicy = password.Contains(letter, firstNumber - 1) ^ password.Contains(letter, secondNumber - 1);
        }
    }
}
