namespace AdventOfCode.Y2025.Puzzle4.Part2
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            const int AdjacentRollsThreshold = 4; 

            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            var grid = new char[lines.Length, lines[0].Length];
            
            for (int r = 0; r < lines.Length; r++)
                for (int c = 0; c < lines[r].Length; c++)
                    grid[r, c] = lines[r][c];

            var totalRemoved = 0;

            while (true)
            {
                var removePositions = new List<(int r, int c)>();

                for (var r = 0; r < grid.GetLength(0); r++)
                {
                    for (var c = 0; c < grid.GetLength(1); c++)
                    {
                        var current = grid[r, c];

                        if (current != '@')
                            continue;

                        var adjacents = new List<char>
                        {
                            r == 0 ? '.' : grid[r - 1, c], // top
                            r == 0 || c == grid.GetLength(1) - 1 ? '.' : grid[r - 1, c + 1], // top-right
                            c == grid.GetLength(1) - 1 ? '.' : grid[r, c + 1], // right
                            r == grid.GetLength(0) - 1 || c == grid.GetLength(1) - 1 ? '.' : grid[r + 1, c + 1], // bottom-right
                            r == grid.GetLength(0) - 1 ? '.' : grid[r + 1, c], // bottom
                            r == grid.GetLength(0) - 1 || c == 0 ? '.' : grid[r + 1, c - 1], // bottom-left
                            c == 0 ? '.' : grid[r, c - 1], // left
                            r == 0 || c == 0 ? '.' : grid[r - 1, c - 1], // top-left
                        };

                        if (adjacents.Count(a => a == '@') < AdjacentRollsThreshold)
                            removePositions.Add((r, c));
                    }
                }

                totalRemoved += removePositions.Count;

                if (!removePositions.Any())
                    break;

                foreach (var (r, c) in removePositions)
                    grid[r, c] = '.';
            }

            Console.WriteLine(totalRemoved);
        }
    }
}