using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle13.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(@"Y2020\Puzzle13\Part1\Input.txt");
            var earliestTimeStamp = int.Parse(input[0]);
            var busIdsAndFrequencies = input[1].Split(',').Where(i => i != "x").Select(int.Parse).OrderBy(i => i);

            var earliestBusFound = false;

            for (var i = earliestTimeStamp; !earliestBusFound; i++)
            {
                foreach (var busIdAndFrequency in busIdsAndFrequencies)
                {
                    if (i % busIdAndFrequency == 0)
                    {
                        var minutesToWait = i - earliestTimeStamp;
                        var answer = busIdAndFrequency * minutesToWait;
                        Console.WriteLine(answer);
                        earliestBusFound = true;
                        break;
                    }
                }
            }
        }
    }
}