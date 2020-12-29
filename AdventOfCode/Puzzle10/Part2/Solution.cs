using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Puzzle10.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var adapters =
                File.ReadAllLines(@"Puzzle10\Part2\Input.txt")
                    .Select(int.Parse)
                    .OrderBy(j => j)
                    .ToList();

            adapters.Insert(0, 0);
            adapters.Add(adapters.Max() + 3);

            Console.WriteLine(CountPaths(adapters, 0));
        }

        private Dictionary<int, long> subPathCache = new Dictionary<int, long>();

        private long CountPaths(List<int> adapters, int currentIndex)
        {
            // base case - if we have aready counted this subpath then return its count
            if (subPathCache.ContainsKey(currentIndex))
            {
                return subPathCache[currentIndex];
            }

            // base case - reached last adapter
            if (currentIndex == adapters.Count() - 1)
            {
                return 1;
            }

            long countOfPaths = 0;

            // at most there can be three adapters from the current index that are
            // compatible with the current adapter, so count their paths individually
            for (var i = 1; i <= 3 && (currentIndex + i) < adapters.Count(); i++)
            {
                // check if "this" adapter meets the condition of being within 3 jolts of the 
                // current one
                if (adapters[currentIndex + i] - adapters[currentIndex] <= 3)
                {
                    countOfPaths += CountPaths(adapters, currentIndex + i);
                }
            }

            // cache result of sub path for when we come across this index again
            subPathCache[currentIndex] = countOfPaths;

            return countOfPaths;
        }
    }
}