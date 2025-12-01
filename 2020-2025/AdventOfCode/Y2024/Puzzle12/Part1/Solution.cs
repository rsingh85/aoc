namespace AdventOfCode.Y2024.Puzzle12.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var grid = Convert1dArrayTo2dArray(lines);

            // add region ids
            for (var r = 0; r < lines.Length; r++)
            {
                for (var c = 0; c < lines[r].Length; c++)
                {
                    var plot = grid[r, c];

                    string? regionId = FindRegionIdFromSurroundingPlotsOfSamePlantType((r, c), plot.PlantType, grid, []);

                    plot.RegionId = (regionId is null) ? $"{plot.PlantType}-{Guid.NewGuid()}" : regionId;
                }
            }

            var regionData = new Dictionary<string, RegionData>(); // key = RegionId

            for (var r = 0; r < grid.GetLength(0); r++)
            {
                for (var c = 0; c < grid.GetLength(1); c++)
                {
                    var currentPlot = grid[r, c];
                    var currentRegionData = new RegionData
                    {
                        Area = 1,
                        Perimeter = 0
                    };

                    var top = (r: r - 1, c);
                    var right = (r, c: c + 1);
                    var bottom = (r: r + 1, c);
                    var left = (r, c: c - 1);

                    if (!InBounds(top, grid) || (InBounds(top, grid) && grid[top.r, top.c].RegionId != currentPlot.RegionId))
                        currentRegionData.Perimeter++;

                    if (!InBounds(right, grid) || (InBounds(right, grid) && grid[right.r, right.c].RegionId != currentPlot.RegionId))
                        currentRegionData.Perimeter++;

                    if (!InBounds(bottom, grid) || (InBounds(bottom, grid) && grid[bottom.r, bottom.c].RegionId != currentPlot.RegionId))
                        currentRegionData.Perimeter++;

                    if (!InBounds(left, grid) || (InBounds(left, grid) && grid[left.r, left.c].RegionId != currentPlot.RegionId))
                        currentRegionData.Perimeter++;

                    if (regionData.ContainsKey(currentPlot.RegionId!))
                    {
                        var existingRegionData = regionData[currentPlot.RegionId!];
                        existingRegionData.Area += currentRegionData.Area;
                        existingRegionData.Perimeter += currentRegionData.Perimeter;
                    }
                    else regionData.Add(currentPlot.RegionId!, currentRegionData);

                }
            }

            Print(grid);

            foreach (var key in regionData.Keys)
            {
                Console.WriteLine("{0} => {1} * {2} = {3}", key, regionData[key].Area, regionData[key].Perimeter, regionData[key].Price);
            }

            Console.WriteLine("Price = {0}", regionData.Values.Select(pd => pd.Price).Sum());
        }

        private static string? FindRegionIdFromSurroundingPlotsOfSamePlantType((int r, int c) coords, char targetPlantType, Plot[,] grid, HashSet<(int, int)> visited)
        {
            if (visited.Contains(coords))
                return null;

            visited.Add(coords);

            var plot = grid[coords.r, coords.c];

            if (plot.PlantType != targetPlantType)
                return null;

            if (plot.PlantType == targetPlantType && plot.RegionId is not null)
                return plot.RegionId;

            string? regionId = null;

            var top = (r: coords.r - 1, coords.c);
            var right = (coords.r, c: coords.c + 1);
            var bottom = (r: coords.r + 1, coords.c);
            var left = (coords.r, c: coords.c - 1);

            if (InBounds(top, grid))
                regionId = FindRegionIdFromSurroundingPlotsOfSamePlantType(top, targetPlantType, grid, visited);

            if (InBounds(right, grid) && regionId is null)
                regionId = FindRegionIdFromSurroundingPlotsOfSamePlantType(right, targetPlantType, grid, visited);

            if (InBounds(bottom, grid) && regionId is null)
                regionId = FindRegionIdFromSurroundingPlotsOfSamePlantType(bottom, targetPlantType, grid, visited);

            if (InBounds(left, grid) && regionId is null)
                regionId = FindRegionIdFromSurroundingPlotsOfSamePlantType(left, targetPlantType, grid, visited);

            return regionId;
        }

        private static bool InBounds((int r, int c) coords, Plot[,] grid) =>
            coords.r >= 0 && coords.r < grid.GetLength(0) && coords.c >= 0 && coords.c < grid.GetLength(1);

        private static Plot[,] Convert1dArrayTo2dArray(string[] lines)
        {
            var grid = new Plot[lines.Length, lines[0].Length];

            for (var r = 0; r < lines.Length; r++)
            {
                for (var c = 0; c < lines[r].Length; c++)
                {
                    grid[r, c] = new Plot { PlantType = lines[r][c] };
                }
            }

            return grid;
        }

        private static void Print(Plot[,] grid)
        {
            for (var r = 0; r < grid.GetLength(0); r++)
            {
                for (var c = 0; c < grid.GetLength(1); c++)
                {
                    Console.Write("{0}{1} ", grid[r, c], grid[r, c].RegionId!.Substring(2, 2));
                }
                Console.WriteLine();
            }
        }
    }

    public class Plot
    {
        public char PlantType { get; set; }
        public string? RegionId { get; set; }
        public override string ToString() => PlantType.ToString();
    }

    public class RegionData
    {
        public int Area { get; set; }
        public int Perimeter { get; set; }
        public int Price => Area * Perimeter;
    }
}