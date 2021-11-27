using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace AdventOfCode.Puzzle20.Part2
{
    public class Solution : ISolution
    {
        private static int TileGridSize = 10;
        private static int ImageGridSize = (TileGridSize - 2) * 12;

        public void Run()
        {
            var tiles = ParseInput(File.ReadAllLines(Helper.GetInputFilePath(this)));
            var tileOrientations = GetTileOrientations(tiles);
            var stichedTiles = StitchTiles(tiles, tileOrientations);
            var topLeftTile = stichedTiles.Single(t => t.TopTile == null && t.LeftTile == null);
            var image = BuildImage(topLeftTile);

            // Count sea monsters
            var imageOrientations = GetImageOrientations(image);
            var totalHashCount = GetTotalHashCount(image);
            var seaMonsterCount = CountSeaMonsters(imageOrientations);

            // Answer
            Console.WriteLine(totalHashCount - (seaMonsterCount * 15));
        }

        private int GetTotalHashCount(Image image)
        {
            var count = 0;

            for (var row = 0; row < ImageGridSize; row++)
            {
                for (var col = 0; col < ImageGridSize; col++)
                {
                    if (image.ImageData[row, col] == '#')
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private int CountSeaMonsters(List<Image> imageOrientations)
        {
            var countsPerOrientation = new List<int>();
            var seaMonsterPattern = @".{18}#.{1}#.{4}##.{4}##.{4}###.{1}#.{2}#.{2}#.{2}#.{2}#.{2}#.{3}";

            foreach (var imageOrientation in imageOrientations)
            {
                var imageOrientationBlocks = GetImageSeaMonsterSizedBlocks(imageOrientation);
                var seaMonstersCountInThisOrientation = 0;

                foreach (var block in imageOrientationBlocks)
                {
                    var match = Regex.Match(block, seaMonsterPattern);

                    if (match.Success)
                    {
                        seaMonstersCountInThisOrientation++;
                    }
                }

                countsPerOrientation.Add(seaMonstersCountInThisOrientation);
            }

            return countsPerOrientation.Max();
        }

        private List<string> GetImageSeaMonsterSizedBlocks(Image image)
        {
            var blocks = new List<string>();

            for (var row = 0; row < ImageGridSize; row++)
            {
                for (var col = 0; col < ImageGridSize; col++)
                {
                    var maxRow = row + 3;
                    var maxCol = col + 20;

                    // Can't fit another block in from this row/column 
                    if (maxRow > ImageGridSize || maxCol > ImageGridSize)
                    {
                        continue;
                    }

                    var block = new StringBuilder();

                    for (var blockRow = row; blockRow < maxRow; blockRow++)
                    {
                        for (var blockCol = col; blockCol < maxCol; blockCol++)
                        {
                            block.Append(image.ImageData[blockRow, blockCol]);
                        }
                    }

                    blocks.Add(block.ToString());
                }
            }

            return blocks;
        }

        private Dictionary<int, List<Tile>> GetTileOrientations(List<Tile> tiles)
        {
            var tileOrientations = new Dictionary<int, List<Tile>>();

            foreach (var tile in tiles)
            {
                var orientations = new List<Tile>();
                orientations.Add(tile);
                orientations.Add(Rotate(orientations.Last()));
                orientations.Add(Rotate(orientations.Last()));
                orientations.Add(Rotate(orientations.Last()));
                orientations.Add(Flip(orientations[0]));
                orientations.Add(Flip(orientations[1]));
                orientations.Add(Flip(orientations[2]));
                orientations.Add(Flip(orientations[3]));

                tileOrientations.Add(tile.Number, orientations);
            }

            return tileOrientations;
        }

        private List<Image> GetImageOrientations(Image image)
        {
            var imageOrientations = new List<Image>();
            imageOrientations.Add(image);
            imageOrientations.Add(Rotate(imageOrientations.Last()));
            imageOrientations.Add(Rotate(imageOrientations.Last()));
            imageOrientations.Add(Rotate(imageOrientations.Last()));
            imageOrientations.Add(Flip(imageOrientations[0]));
            imageOrientations.Add(Flip(imageOrientations[1]));
            imageOrientations.Add(Flip(imageOrientations[2]));
            imageOrientations.Add(Flip(imageOrientations[3]));

            return imageOrientations;
        }

        private List<Tile> StitchTiles(List<Tile> tiles, Dictionary<int, List<Tile>> tileOrientations)
        {
            var stitchedTiles = new List<Tile>();
            var tileQueue = new Queue<Tile>();
            tileQueue.Enqueue(tiles.First());

            while (tileQueue.Any())
            {
                var currentTile = tileQueue.Dequeue();

                if (stitchedTiles.Any(t => t.Number == currentTile.Number))
                {
                    continue;
                }

                stitchedTiles.Add(currentTile);

                foreach (var otherTile in tiles.Where(t => t.Number != currentTile.Number))
                {
                    var otherTileOrientations = tileOrientations[otherTile.Number];

                    foreach (var otherTileOrientation in otherTileOrientations)
                    {
                        if (currentTile.TopTile == null &&
                            currentTile.GetTopBorder() == otherTileOrientation.GetBottomBorder())
                        {
                            currentTile.TopTile = otherTileOrientation;
                            otherTileOrientation.BottomTile = currentTile;
                            tileQueue.Enqueue(otherTileOrientation);
                        }
                        else if (currentTile.RightTile == null &&
                                 currentTile.GetRightBorder() == otherTileOrientation.GetLeftBorder())
                        {
                            currentTile.RightTile = otherTileOrientation;
                            otherTileOrientation.LeftTile = currentTile;
                            tileQueue.Enqueue(otherTileOrientation);
                        }
                        else if (currentTile.BottomTile == null &&
                                 currentTile.GetBottomBorder() == otherTileOrientation.GetTopBorder())
                        {
                            currentTile.BottomTile = otherTileOrientation;
                            otherTileOrientation.TopTile = currentTile;
                            tileQueue.Enqueue(otherTileOrientation);
                        }
                        else if (currentTile.LeftTile == null &&
                                 currentTile.GetLeftBorder() == otherTileOrientation.GetRightBorder())
                        {
                            currentTile.LeftTile = otherTileOrientation;
                            otherTileOrientation.RightTile = currentTile;
                            tileQueue.Enqueue(otherTileOrientation);
                        }
                    }
                }
            }

            return stitchedTiles;
        }

        private Image Rotate(Image image)
        {
            var rotatedImageData = new char[ImageGridSize, ImageGridSize];

            for (int i = 0; i < ImageGridSize; ++i)
            {
                for (int j = 0; j < ImageGridSize; ++j)
                {
                    rotatedImageData[i, j] = image.ImageData[ImageGridSize - j - 1, i];
                }
            }

            var orientation = (image.Orientation + 90 == 360) ? 0 : image.Orientation + 90;

            return new Image(rotatedImageData, ImageGridSize, orientation, image.Flipped);
        }

        private Tile Rotate(Tile tile)
        {
            var rotatedImageData = new char[TileGridSize, TileGridSize];

            for (int i = 0; i < TileGridSize; ++i)
            {
                for (int j = 0; j < TileGridSize; ++j)
                {
                    rotatedImageData[i, j] = tile.ImageData[TileGridSize - j - 1, i];
                }
            }

            var orientation = (tile.Orientation + 90 == 360) ? 0 : tile.Orientation + 90;

            return new Tile(tile.Number, rotatedImageData, TileGridSize, orientation, tile.Flipped);
        }

        private Image Flip(Image image)
        {
            var flippedImageData = new char[ImageGridSize, ImageGridSize];

            for (int i = 0; i < ImageGridSize; i++)
            {
                for (int j = 0; j < ImageGridSize; j++)
                {
                    flippedImageData[i, j] = image.ImageData[i, ImageGridSize - j - 1];
                }
            }

            return new Image(flippedImageData, ImageGridSize, image.Orientation, !image.Flipped);
        }

        private Tile Flip(Tile tile)
        {
            var flippedImageData = new char[TileGridSize, TileGridSize];

            for (int i = 0; i < TileGridSize; i++)
            {
                for (int j = 0; j < TileGridSize; j++)
                {
                    flippedImageData[i, j] = tile.ImageData[i, TileGridSize - j - 1];
                }
            }

            return new Tile(tile.Number, flippedImageData, TileGridSize, tile.Orientation, !tile.Flipped);
        }

        private Image BuildImage(Tile topLeftTile)
        {
            var tilePointer = topLeftTile;
            var imageData = new char[ImageGridSize, ImageGridSize];

            var tileRow = 1;
            var imageRow = 0;
            var imageCol = 0;

            // go through each tile from left to right a row at a time
            while (tilePointer != null)
            {
                for (var tileCol = 1; tileCol < TileGridSize - 1; tileCol++)
                {
                    imageData[imageRow, imageCol++] = tilePointer.ImageData[tileRow, tileCol];
                }

                // have we reached the end of this row of tiles?
                if (tilePointer.RightTile == null)
                {
                    // add a new line
                    imageRow++;
                    imageCol = 0;

                    // go all the way back to the left
                    while (tilePointer.LeftTile != null)
                    {
                        tilePointer = tilePointer.LeftTile;
                    }

                    // have we reached the end of row count for this row of tiles?
                    if (tileRow == TileGridSize - 2)
                    {
                        tileRow = 1;
                        // move a row of tiles down
                        tilePointer = tilePointer.BottomTile;
                    }
                    else
                    {
                        tileRow++;
                    }
                }
                else
                {
                    // no, move right one tile
                    tilePointer = tilePointer.RightTile;
                }
            }

            return new Image(imageData, ImageGridSize, 0, false);
        }

        private List<Tile> ParseInput(string[] lines)
        {
            var tiles = new List<Tile>();

            for (var i = 0; i < lines.Length; i += (TileGridSize + 1))
            {
                var currentLine = lines[i];
                var number = int.Parse(Regex.Match(currentLine, @"^Tile (\d+)").Groups[1].Value);
                var imageData = new char[TileGridSize, TileGridSize];

                i++;

                for (var row = 0; row < TileGridSize; row++)
                {
                    currentLine = lines[i + row];

                    for (var col = 0; col < currentLine.Length; col++)
                    {
                        imageData[row, col] = currentLine[col];
                    }
                }

                var tile = new Tile(number, imageData, TileGridSize, 0, false);
                tiles.Add(tile);
            }

            return tiles;
        }

        private class Image
        {
            public char[,] ImageData { get; private set; }
            public int GridSize { get; private set; }
            public int Orientation { get; private set; }
            public bool Flipped { get; private set; }

            public Image(char[,] imageData, int gridSize, int orientation, bool flipped)
            {
                ImageData = imageData;
                GridSize = gridSize;
                Orientation = orientation;
                Flipped = flipped;
            }

            public void Print()
            {
                for (var row = 0; row < GridSize; row++)
                {
                    for (var col = 0; col < GridSize; col++)
                    {
                        Console.Write("{0} ", ImageData[row, col]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            public override string ToString()
            {
                return $"Orientation = {Orientation} - Flipped = {Flipped}";
            }
        }

        private class Tile : Image
        {
            public int Number { get; set; }
            public Tile TopTile { get; set; }
            public Tile RightTile { get; set; }
            public Tile BottomTile { get; set; }
            public Tile LeftTile { get; set; }

            public Tile(int number, char[,] imageData, int gridSize, int orientation, bool flipped)
                : base(imageData, gridSize, orientation, flipped)
            {
                Number = number;
            }

            public string GetTopBorder()
            {
                var border = string.Empty;

                for (var col = 0; col < GridSize; col++)
                {
                    border += ImageData[0, col];
                }

                return border;
            }

            public string GetRightBorder()
            {
                var border = string.Empty;

                for (var row = 0; row < GridSize; row++)
                {
                    border += ImageData[row, GridSize - 1];
                }

                return border;
            }

            public string GetBottomBorder()
            {
                var border = string.Empty;

                for (var col = 0; col < GridSize; col++)
                {
                    border += ImageData[GridSize - 1, col];
                }

                return border;
            }

            public string GetLeftBorder()
            {
                var border = string.Empty;

                for (var row = 0; row < GridSize; row++)
                {
                    border += ImageData[row, 0];
                }

                return border;
            }

            public override string ToString()
            {
                return $"Tile = {Number} - Orientation = {Orientation} - Flipped = {Flipped}";
            }
        }
    }
}