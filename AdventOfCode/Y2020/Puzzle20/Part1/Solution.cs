using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle20.Part1
{
    public class Solution : ISolution
    {
        private readonly List<Tile> _tiles = new List<Tile>();

        public void Run()
        {
            ParseInput(File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution))));

            long productOfCornerTileNumbers = 0;

            foreach (var tile in _tiles)
            {
                tile.TopTile = FindTopTileFor(tile);
                tile.RightTile = FindRightTileFor(tile);
                tile.BottomTile = FindBottomTileFor(tile);
                tile.LeftTile = FindLeftTileFor(tile);

                var edgeTileCount = 0;
                edgeTileCount += tile.TopTile != null ? 1 : 0;
                edgeTileCount += tile.RightTile != null ? 1 : 0;
                edgeTileCount += tile.BottomTile != null ? 1 : 0;
                edgeTileCount += tile.LeftTile != null ? 1 : 0;

                if (edgeTileCount == 2)
                {
                    // corner tiles only have two edge tiles...

                    Console.WriteLine(tile.Number);

                    productOfCornerTileNumbers =
                        (productOfCornerTileNumbers == 0) ?
                            tile.Number :
                            productOfCornerTileNumbers * tile.Number;
                }
            }

            Console.WriteLine(productOfCornerTileNumbers);
        }

        private Tile FindTopTileFor(Tile tile)
        {
            foreach (var currentTile in _tiles.Where(t => t.Number != tile.Number))
            {
                for (var rotations = 0; rotations < 4; rotations++)
                {
                    for (var flips = 0; flips < 2; flips++)
                    {
                        if (tile.GetBottomBorder() == currentTile.GetTopBorder())
                        {
                            return currentTile;
                        }
                        currentTile.Flip();
                    }
                    currentTile.Rotate();
                }
            }
            return null;
        }

        private Tile FindRightTileFor(Tile tile)
        {
            foreach (var currentTile in _tiles.Where(t => t.Number != tile.Number))
            {
                for (var rotations = 0; rotations < 4; rotations++)
                {
                    for (var flips = 0; flips < 2; flips++)
                    {
                        if (tile.GetRightBorder() == currentTile.GetLeftBorder())
                        {
                            return currentTile;
                        }
                        currentTile.Flip();
                    }
                    currentTile.Rotate();
                }
            }
            return null;
        }

        private Tile FindBottomTileFor(Tile tile)
        {
            foreach (var currentTile in _tiles.Where(t => t.Number != tile.Number))
            {
                for (var rotations = 0; rotations < 4; rotations++)
                {
                    for (var flips = 0; flips < 2; flips++)
                    {
                        if (tile.GetTopBorder() == currentTile.GetBottomBorder())
                        {
                            return currentTile;
                        }
                        currentTile.Flip();
                    }
                    currentTile.Rotate();
                }
            }
            return null;
        }

        private Tile FindLeftTileFor(Tile tile)
        {
            foreach (var currentTile in _tiles.Where(t => t.Number != tile.Number))
            {
                for (var rotations = 0; rotations < 4; rotations++)
                {
                    for (var flips = 0; flips < 2; flips++)
                    {
                        if (tile.GetLeftBorder() == currentTile.GetRightBorder())
                        {
                            return currentTile;
                        }
                        currentTile.Flip();
                    }
                    currentTile.Rotate();
                }
            }
            return null;
        }

        private void ParseInput(string[] lines)
        {
            for (var i = 0; i < lines.Length; i += (Tile.TileGridSize + 1))
            {
                var currentLine = lines[i];
                var number = int.Parse(Regex.Match(currentLine, @"^Tile (\d+)").Groups[1].Value);
                var imageData = new char[Tile.TileGridSize, Tile.TileGridSize];

                i++;

                for (var row = 0; row < Tile.TileGridSize; row++)
                {
                    currentLine = lines[i + row];

                    for (var col = 0; col < currentLine.Length; col++)
                    {
                        imageData[row, col] = currentLine[col];
                    }
                }

                var tile = new Tile(number, imageData);
                _tiles.Add(tile);
            }
        }

        private class Tile
        {
            public static int TileGridSize = 10; // TODO: Alter this to input grid size...

            public int Number { get; set; }
            public char[,] ImageData { get; set; }
            public Tile TopTile { get; set; }
            public Tile RightTile { get; set; }
            public Tile BottomTile { get; set; }
            public Tile LeftTile { get; set; }

            public Tile(int number, char[,] imageData)
            {
                Number = number;
                ImageData = imageData;
            }

            public void Rotate()
            {
                var rotatedImageData = new char[TileGridSize, TileGridSize];

                for (int i = 0; i < TileGridSize; ++i)
                {
                    for (int j = 0; j < TileGridSize; ++j)
                    {
                        rotatedImageData[i, j] = ImageData[TileGridSize - j - 1, i];
                    }
                }

                ImageData = rotatedImageData;
            }

            public void Flip()
            {
                var flippedImageData = new char[TileGridSize, TileGridSize];

                for (int i = 0; i < TileGridSize; i++)
                {
                    for (int j = 0; j < TileGridSize; j++)
                    {
                        flippedImageData[i, j] = ImageData[i, TileGridSize - j - 1];
                    }
                }

                ImageData = flippedImageData;
            }

            public string GetTopBorder()
            {
                var border = string.Empty;

                for (var col = 0; col < TileGridSize; col++)
                {
                    border += ImageData[0, col];
                }

                return border;
            }

            public string GetRightBorder()
            {
                var border = string.Empty;

                for (var row = 0; row < TileGridSize; row++)
                {
                    border += ImageData[row, TileGridSize - 1];
                }

                return border;
            }

            public string GetBottomBorder()
            {
                var border = string.Empty;

                for (var col = 0; col < TileGridSize; col++)
                {
                    border += ImageData[TileGridSize - 1, col];
                }

                return border;
            }

            public string GetLeftBorder()
            {
                var border = string.Empty;

                for (var row = 0; row < TileGridSize; row++)
                {
                    border += ImageData[row, 0];
                }

                return border;
            }

            public void Print()
            {
                for (var row = 0; row < TileGridSize; row++)
                {
                    for (var col = 0; col < TileGridSize; col++)
                    {
                        Console.Write("{0} ", ImageData[row, col]);
                    }

                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            public override bool Equals(object obj)
            {
                //Check for null and compare run-time types.
                if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                {
                    return false;
                }
                else
                {
                    var t = (Tile)obj;
                    return Number == t.Number;
                }
            }

            public override string ToString()
            {
                return Number.ToString();
            }
        }
    }
}