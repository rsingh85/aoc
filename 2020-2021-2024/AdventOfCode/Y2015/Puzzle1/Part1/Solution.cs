using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2015.Puzzle1.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var floor = File.ReadAllLines(Helper.GetInputFilePath(this))
                .First()
                .AsQueryable()
                .Select(d => d == '(' ? 1 : -1)
                .Sum();

            Console.WriteLine(floor);
        }
    }
}