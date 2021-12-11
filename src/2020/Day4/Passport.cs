namespace Day4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Passport
    {
        private static readonly string hairColorCharacters = "0123456789abcdef";
        private static readonly string[] eyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        // Yea, I know, Regex is a possibility, but let's make it more fun, shall we? :)
        private readonly Dictionary<string, Func<string, bool>> requiredFields = new()
        {
            ["byr"] = (value) => int.TryParse(value, out int birthYear) && birthYear >= 1920 && birthYear <= 2002,
            ["iyr"] = (value) => int.TryParse(value, out int issueYear) && issueYear >= 2010 && issueYear <= 2020,
            ["eyr"] = (value) => int.TryParse(value, out int expirationYear) && expirationYear >= 2020 && expirationYear <= 2030,
            ["hgt"] = (value) => value.EndsWith("cm") ? int.TryParse(value.Substring(0, value.Length - 2), out int cm) && cm >= 150 && cm <= 193 : value.EndsWith("in") && int.TryParse(value.Substring(0, value.Length - 2), out int inch) && inch >= 59 && inch <= 76,
            ["hcl"] = (value) => value[0] == '#' && value.Length == 7 && value.Substring(1).All(c => hairColorCharacters.Contains(c)),
            ["ecl"] = (value) => eyeColors.Contains(value),
            ["pid"] = (value) => value.Length == 9 && long.TryParse(value, out _)
        };
        private readonly Dictionary<string, string> fields = new();

        public bool IsValidWithoutValidations => requiredFields.Keys.All(field => fields.ContainsKey(field));
        public bool IsValidWithValidations => requiredFields.All(kv => fields.ContainsKey(kv.Key) && kv.Value(fields[kv.Key]));

        public void AddFields(Dictionary<string, string> fields)
        {
            foreach (var field in fields)
            {
                this.fields.Add(field.Key, field.Value);
            }
        }
    }
}
