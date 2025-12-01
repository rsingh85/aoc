using System;
using System.IO;

namespace AdventOfCode.Y2020.Puzzle4.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var passports = File.ReadAllText(@"Y2020\Puzzle4\Part1\Input.txt")
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
            var containsAllFieldsButCidField = passport.Contains("byr:")
                                         && passport.Contains("iyr:")
                                         && passport.Contains("eyr:")
                                         && passport.Contains("hgt:")
                                         && passport.Contains("hcl:")
                                         && passport.Contains("ecl:")
                                         && passport.Contains("pid:");

            var containsCidField = passport.Contains("cid:");

            if ((containsAllFieldsButCidField && containsCidField) || containsAllFieldsButCidField)
            {
                return true;
            }

            return false;
        }
    }
}