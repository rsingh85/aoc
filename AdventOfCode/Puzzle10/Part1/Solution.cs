using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Puzzle10.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var adapterOutputJoltages =
                File.ReadAllLines(@"Puzzle10\Part1\Input.txt")
                    .Select(int.Parse)
                    .OrderBy(j => j)
                    .ToList();

            adapterOutputJoltages.Add(adapterOutputJoltages.Max() + 3);

            var currentOutletRating = 0;

            // key = difference, value = tally
            var differenceCounts = new Dictionary<int, int>();

            for (var i = 0; i < adapterOutputJoltages.Count(); i++)
            {
                var currentAdapterOutputJoltage = adapterOutputJoltages[i];

                var difference = currentAdapterOutputJoltage - currentOutletRating;

                if (difference <= 3)
                {
                    if (differenceCounts.ContainsKey(difference))
                    {
                        differenceCounts[difference] = differenceCounts[difference] + 1;
                    }
                    else
                    {
                        differenceCounts[difference] = 1;
                    }

                    currentOutletRating = currentAdapterOutputJoltage;
                }
            }

            Console.WriteLine(differenceCounts[1] * differenceCounts[3]);
        }
    }
}