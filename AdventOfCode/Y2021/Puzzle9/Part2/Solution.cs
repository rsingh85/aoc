namespace AdventOfCode.Y2021.Puzzle9.Part2
{
    public class Solution : ISolution
    {
        private int[,] _grid;
        private int maxRows;
        private int maxCols;
        private List<string> _usedLocations = new List<string>();

        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this));

            maxRows = input.Length;
            maxCols = input.First().Length;
            _grid = new int[maxRows, maxCols];

            for (var r = 0; r < maxRows; r++)
            {
                for (var c = 0; c < maxCols; c++)
                {
                    _grid[r, c] = Convert.ToInt32(input[r][c].ToString());
                }
            }

            var basins = new List<int>();

            for (var r = 0; r < maxRows; r++)
            {
                for (var c = 0; c < maxCols; c++)
                {
                    var height = _grid[r, c];
                    var top = r == 0 ? 10 : _grid[r - 1, c];
                    var left = c == 0 ? 10 : _grid[r, c - 1];
                    var right = c == maxCols - 1 ? 10 : _grid[r, c + 1];
                    var bottom = r == maxRows - 1 ? 10 : _grid[r + 1, c];

                    if (height < top && height < left && height < right && height < bottom)
                    {
                        var topBasinSize = GetBasinSize(r - 1, c, height);
                        var leftBasinSize = GetBasinSize(r, c - 1, height);
                        var rightBasinSize = GetBasinSize(r, c + 1, height);
                        var bottomBasinSize = GetBasinSize(r + 1, c, height);

                        basins.Add(1 + topBasinSize + leftBasinSize + rightBasinSize + bottomBasinSize);
                    }
                }
            }

            basins = basins.OrderByDescending(b => b).Take(3).ToList();

            Console.WriteLine(basins[0] * basins[1] * basins[2]);
        }

        private int GetBasinSize(int r, int c, int previousHeight)
        {
            if (r < 0 || r >= maxRows || c < 0 || c >= maxCols)
            {
                return 0;
            }

            var currentHeight = _grid[r, c];

            if (currentHeight > previousHeight && currentHeight < 9 && !_usedLocations.Contains($"{r},{c}"))
            {
                _usedLocations.Add($"{r},{c}");

                var topBasinSize = GetBasinSize(r - 1, c, currentHeight);
                var leftBasinSize = GetBasinSize(r, c - 1, currentHeight);
                var rightBasinSize = GetBasinSize(r, c + 1, currentHeight);
                var bottomBasinSize = GetBasinSize(r + 1, c, currentHeight);

                return 1 + topBasinSize + leftBasinSize + rightBasinSize + bottomBasinSize;
            }

            return 0;
        }
    }
}