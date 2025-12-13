namespace AdventOfCode.Y2025.Puzzle7.Part1
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            var grid = new char[lines.Length, lines[0].Length];

            for (int r = 0; r < lines.Length; r++)
                for (int c = 0; c < lines[r].Length; c++)
                    grid[r, c] = lines[r][c];

            var beamColIndexPositions = new HashSet<int>();
            var splits = 0;

            for (var r = 0; r < grid.GetLength(0); r++)
            {
                for (var c = 0; c < grid.GetLength(1); c++)
                {
                    if (grid[r, c] == 'S')
                        beamColIndexPositions.Add(c);
                    else if (beamColIndexPositions.Contains(c))
                    {
                        if (grid[r, c] == '.')
                            grid[r, c] = '|';
                        else if (grid[r, c] == '^')
                        {
                            // beam encountered a splitter, terminate this beam
                            beamColIndexPositions.Remove(c);
                            splits++;

                            // start new beams if they will still be in the manifold
                            if ((c - 1) >= 0)
                            {
                                beamColIndexPositions.Add(c - 1);
                                grid[r, c - 1] = '|';
                            }

                            if ((c + 1) < grid.GetLength(1))
                            {
                                beamColIndexPositions.Add(c + 1);
                                grid[r, c + 1] = '|';
                            }
                        }
                    }
                }
            }

            // Print for debugging
            //for (int r = 0; r < lines.Length; r++)
            //{
            //    for (int c = 0; c < lines[r].Length; c++)
            //        Console.Write(grid[r, c]);
            //    Console.WriteLine();
            //}

            Console.WriteLine(splits);
        }
    }
}