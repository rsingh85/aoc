namespace AdventOfCode.Y2021.Puzzle13.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var points = new List<Point>();
            var folds = new List<Fold>();

            foreach (var line in lines.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                if (line.Contains(","))
                {
                    var splitLine = line.Split(',');
                    points.Add(new Point { X = int.Parse(splitLine[0]), Y = int.Parse(splitLine[1]) });
                }
                else
                {
                    var splitLine = line.Split('=');
                    folds.Add(new Fold { Axis = line.Contains("x") ? 'X' : 'Y' });
                }
            }

            var maxX = points.Select(p => p.X).Max() + 1;
            var maxY = points.Select(p => p.Y).Max() + 1;
            var grid = new char[maxX, maxY];

            foreach (var point in points)
            {
                grid[point.X, point.Y] = '#';
            }

            var foldedGrid =
                folds.First().Axis == 'X' ?
                        FoldOnX(grid) :
                        FoldOnY(grid);

            Console.WriteLine(foldedGrid.Cast<char>().ToArray().Where(c => c == '#').Count());
        }

        private char[,] FoldOnX(char[,] grid)
        {
            var maxX = grid.GetLength(0);
            var maxY = grid.GetLength(1);
            var foldedMaxX = maxX / 2;
            var foldedGrid = new char[foldedMaxX, maxY];

            for (var y = 0; y < maxY; y++)
            {
                for (var x = 0; x < foldedMaxX; x++)
                {
                    var leftSide = grid[x, y];
                    var oppositeSide = grid[maxX - x - 1, y];

                    foldedGrid[x, y] = leftSide == '#' || oppositeSide == '#' ? '#' : '.';
                }
            }

            return foldedGrid;
        }

        private char[,] FoldOnY(char[,] grid)
        {
            var maxX = grid.GetLength(0);
            var maxY = grid.GetLength(1);
            var foldedMaxY = maxY / 2;
            var foldedGrid = new char[maxX, foldedMaxY];

            for (var y = 0; y < foldedMaxY; y++)
            {
                for (var x = 0; x < maxX; x++)
                {
                    var topSide = grid[x, y];
                    var oppositeSide = grid[x, maxY - y - 1];

                    foldedGrid[x, y] = topSide == '#' || oppositeSide == '#' ? '#' : '.';
                }
            }

            return foldedGrid;
        }
    }

    public struct Point
    {
        public int X;
        public int Y;

        public override string ToString() => $"{X},{Y}";
    }

    public struct Fold
    {
        public char Axis;
    }
}