using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2015.Puzzle1.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var directions = File.ReadAllLines(Helper.GetInputFilePath(this)).First();

            var floor = 0;

            foreach (var character in directions)
            {
                switch (character)
                {
                    case '(': floor++; break;
                    case ')': floor--; break;
                }
            }

            Console.WriteLine(floor);
        }
    }
}