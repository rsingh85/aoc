namespace AdventOfCode.Y2021.Puzzle25.Part1
{
    public class Solution : ISolution
    {
        private char[,] _grid;
        private int _gridMaxC, _gridMaxR;

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            _gridMaxR = lines.Length;
            _gridMaxC = lines.First().Length;
            _grid = new char[_gridMaxR, _gridMaxC];

            for (var r = 0; r < _gridMaxR; r++)
            {
                for (var c = 0; c < _gridMaxC; c++)
                {
                    _grid[r, c] = lines[r][c];
                }
            }

            var steps = Process();
            Console.WriteLine(steps);
        }

        private int Process()
        {
            var steps = 0;
            var processedPoints = new HashSet<string>();

            while (true)
            {
                var moved = false;
                processedPoints.Clear();

                for (var r = 0; r < _gridMaxR; r++)
                {
                    for (var c = 0; c < _gridMaxC; c++)
                    {
                        if (processedPoints.Contains($"{r},{c}"))
                            continue;

                        var current = _grid[r, c];

                        if (current == '>')
                        {
                            var nextC = (c + 1) % _gridMaxC;

                            if (processedPoints.Contains($"{r},{nextC}"))
                                continue;

                            var next = _grid[r, nextC];
                            
                            if (next == '.')
                            {
                                _grid[r, c] = '.';
                                _grid[r, nextC] = '>';
                                moved = true;

                                processedPoints.Add($"{r},{c}");
                                processedPoints.Add($"{r},{nextC}");
                            }
                        }
                    }
                }

                processedPoints.Clear();

                for (var r = 0; r < _gridMaxR; r++)
                {
                    for (var c = 0; c < _gridMaxC; c++)
                    {
                        if (processedPoints.Contains($"{r},{c}"))
                            continue;

                        var current = _grid[r, c];

                        if (current == 'v')
                        {
                            var nextR = (r + 1) % _gridMaxR;

                            if (processedPoints.Contains($"{nextR},{c}"))
                                continue;

                            var next = _grid[nextR, c];

                            if (next == '.')
                            {
                                _grid[r, c] = '.';
                                _grid[nextR, c] = 'v';
                                moved = true;

                                processedPoints.Add($"{r},{c}");
                                processedPoints.Add($"{nextR},{c}");
                            }
                        }
                    }
                }

                steps++;

                if (!moved) break;
            }

            return steps;
        }
    }
}