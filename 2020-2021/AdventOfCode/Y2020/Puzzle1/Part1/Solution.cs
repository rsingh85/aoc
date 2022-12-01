using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle1.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var numbers = File.ReadAllLines(Helper.GetInputFilePath(this)).Select(int.Parse);

            foreach (var a in numbers)
            {
                foreach (var b in numbers.Except(new List<int> { a }))
                {
                    if (a + b == 2020)
                    {
                        Console.WriteLine(a * b);
                        return;
                    }
                }
            }
        }
    }
}