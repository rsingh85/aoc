using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Puzzle15.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(@"Puzzle15\Part2\Input.txt")
                .First()
                .Split(',')
                .Select(long.Parse)
                .ToList();

            var targetTurnReached = false;
            var currentTurn = input.Count + 1;
            var targetTurn = 30000000;

            var dictionary = new Dictionary<long, List<long>>();

            // add starting numbers
            for (var i = 0; i < input.Count; i++)
            {
                dictionary.Add(input[i], new List<long>() { i + 1 });
            }

            while (!targetTurnReached)
            {
                long lastNumber = input.Last();
                long nextNumber = -1;

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
                        dictionary.Add(0, new List<long> { currentTurn });
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
                        dictionary.Add(nextNumber, new List<long> { currentTurn });
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