using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2021.Puzzle3.Part2
{
    public class Solution : ISolution
    {
        private enum Rating { OxygenGenerator, CO2Scrubber }

        public void Run()
        {
            var binaries = File.ReadAllLines(Helper.GetInputFilePath(this));

            var lifeSupportRating =
                FindRating(binaries, 0, Rating.OxygenGenerator) *
                FindRating(binaries, 0, Rating.CO2Scrubber);

            Console.WriteLine(lifeSupportRating);
        }

        private int FindRating(IEnumerable<string> candidates, int index, Rating rating)
        {
            if (candidates.Count() == 1)
            {
                return Convert.ToInt32(candidates.First(), 2);
            }

            var zeroes = candidates.Select(b => b[index]).Count(b => b == '0');
            var ones = candidates.Select(b => b[index]).Count(b => b == '1');

            if (zeroes > ones)
            {
                return FindRating(candidates.Where(b => (rating == Rating.OxygenGenerator) ? b[index] == '0' : b[index] == '1').ToList(), ++index, rating);
            }
            else if (ones > zeroes)
            {
                return FindRating(candidates.Where(b => (rating == Rating.OxygenGenerator) ? b[index] == '1' : b[index] == '0').ToList(), ++index, rating);
            }
            else
            {
                return FindRating(candidates.Where(b => (rating == Rating.OxygenGenerator) ? b[index] == '1' : b[index] == '0').ToList(), ++index, rating);
            }
        }
    }
}