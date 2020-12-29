using System;
using System.IO;

namespace AdventOfCode.Puzzle3.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var grid = File.ReadAllLines(@"Puzzle3\Part1\Input.txt");
            var colIndex = 3;
            var treesEncountered = 0;

            for (var rowIndex = 1; rowIndex < grid.Length; rowIndex++)
            {
                var square = grid[rowIndex][colIndex];

                if (square == '#')
                {
                    treesEncountered++;
                }

                colIndex = (colIndex + 3) % grid[0].Length;
            }

            Console.WriteLine(treesEncountered);
        }
    }
}