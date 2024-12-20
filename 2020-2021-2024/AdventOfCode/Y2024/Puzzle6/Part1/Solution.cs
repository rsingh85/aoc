﻿namespace AdventOfCode.Y2024.Puzzle6.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var grid = Convert1dArrayTo2dArray(lines);

            var guardPosition = GetGuardPosition(grid);
            var guardInBounds = true;
            var distinctPositions = new HashSet<(int, int)>();

            while (guardInBounds)
            {
                distinctPositions.Add((guardPosition.r,guardPosition.c));

                var guard = grid[guardPosition.r, guardPosition.c];
                var nextGuardPosition = GetNextPosition(guardPosition.r, guardPosition.c, guard);
                
                if (nextGuardPosition.r >= 0 && nextGuardPosition.r < grid.GetLength(0) &&
                        nextGuardPosition.c >= 0 && nextGuardPosition.c < grid.GetLength(1))
                {
                    if (grid[nextGuardPosition.r, nextGuardPosition.c] == '.')
                    {
                        grid[guardPosition.r, guardPosition.c] = '.';
                        grid[nextGuardPosition.r, nextGuardPosition.c] = guard;
                        guardPosition = nextGuardPosition;
                    }
                    else
                        grid[guardPosition.r, guardPosition.c] = RotateGuard(guard);
                }
                else 
                    guardInBounds = false;       
            }

            Console.WriteLine(distinctPositions.Count());
        }

        private static char[,] Convert1dArrayTo2dArray(string[] lines)
        {
            var grid = new char[lines.Length, lines[0].Length];

            for (var r = 0; r < lines.Length; r++)
                for (var c = 0; c < lines[r].Length; c++)
                    grid[r, c] = lines[r][c];

            return grid;
        }

        private static (int r, int c) GetGuardPosition(char[,] grid)
        {
            var guardChars = new List<char>() { '^', '>', 'v', '<' };

            for (var r = 0; r < grid.GetLength(0); r++)
                for (var c = 0; c < grid.GetLength(1); c++)
                    if (guardChars.Contains(grid[r, c])) 
                        return (r, c);

            return (-1, -1);
        }

        private static (int r, int c) GetNextPosition(int r, int c, char guard) => 
            guard switch
            {
                '^' => (r - 1, c),
                '>' => (r, c + 1),
                'v' => (r + 1, c),
                '<' => (r, c - 1),
                _ => throw new Exception("Invalid guard character"),
            };

        private static char RotateGuard(char guard) =>
            guard switch
            {
                '^' => '>',
                '>' => 'v',
                'v' => '<',
                '<' => '^',
                _ => throw new Exception("Invalid guard character"),
            };
    }
}