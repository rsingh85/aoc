using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzle2.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var policyAndPasswords = File.ReadAllLines(@"Y2020\Puzzle2\Part2\Input.txt");
            var validPasswordCount = 0;

            foreach (var policyAndPassword in policyAndPasswords)
            {
                var match = Regex.Match(policyAndPassword, @"^(\d+)\-(\d+)\s([a-z]):\s([a-z]+)$");

                if (!match.Success)
                {
                    continue;
                }

                var firstIndex = int.Parse(match.Groups[1].Value) - 1;
                var secondIndex = int.Parse(match.Groups[2].Value) - 1;
                var letter = char.Parse(match.Groups[3].Value);
                var password = match.Groups[4].Value;

                if (password[firstIndex] == letter && password[secondIndex] != letter
                    || password[firstIndex] != letter && password[secondIndex] == letter)
                {
                    validPasswordCount++;
                }
            }
            Console.WriteLine(validPasswordCount);
        }
    }
}