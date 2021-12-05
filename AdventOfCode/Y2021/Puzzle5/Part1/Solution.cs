using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2021.Puzzle5.Part1
{
    public class Solution : ISolution
    {
        private int[,] grid;
        private HashSet<string> dangerousPointsSet;

        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this));
            
            var lines = ReadLines(input);
            var gridSize = GetGridSize(lines) + 1;

            grid = new int[gridSize, gridSize];
            dangerousPointsSet = new HashSet<string>();

            foreach (var line in lines.Where(l => l.IsHorizontalOrVertical))
            {
                if (line.IsHorizontal)
                {
                    var minX = Math.Min(line.A.X, line.B.X);
                    var maxX = Math.Max(line.A.X, line.B.X);
                    
                    for (var x = minX; x <= maxX; x++)
                    {
                        AddPointToGrid(x, line.A.Y);
                    }
                }
                else
                {
                    var minY = Math.Min(line.A.Y, line.B.Y);
                    var maxY = Math.Max(line.A.Y, line.B.Y);

                    for (var y = minY; y <= maxY; y++)
                    {
                        AddPointToGrid(line.A.X, y);
                    }
                }
            }

            Console.WriteLine(dangerousPointsSet.Count);
        }
        
        private IEnumerable<Line> ReadLines(string[] input)
        {
            var lines = new List<Line>();

            foreach (var inputLine in input)
            {
                var lineSplit = inputLine.Split(new string[] { " -> " }, StringSplitOptions.None);
                var pointASplit = lineSplit[0].Split(',');
                var pointBSplit = lineSplit[1].Split(',');

                var line = new Line
                {
                    A = new Point
                    {
                        X = int.Parse(pointASplit[0]),
                        Y = int.Parse(pointASplit[1])
                    },
                    B = new Point
                    {
                        X = int.Parse(pointBSplit[0]),
                        Y = int.Parse(pointBSplit[1])
                    }
                };

                lines.Add(line);
            }

            return lines;
        }

        private int GetGridSize(IEnumerable<Line> lines)
        {
            var points = new List<int>
            {
                lines.Select(l => l.A.X).Max(),
                lines.Select(l => l.A.Y).Max(),
                lines.Select(l => l.B.X).Max(),
                lines.Select(l => l.B.Y).Max()
            };

            return points.Max();
        }

        private void AddPointToGrid(int x, int y)
        {
            grid[x, y]++;

            if (grid[x, y] > 1)
            {
                dangerousPointsSet.Add($"{x},{y}");
            }
        }
    }

    public class Line
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public bool IsHorizontalOrVertical => (A.X == B.X) || (A.Y == B.Y);
        public bool IsHorizontal => A.Y == B.Y;
        public override string ToString() => $"{A.X},{A.Y} -> {B.X},{B.Y}";
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override string ToString() => $"{X},{Y}";
    }
}