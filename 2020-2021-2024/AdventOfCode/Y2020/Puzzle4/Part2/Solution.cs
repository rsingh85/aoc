using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020.Puzzle4.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var passports = File.ReadAllText(@"Y2020\Puzzle4\Part2\Input.txt")
                                .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            
            var validPassports = 0;
            
            foreach (var passport in passports)
            {
                var parsedPassport = passport.Replace("\r\n", " ");
                
                if (IsValid(parsedPassport))
                {
                    validPassports++;
                }
            }

            Console.WriteLine(validPassports);
        }

        private bool IsValid(string passport)
        {
            var passportFieldsDictionary = new Dictionary<string, string>();

            foreach (var fieldValue in passport.Split(' '))
            {
                var fieldValueSplit = fieldValue.Split(':');
                var field = fieldValueSplit[0];
                var value = fieldValueSplit[1];
                passportFieldsDictionary.Add(field, value);
            }

            var fieldExistenceValidation = false;
            var containsAllFieldsButCidField = passportFieldsDictionary.ContainsKey("byr")
                                               && passportFieldsDictionary.ContainsKey("iyr")
                                               && passportFieldsDictionary.ContainsKey("eyr")
                                               && passportFieldsDictionary.ContainsKey("hgt")
                                               && passportFieldsDictionary.ContainsKey("hcl")
                                               && passportFieldsDictionary.ContainsKey("ecl")
                                               && passportFieldsDictionary.ContainsKey("pid");

            var containsCidField = passportFieldsDictionary.ContainsKey("cid");

            if ((containsAllFieldsButCidField && containsCidField) || containsAllFieldsButCidField)
            {
                fieldExistenceValidation = true;
            }

            if (!fieldExistenceValidation)
            {
                return false;
            }

            // field validation
            var isByrValid = ValidateByrField(passportFieldsDictionary["byr"]);
            var isIyrValid = ValidateIyrField(passportFieldsDictionary["iyr"]);
            var isEyrValid = ValidateEyrField(passportFieldsDictionary["eyr"]);
            var isHgtValid = ValidateHgtField(passportFieldsDictionary["hgt"]);
            var isHclValid = ValidateHclField(passportFieldsDictionary["hcl"]);
            var isEclValid = ValidateEclField(passportFieldsDictionary["ecl"]);
            var isPidValid = ValidatePidField(passportFieldsDictionary["pid"]);

            if (containsCidField)
            {
                var isCidValid = ValidateCidField(passportFieldsDictionary["cid"]);
                return isByrValid && isIyrValid && isEyrValid && isHgtValid && isHclValid && isEclValid && isPidValid && isCidValid;
            }

            return isByrValid && isIyrValid && isEyrValid && isHgtValid && isHclValid && isEclValid && isPidValid;
        }

        private bool ValidateByrField(string value)
        {
            if (int.TryParse(value, out var valueAsInt))
            {
                return value.Length == 4 && (valueAsInt >= 1920 && valueAsInt <= 2002);
            }
            return false;
        }

        private bool ValidateIyrField(string value)
        {
            if (int.TryParse(value, out var valueAsInt))
            {
                return value.Length == 4 && (valueAsInt >= 2010 && valueAsInt <= 2020);
            }
            return false;
        }

        private bool ValidateEyrField(string value)
        {
            if (int.TryParse(value, out var valueAsInt))
            {
                return value.Length == 4 && (valueAsInt >= 2020 && valueAsInt <= 2030);
            }
            return false;
        }

        private bool ValidateHgtField(string value)
        {
            if (Regex.IsMatch(value, @"^\d+(cm|in)$"))
            {
                var number = int.Parse(Regex.Match(value, @"^\d+").Value);
                var unit = Regex.Match(value, "[a-z]+$").Value;
                if (unit == "cm")
                {
                    return number >= 150 && number <= 193;
                }
                if (unit == "in")
                {
                    return number >= 59 && number <= 76;
                }
            }
            return false;
        }

        private bool ValidateHclField(string value)
        {
            return Regex.IsMatch(value, "^#[a-f0-9]{6}$");
        }

        private bool ValidateEclField(string value)
        {
            return new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(value);
        }

        private bool ValidatePidField(string value)
        {
            return Regex.IsMatch(value, "^[0-9]{9}$");
        }

        private bool ValidateCidField(string value)
        {
            return true;
        }
    }
}