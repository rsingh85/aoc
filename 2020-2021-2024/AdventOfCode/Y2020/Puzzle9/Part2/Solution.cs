using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle9.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this)).Select(long.Parse).ToList();

            const int PreambleLength = 25;
            long invalidNumber = -1;

            for (var i = PreambleLength; i < input.Count; i++)
            {
                var currentNumber = input[i];

                if (!IsSumOfTwoNumbersOfAList(currentNumber, input.GetRange(i - PreambleLength, PreambleLength)))
                {
                    invalidNumber = currentNumber;
                    break;
                }
            }

            var listOfNumbersThatSumToInvalidNumber = ListOfNumbersThatSumToNumber(invalidNumber, input);

            var orderedList = listOfNumbersThatSumToInvalidNumber.OrderBy(n => n);

            var sumOfHighestAndLowest = (long)orderedList.First() + orderedList.Last();

            Console.WriteLine(sumOfHighestAndLowest);
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

        private List<long> ListOfNumbersThatSumToNumber(long summedNumber, List<long> numbers)
        {
            var result = new List<long>();
            var contiguousListFound = false;
            var startIndex = -1;

            while (!contiguousListFound)
            {
                startIndex++;
                long currentSum = 0;
                result = new List<long>();

                for (var i = startIndex; i < numbers.Count; i++)
                {
                    currentSum += numbers[i];
                    result.Add(numbers[i]);

                    if (currentSum == summedNumber)
                    {
                        contiguousListFound = true;
                        break;
                    }
                    else if (currentSum > summedNumber)
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}