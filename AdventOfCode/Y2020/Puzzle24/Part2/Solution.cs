using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Puzzle24.Part2
{
    public class Solution : ISolution
    {
        /*
         * Hex tiles coordinate system:
         * 
         *      (-1,1) (0,1)
         * (-1,0 )  (0,0)  (1,0)
         *      (0,-1) (1,-1)
         */
        public void Run()
        {
            var directions = File.ReadAllLines(Helper.GetInputFilePath(this));
            var tiles = ReadTiles(directions);

            var currentDay = 1;
            const int MaxDays = 100;

            var tilesToFlipOnThisDay = new List<Point>();

            while (currentDay <= MaxDays)
            {
                tilesToFlipOnThisDay.Clear();

                // add in the neighbour tiles
                var neighbourPoints = tiles.Keys.SelectMany(p => GetAdjacentBlackTiles(p)).ToList();

                foreach (var neighbourPoint in neighbourPoints)
                {
                    if (!tiles.ContainsKey(neighbourPoint))
                    {
                        tiles.Add(neighbourPoint, false);
                    }
                }

                foreach (var tilePoint in tiles.Keys)
                {
                    var tileIsBlack = tiles[tilePoint];
                    var adjacentBlackTilesCount = CountAdjacentBlackTiles(tilePoint, tiles);

                    if (tileIsBlack && (adjacentBlackTilesCount == 0 || adjacentBlackTilesCount > 2))
                    {
                        tilesToFlipOnThisDay.Add(tilePoint);
                    }
                    else if (!tileIsBlack && adjacentBlackTilesCount == 2)
                    {
                        tilesToFlipOnThisDay.Add(tilePoint);
                    }
                }

                // Flip 'em!
                foreach (var tileToFlip in tilesToFlipOnThisDay)
                {
                    tiles[tileToFlip] = !tiles[tileToFlip];
                }

                Console.WriteLine("Day {0}: {1}", currentDay, tiles.Values.Where(v => v).Count());
                currentDay++;
            }
        }

        private List<Point> GetAdjacentBlackTiles(Point tilePoint)
        {
            return new List<Point>
            {
                new Point { X = tilePoint.X + 1, Y = tilePoint.Y }, // east
                new Point { X = tilePoint.X - 1, Y = tilePoint.Y }, // west
                new Point { X = tilePoint.X + 1, Y = tilePoint.Y - 1 }, // south east
                new Point { X = tilePoint.X, Y = tilePoint.Y - 1 }, // south west
                new Point { X = tilePoint.X, Y = tilePoint.Y + 1 }, // north east
                new Point { X = tilePoint.X - 1, Y = tilePoint.Y + 1 }  // north west
            };
        }

        private int CountAdjacentBlackTiles(Point tilePoint, Dictionary<Point, bool> tiles)
        {
            var adjacentTilePoints = new List<Point>
            {
                new Point { X = tilePoint.X + 1, Y = tilePoint.Y }, // east
                new Point { X = tilePoint.X - 1, Y = tilePoint.Y }, // west
                new Point { X = tilePoint.X + 1, Y = tilePoint.Y - 1 }, // south east
                new Point { X = tilePoint.X, Y = tilePoint.Y - 1 }, // south west
                new Point { X = tilePoint.X, Y = tilePoint.Y + 1 }, // north east
                new Point { X = tilePoint.X - 1, Y = tilePoint.Y + 1 }  // north west
            };

            var count = 0;

            foreach (var adjacentTilePoint in adjacentTilePoints)
            {
                if (tiles.ContainsKey(adjacentTilePoint) && tiles[adjacentTilePoint])
                {
                    count++;
                }
            }

            return count;
        }

        private Dictionary<Point, bool> ReadTiles(string[] directions)
        {
            // white == false, black == true
            var tiles = new Dictionary<Point, bool>();

            foreach (var direction in directions)
            {
                var point = new Point();

                for (var d = 0; d < direction.Length; d++)
                {
                    var directionChar = direction[d];

                    switch (directionChar)
                    {
                        case 'e':
                            point.X++;
                            break;
                        case 'w':
                            point.X--;
                            break;
                        case 's':
                            switch (direction[++d])
                            {
                                case 'e':
                                    point.X++;
                                    point.Y--;
                                    break;
                                case 'w':
                                    point.Y--;
                                    break;
                            }
                            break;
                        case 'n':
                            switch (direction[++d])
                            {
                                case 'e':
                                    point.Y++;
                                    break;
                                case 'w':
                                    point.X--;
                                    point.Y++;
                                    break;
                            }
                            break;
                    }
                }

                tiles[point] =
                    tiles.ContainsKey(point) ?
                        !tiles[point] : true;
            }

            return tiles;
        }

        private class Point
        {
            public int X { get; set; }
            public int Y { get; set; }

            public override bool Equals(object obj)
            {
                //Check for null and compare run-time types.
                if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                {
                    return false;
                }
                else
                {
                    Point p = (Point)obj;
                    return (X == p.X) && (Y == p.Y);
                }
            }

            public override int GetHashCode()
            {
                return (X << 2) ^ Y;
            }

            public override string ToString()
            {
                return $"{{{X},{Y}}}";
            }
        }
    }
}