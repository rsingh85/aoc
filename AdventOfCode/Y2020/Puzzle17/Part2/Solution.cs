using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Puzzle17.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution)));

            var currentGenActivePoints = new List<Point>();

            for (var y = 0; y < input.Length; y++)
            {
                var line = input[y];

                for (var x = 0; x < line.Length; x++)
                {
                    var ch = line[x];

                    if (ch == '#')
                    {
                        currentGenActivePoints.Add(new Point(x, y, z: 0, w: 0));
                    }
                }
            }

            var cycle = 1;
            const int totalCycles = 6;

            while (cycle <= totalCycles)
            {
                var nextGenActivePoints = new List<Point>();

                foreach (var point in currentGenActivePoints)
                {
                    var numberOfActiveNeighbours = GetActiveNeighbours(currentGenActivePoints, point);

                    if (numberOfActiveNeighbours == 2 || numberOfActiveNeighbours == 3)
                    {
                        nextGenActivePoints.Add(point);
                    }

                    for (var x = -1; x <= 1; x++)
                    {
                        for (var y = -1; y <= 1; y++)
                        {
                            for (var z = -1; z <= 1; z++)
                            {
                                for (var w = -1; w <= 1; w++)
                                {
                                    if (x != 0 || y != 0 || z != 0 || w != 0)
                                    {
                                        var neighbourPoint = new Point(point.X + x, point.Y + y, point.Z + z, point.W + w);

                                        if (!currentGenActivePoints.Contains(neighbourPoint))
                                        {
                                            var num = GetActiveNeighbours(currentGenActivePoints, neighbourPoint);

                                            if (num == 3)
                                            {
                                                nextGenActivePoints.Add(neighbourPoint);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var uniqueNextGenActivePoints = new List<Point>();

                foreach (var nextGenPoint in nextGenActivePoints)
                {
                    if (!uniqueNextGenActivePoints.Contains(nextGenPoint))
                    {
                        uniqueNextGenActivePoints.Add(nextGenPoint);
                    }
                }

                currentGenActivePoints = uniqueNextGenActivePoints;
                Console.WriteLine("Cycle = {0} | Active = {1}", cycle, currentGenActivePoints.Count);
                cycle++;
            }
        }

        private int GetActiveNeighbours(List<Point> activeGeneration, Point point)
        {
            var count = 0;

            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    for (var z = -1; z <= 1; z++)
                    {
                        for (var w = -1; w <= 1; w++)
                        {
                            if (x != 0 || y != 0 || z != 0 || w != 0)
                            {
                                var neighbourPoint = new Point(point.X + x, point.Y + y, point.Z + z, point.W + w);

                                count += activeGeneration.Contains(neighbourPoint) ? 1 : 0;
                            }
                        }
                    }
                }
            }

            return count;
        }


        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public int W { get; set; }

            public Point(int x, int y, int z, int w)
            {
                X = x;
                Y = y;
                Z = z;
                W = w;
            }

            public override string ToString()
            {
                return $"x = {X}, y = {Y}, z = {Z}, w = {W}";
            }

            public override bool Equals(Object obj)
            {
                //Check for null and compare run-time types.
                if ((obj == null) || !GetType().Equals(obj.GetType()))
                {
                    return false;
                }

                Point p = (Point)obj;
                return (X == p.X) && (Y == p.Y) && (Z == p.Z) && (W == p.W);
            }
        }
    }
}