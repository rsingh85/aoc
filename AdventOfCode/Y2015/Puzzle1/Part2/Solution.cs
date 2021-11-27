using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2015.Puzzle1.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var directions = File.ReadAllLines(@"Y2015\Puzzle1\Part2\Input.txt").First();

            var floor = 0;

            for (var i = 0; i < directions.Length; i++)
            {
                var character = directions[i];

                switch (character)
                {
                    case '(': floor++; break;
                    case ')': floor--; break;
                }

                if (floor == -1)
                {
                    Console.WriteLine(i + 1);
                    break;
                }
            }

        }
    }
}