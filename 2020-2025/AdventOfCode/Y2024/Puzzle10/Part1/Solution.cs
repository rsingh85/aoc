namespace AdventOfCode.Y2024.Puzzle10.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var grid = Convert1dArrayTo2dArray(lines);

            Print(grid);

            var trailHeads = 0;
            var scoreSum = 0;

            for (var r = 0; r < grid.GetLength(0); r++)
            {
                for (var c = 0; c < grid.GetLength(1); c++)
                {
                    var current = grid[r, c];

                    if (current == '0')
                    {
                        trailHeads++;
                        
                        var score = FindPaths((r, c), (r, c), grid);

                        scoreSum += score;

                        Console.WriteLine("Trailhead #{0} ({1},{2}) score = {3}", trailHeads, r, c, score);
                    }
                }
            }

            Console.WriteLine("Total Trailhead score: {0}", scoreSum);
        }

        private static HashSet<string> uniqueTrailHeadsReachingTarget = new();

        private static int FindPaths((int r, int c) trailhead, (int r, int c) currentCoords, char[,] grid)
        {
            var current = grid[currentCoords.r, currentCoords.c];

            if (current == '9')
            {
                if (uniqueTrailHeadsReachingTarget.Add($"({currentCoords.r},{currentCoords.c})reachedFrom({trailhead.r},{trailhead.c})"))
                    return 1;

                return 0;
            }

            var result = 0;

            (int r, int c) top = (currentCoords.r - 1, currentCoords.c);
            if (InBounds(top, grid) && IsNextStep(current, grid[top.r, top.c]))
                result += FindPaths(trailhead, top, grid);

            (int r, int c) right = (currentCoords.r, currentCoords.c + 1);
            if (InBounds(right, grid) && IsNextStep(current, grid[right.r, right.c]))
                result += FindPaths(trailhead, right, grid);

            (int r, int c) down = (currentCoords.r + 1, currentCoords.c);
            if (InBounds(down, grid) && IsNextStep(current, grid[down.r, down.c]))
                result += FindPaths(trailhead, down, grid);

            (int r, int c) left = (currentCoords.r, currentCoords.c - 1);
            if (InBounds(left, grid) && IsNextStep(current, grid[left.r, left.c]))
                result += FindPaths(trailhead, left, grid);

            return result;
        }

        private static bool InBounds((int r, int c) coords, char[,] grid) =>
            coords.r >= 0 && coords.r < grid.GetLength(0) && coords.c >= 0 && coords.c < grid.GetLength(1);

        private static bool IsNextStep(char current, char next) =>
            next != '.' && int.Parse(next.ToString()) == (int.Parse(current.ToString()) + 1);

        private static char[,] Convert1dArrayTo2dArray(string[] lines)
        {
            var grid = new char[lines.Length, lines[0].Length];

            for (var r = 0; r < lines.Length; r++)
                for (var c = 0; c < lines[r].Length; c++)
                    grid[r, c] = lines[r][c];

            return grid;
        }

        private static void Print(char[,] grid)
        {
            for (var r = 0; r < grid.GetLength(0); r++)
            {
                for (var c = 0; c < grid.GetLength(1); c++)
                {
                    Console.Write(grid[r, c]);
                }
                Console.WriteLine();
            }
        }
    }
}