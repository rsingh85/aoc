namespace AdventOfCode.Y2021.Puzzle11.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this));
            var maxRows = input.Length;
            var maxCols = input.First().Length;
            var grid = new Octopus[maxRows, maxCols];

            for (var r = 0; r < maxRows; r++)
            {
                for (var c = 0; c < maxCols; c++)
                {
                    grid[r, c] = new Octopus(Convert.ToInt32(input[r][c].ToString()));
                }
            }

            for (var r = 0; r < maxRows; r++)
            {
                for (var c = 0; c < maxCols; c++)
                {
                    var octopus = grid[r, c];

                    if (r > 0 && c > 0) octopus.AddAdjacentOctopus(grid[r - 1, c - 1]);
                    if (r > 0) octopus.AddAdjacentOctopus(grid[r - 1, c]);
                    if (r > 0 && c < maxCols - 1) octopus.AddAdjacentOctopus(grid[r - 1, c + 1]);
                    if (c < maxCols - 1) octopus.AddAdjacentOctopus(grid[r, c + 1]);
                    if (r < maxRows - 1 && c < maxCols - 1) octopus.AddAdjacentOctopus(grid[r + 1, c + 1]);
                    if (r < maxRows - 1) octopus.AddAdjacentOctopus(grid[r + 1, c]);
                    if (r < maxRows - 1 && c > 0) octopus.AddAdjacentOctopus(grid[r + 1, c - 1]);
                    if (c > 0) octopus.AddAdjacentOctopus(grid[r, c - 1]);
                }
            }

            var step = 1;

            while (step <= 100)
            {
                for (var r = 0; r < maxRows; r++)
                {
                    for (var c = 0; c < maxCols; c++)
                    {
                        var octopus = grid[r, c];
                        octopus.IncreaseEnergy(step);
                    }
                }

                step++;
            }

            Console.WriteLine(grid.Cast<Octopus>().Sum(o => o.Flashes));
        }
    }
    public class Octopus
    {
        public int Energy { get; private set; }
        public int Flashes => _stepsFlashedIn.Count;
        
        private readonly List<Octopus> _adjacentOctopuses = new();
        private readonly List<int> _stepsFlashedIn = new();

        public Octopus(int energy)
        {
            Energy = energy;
        }

        public void AddAdjacentOctopus(Octopus octopus)
        {
            _adjacentOctopuses.Add(octopus);
        }

        public void IncreaseEnergy(int step)
        {
            if (_stepsFlashedIn.Contains(step)) { return; }

            Energy++;

            if (Energy > 9)
            {
                _stepsFlashedIn.Add(step);

                Energy = 0;

                foreach (var octopus in _adjacentOctopuses)
                {
                    octopus.IncreaseEnergy(step);
                }
            }
        }
    }
}