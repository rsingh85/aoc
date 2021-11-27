using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle9.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(@"Y2020\Puzzle9\Part1\Input.txt").Select(long.Parse).ToList();

            const int PreambleLength = 25;

            for (var i = PreambleLength; i < input.Count; i++)
            {
                var currentNumber = input[i];

                if (!IsSumOfTwoNumbersOfAList(currentNumber, input.GetRange(i - PreambleLength, PreambleLength)))
                {
                    Console.WriteLine(currentNumber);
                    break;
                }
            }
        }

        private bool IsSumOfTwoNumbersOfAList(long summedNumber, List<long> numbers)
        {
            foreach (var a in numbers)
            {
                foreach (var b in numbers)
                {
                    if (a != b && ((long)a + b) == summedNumber)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}