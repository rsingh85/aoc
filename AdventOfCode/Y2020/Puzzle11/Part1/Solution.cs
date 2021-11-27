using System;
using System.IO;

namespace AdventOfCode.Y2020.Puzzle11.Part1
{
    public class Solution : ISolution
    {
        private string[] input;
        public void Run()
        {
            input = File.ReadAllLines(@"Y2020\Puzzle11\Part1\Input.txt");

            var originalSeatLayout = new char[input.Length, input[0].Length];

            // Read input into 2d array
            for (var rowIndex = 0; rowIndex < input.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < input[rowIndex].Length; colIndex++)
                {
                    originalSeatLayout[rowIndex, colIndex] = input[rowIndex][colIndex];
                }
            }

            var changesInIteration = 1;
            char[,] previousSeatLayout = null;
            char[,] newSeatLayout = null;
            var iterationCount = 0;

            while (changesInIteration > 0)
            {
                changesInIteration = 0;
                previousSeatLayout = newSeatLayout == null ? originalSeatLayout : newSeatLayout;
                newSeatLayout = new char[input.Length, input[0].Length];

                for (var rowIndex = 0; rowIndex < input.Length; rowIndex++)
                {
                    for (var colIndex = 0; colIndex < input[rowIndex].Length; colIndex++)
                    {
                        var originalSeatState = previousSeatLayout[rowIndex, colIndex];

                        if (originalSeatState == 'L' && GetAdjacentOccupiedSeatCount(previousSeatLayout, input.Length, input[0].Length, rowIndex, colIndex) == 0)
                        {
                            newSeatLayout[rowIndex, colIndex] = '#';
                        }
                        else if (originalSeatState == '#' && GetAdjacentOccupiedSeatCount(previousSeatLayout, input.Length, input[0].Length, rowIndex, colIndex) >= 4)
                        {
                            newSeatLayout[rowIndex, colIndex] = 'L';
                        }
                        else
                        {
                            newSeatLayout[rowIndex, colIndex] = originalSeatState;
                        }

                        if (newSeatLayout[rowIndex, colIndex] != previousSeatLayout[rowIndex, colIndex])
                        {
                            changesInIteration++;
                        }
                    }
                }

                iterationCount++;
            }

            // count occupied seats on last seat layout
            var occupiedSeats = 0;

            for (var rowIndex = 0; rowIndex < input.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < input[rowIndex].Length; colIndex++)
                {
                    occupiedSeats += newSeatLayout[rowIndex, colIndex] == '#' ? 1 : 0;
                }
            }

            Console.WriteLine(occupiedSeats);
        }

        private void PrintSeatLayout(char[,] newSeatLayout)
        {
            for (var rowIndex = 0; rowIndex < input.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < input[0].Length; colIndex++)
                {
                    Console.Write(newSeatLayout[rowIndex, colIndex]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private int GetAdjacentOccupiedSeatCount(char[,] originalSeatLayout, int rowSize, int colSize, int seatRowIndex, int seatColIndex)
        {
            var occupiedAdjacentSeats = 0;

            // up
            if (seatRowIndex - 1 >= 0 &&
                originalSeatLayout[seatRowIndex - 1, seatColIndex] == '#')
            {
                occupiedAdjacentSeats++;
            }

            // up right (diagonal)
            if (seatRowIndex - 1 >= 0 &&
                seatColIndex + 1 < colSize &&
                originalSeatLayout[seatRowIndex - 1, seatColIndex + 1] == '#')
            {
                occupiedAdjacentSeats++;
            }

            // right
            if (seatColIndex + 1 < colSize &&
                originalSeatLayout[seatRowIndex, seatColIndex + 1] == '#')
            {
                occupiedAdjacentSeats++;
            }

            // down right (diagonal)
            if (seatRowIndex + 1 < rowSize &&
                seatColIndex + 1 < colSize &&
                originalSeatLayout[seatRowIndex + 1, seatColIndex + 1] == '#')
            {
                occupiedAdjacentSeats++;
            }

            // down
            if (seatRowIndex + 1 < rowSize &&
                originalSeatLayout[seatRowIndex + 1, seatColIndex] == '#')
            {
                occupiedAdjacentSeats++;
            }

            // down left (diagonal)
            if (seatRowIndex + 1 < rowSize &&
                seatColIndex - 1 >= 0 &&
                originalSeatLayout[seatRowIndex + 1, seatColIndex - 1] == '#')
            {
                occupiedAdjacentSeats++;
            }

            // left
            if (seatColIndex - 1 >= 0 &&
                originalSeatLayout[seatRowIndex, seatColIndex - 1] == '#')
            {
                occupiedAdjacentSeats++;
            }

            // up left (diagonal)
            if (seatRowIndex - 1 >= 0 &&
                seatColIndex - 1 >= 0 &&
                originalSeatLayout[seatRowIndex - 1, seatColIndex - 1] == '#')
            {
                occupiedAdjacentSeats++;
            }

            return occupiedAdjacentSeats;
        }
    }
}