using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle15.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution)))
                .First()
                .Split(',')
                .Select(int.Parse)
                .ToList();

            var targetTurnReached = false;
            var currentTurn = input.Count + 1;
            var targetTurn = 2020;

            var dictionary = new Dictionary<int, List<int>>();

            // add starting numbers
            for (var i = 0; i < input.Count; i++)
            {
                dictionary.Add(input[i], new List<int>() { i + 1 });
            }

            while (!targetTurnReached)
            {
                var lastNumber = input.Last();
                var nextNumber = -1;

                if (dictionary.ContainsKey(lastNumber) && dictionary[lastNumber].Count == 1)
                {
                    nextNumber = 0;
                    input.Add(nextNumber);

                    if (dictionary.ContainsKey(nextNumber))
                    {
                        dictionary[0].Add(currentTurn);
                    }
                    else
                    {
                        dictionary.Add(0, new List<int> { currentTurn });
                    }
                }
                else if (dictionary.ContainsKey(lastNumber) && dictionary[lastNumber].Count > 1)
                {
                    var lastNumberTurnsList = dictionary[lastNumber];
                    nextNumber = lastNumberTurnsList[lastNumberTurnsList.Count - 1] - lastNumberTurnsList[lastNumberTurnsList.Count - 2];

                    input.Add(nextNumber);

                    if (dictionary.ContainsKey(nextNumber))
                    {
                        dictionary[nextNumber].Add(currentTurn);
                    }
                    else
                    {
                        dictionary.Add(nextNumber, new List<int> { currentTurn });
                    }
                }

                if (currentTurn == targetTurn)
                {
                    targetTurnReached = true;
                    Console.WriteLine(nextNumber);
                }

                currentTurn++;
            }
        }
    }
}