using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2024.Puzzle1.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();

            Console.WriteLine(lines[0]);
        }
    }
}