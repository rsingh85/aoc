namespace AdventOfCode.Y2024.Puzzle8.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var grid = Convert1dArrayTo2dArray(lines);
            var antennaLocations = ScanForAntennas(grid);

            var antiNodeCount = AddAntiNodes(grid, antennaLocations);

            //Print(grid);
            Console.WriteLine(antiNodeCount);
        }

        private Dictionary<char, List<(int, int)>> ScanForAntennas(char[,] grid)
        {
            var antennaLocations = new Dictionary<char, List<(int r, int c)>>();

            for (var r = 0; r < grid.GetLength(0); r++)
            {
                for (var c = 0; c < grid.GetLength(1); c++)
                {
                    var antenna = grid[r, c];

                    if (antenna == '.')
                        continue;

                    if (antennaLocations.ContainsKey(antenna))
                    {
                        antennaLocations[antenna].Add((r, c));
                        antennaLocations[antenna] = [.. antennaLocations[antenna].OrderBy(l => l.r)];
                    }
                    else
                        antennaLocations.Add(antenna, [(r, c)]);
                }
            }

            return antennaLocations;
        }

        private int AddAntiNodes(char[,] grid, Dictionary<char, List<(int r, int c)>> antennaLocations)
        {
            var antiNodeLocationSet = new HashSet<(int, int)>();

            foreach (var antenna in antennaLocations.Keys)
            {
                var locations = antennaLocations[antenna];

                if (locations.Count == 1)
                    continue;

                for (var i = 0; i < locations.Count; i++)
                {
                    var currentLocation = locations[i];
                    var otherLocations = locations.Where(l => l != currentLocation).ToList();

                    foreach (var otherLocation in otherLocations)
                    {
                        var currentLocationAntiNodeR = currentLocation.r - (otherLocation.r - currentLocation.r);
                        var currentLocationAntiNodeC = currentLocation.c - (otherLocation.c - currentLocation.c);

                        if (IsLocationInBounds(currentLocationAntiNodeR, currentLocationAntiNodeC, grid.GetLength(0), grid.GetLength(1)))
                        {
                            grid[currentLocationAntiNodeR, currentLocationAntiNodeC] = '#';
                            antiNodeLocationSet.Add((currentLocationAntiNodeR, currentLocationAntiNodeC));
                        }
                    }
                }
            }
            return antiNodeLocationSet.Count();
        }

        private static bool IsLocationInBounds(int r, int c, int maxR, int maxC)
        {
            return r >= 0 && r < maxR && c >= 0 && c < maxC;
        }

        private static void Print(char[,] grid)
        {
            for (var r = 0; r < grid.GetLength(0); r++)
            {
                for (var c = 0; c < grid.GetLength(1); c++)
                {
                    Console.Write(grid[r, c] + " ");
                }
                Console.WriteLine();
            }
        }

        private static char[,] Convert1dArrayTo2dArray(string[] lines)
        {
            var grid = new char[lines.Length, lines[0].Length];

            for (var r = 0; r < lines.Length; r++)
                for (var c = 0; c < lines[r].Length; c++)
                    grid[r, c] = lines[r][c];

            return grid;
        }
    }
}