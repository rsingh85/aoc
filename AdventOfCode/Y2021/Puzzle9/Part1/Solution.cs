namespace AdventOfCode.Y2021.Puzzle9.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this));

            var maxRows = input.Length;
            var maxCols = input.First().Length;
            var grid = new int[maxRows, maxCols];

            for (var r = 0; r < maxRows; r++)
            {
                for (var c = 0; c < maxCols; c++)
                {
                    grid[r, c] = Convert.ToInt32(input[r][c].ToString());
                }
            }

            var sum = 0;

            for (var r = 0; r < maxRows; r++)
            {
                for (var c = 0; c < maxCols; c++)
                {
                    var height = grid[r, c];
                    var top = r == 0 ? 10 : grid[r - 1, c];
                    var left = c == 0 ? 10 : grid[r, c - 1];
                    var right = c == maxCols - 1 ? 10 : grid[r, c + 1];
                    var bottom = r == maxRows - 1 ? 10 : grid[r + 1, c];

                    if (height < top && height < left && height < right && height < bottom)
                    {
                        sum += height + 1;
                    }
                }
            }

            Console.WriteLine(sum);
        }
    }
}