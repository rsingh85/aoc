using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020.Puzzle2.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var policyAndPasswords = File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution)));
            var validPasswordCount = 0;

            foreach (var policyAndPassword in policyAndPasswords)
            {
                var match = Regex.Match(policyAndPassword, @"^(\d+)\-(\d+)\s([a-z]):\s([a-z]+)$");

                if (!match.Success)
                {
                    continue;
                }

                var minLetterOccurrence = int.Parse(match.Groups[1].Value);
                var maxLetterOccurrence = int.Parse(match.Groups[2].Value);
                var letter = match.Groups[3].Value;
                var password = match.Groups[4].Value;
                var letterOccurrences = Regex.Matches(password, letter).Count;

                if (letterOccurrences >= minLetterOccurrence
                    && letterOccurrences <= maxLetterOccurrence)
                {
                    validPasswordCount++;
                }
            }
            Console.WriteLine(validPasswordCount);
        }
    }
}