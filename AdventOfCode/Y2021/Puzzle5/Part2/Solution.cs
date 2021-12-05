using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2021.Puzzle5.Part2
{
    public class Solution : ISolution
    {
        private Dictionary<string, int> _ventPointsAndOverlaps;

        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this));
            var lines = ReadLines(input);

            _ventPointsAndOverlaps = new Dictionary<string, int>();

            foreach (var line in lines)
            {
                if (line.IsHorizontalOrVertical && line.IsHorizontal)
                {
                    var minX = Math.Min(line.A.X, line.B.X);
                    var maxX = Math.Max(line.A.X, line.B.X);
                    
                    for (var x = minX; x <= maxX; x++)
                    {
                        AddPoint(x, line.A.Y);
                    }
                }
                else if (line.IsHorizontalOrVertical && !line.IsHorizontal)
                {
                    var minY = Math.Min(line.A.Y, line.B.Y);
                    var maxY = Math.Max(line.A.Y, line.B.Y);

                    for (var y = minY; y <= maxY; y++)
                    {
                        AddPoint(line.A.X, y);
                    }
                }
                else
                {
                    var x = line.A.X;
                    var y = line.A.Y;

                    while (x != line.B.X && y != line.B.Y)
                    {
                        AddPoint(x, y);

                        x += x < line.B.X ? 1 : -1;
                        y += y < line.B.Y ? 1 : -1;
                    }

                    AddPoint(x, y);
                }
            }

            Console.WriteLine(_ventPointsAndOverlaps.Values.Count(overlaps => overlaps > 1));
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

        private void AddPoint(int x, int y)
        {
            var pointKey = $"{x},{y}";

            if (_ventPointsAndOverlaps.ContainsKey(pointKey))
            {
                _ventPointsAndOverlaps[pointKey]++;
            }
            else
            {
                _ventPointsAndOverlaps.Add(pointKey, 1);
            }
        }
    }

    public class Line
    {
        public Point A { get; set; }
        public Point B { get; set; }
        public bool IsHorizontalOrVertical => (A.X == B.X) || (A.Y == B.Y);
        public bool IsHorizontal => A.Y == B.Y;
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Overlaps { get; set; }
    }
}