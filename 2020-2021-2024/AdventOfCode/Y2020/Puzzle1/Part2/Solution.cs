using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle1.Part2
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
                    foreach (var c in numbers.Except(new List<int> { a, b }))
                    {
                        if (a + b + c == 2020)
                        {
                            Console.WriteLine(a * b * c);
                            return;
                        }
                    }
                }
            }
        }
    }
}