using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2021.Puzzle1.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var depths = File.ReadAllLines(Helper.GetInputFilePath(this)).Select(int.Parse).ToArray();

            var increases = depths
                .Select((d, i) =>
                    (i < depths.Length - 1) ?
                        ((d < depths[i + 1]) ? 1 : 0) : 0)
                .Sum();

            Console.WriteLine(increases);
        }
    }
}