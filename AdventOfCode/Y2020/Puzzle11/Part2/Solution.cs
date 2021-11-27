using System;
using System.IO;

namespace AdventOfCode.Puzzle11.Part2
{
    public class Solution : ISolution
    {
        private string[] input;

        public void Run()
        {
            input = File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution)));
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
                previousSeatLayout = (newSeatLayout == null) ? originalSeatLayout : newSeatLayout;
                newSeatLayout = new char[input.Length, input[0].Length];

                for (var rowIndex = 0; rowIndex < input.Length; rowIndex++)
                {
                    for (var colIndex = 0; colIndex < input[rowIndex].Length; colIndex++)
                    {
                        var originalSeatState = previousSeatLayout[rowIndex, colIndex];

                        if (originalSeatState == 'L' && GetVisibleOccupiedSeatCount(previousSeatLayout, input.Length, input[0].Length, rowIndex, colIndex) == 0)
                        {
                            newSeatLayout[rowIndex, colIndex] = '#';
                        }
                        else if (originalSeatState == '#' && GetVisibleOccupiedSeatCount(previousSeatLayout, input.Length, input[0].Length, rowIndex, colIndex) >= 5)
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
            var occupiedSeats = 0;

            for (var rowIndex = 0; rowIndex < input.Length; rowIndex++)
            {
                for (var colIndex = 0; colIndex < input[rowIndex].Length; colIndex++)
                {
                    occupiedSeats += (newSeatLayout[rowIndex, colIndex] == '#') ? 1 : 0;
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

        private int GetVisibleOccupiedSeatCount(char[,] originalSeatLayout, int rowSize, int colSize, int seatRowIndex, int seatColIndex)
        {
            var occupiedVisibleSeats = 0;
            var movingColIndex = 0;

            // up
            for (var rowIndex = seatRowIndex - 1; rowIndex >= 0; rowIndex--)
            {
                if (originalSeatLayout[rowIndex, seatColIndex] == '#')
                {
                    occupiedVisibleSeats++;
                    break;
                }

                if (originalSeatLayout[rowIndex, seatColIndex] == 'L')
                {
                    break;
                }
            }

            // up right (diagonal)
            movingColIndex = seatColIndex + 1;
            for (var rowIndex = seatRowIndex - 1; rowIndex >= 0; rowIndex--)
            {
                if (movingColIndex == colSize)
                {
                    break;
                }

                if (originalSeatLayout[rowIndex, movingColIndex] == '#')
                {
                    occupiedVisibleSeats++;
                    break;
                }

                if (originalSeatLayout[rowIndex, movingColIndex] == 'L')
                {
                    break;
                }
                movingColIndex++;
            }

            // right
            for (var colIndex = seatColIndex + 1; colIndex < colSize; colIndex++)
            {
                if (originalSeatLayout[seatRowIndex, colIndex] == '#')
                {
                    occupiedVisibleSeats++;
                    break;
                }

                if (originalSeatLayout[seatRowIndex, colIndex] == 'L')
                {
                    break;
                }
            }

            // down right (diagonal)
            movingColIndex = seatColIndex + 1;
            for (var rowIndex = seatRowIndex + 1; rowIndex < rowSize; rowIndex++)
            {
                if (movingColIndex == colSize)
                {
                    break;
                }

                if (originalSeatLayout[rowIndex, movingColIndex] == '#')
                {
                    occupiedVisibleSeats++;
                    break;
                }

                if (originalSeatLayout[rowIndex, movingColIndex] == 'L')
                {
                    break;
                }
                movingColIndex++;
            }

            // down
            for (var rowIndex = seatRowIndex + 1; rowIndex < rowSize; rowIndex++)
            {
                if (originalSeatLayout[rowIndex, seatColIndex] == '#')
                {
                    occupiedVisibleSeats++;
                    break;
                }

                if (originalSeatLayout[rowIndex, seatColIndex] == 'L')
                {
                    break;
                }
            }

            // down left (diagonal)
            movingColIndex = seatColIndex - 1;
            for (var rowIndex = seatRowIndex + 1; rowIndex < rowSize; rowIndex++)
            {
                if (movingColIndex == -1)
                {
                    break;
                }

                if (originalSeatLayout[rowIndex, movingColIndex] == '#')
                {
                    occupiedVisibleSeats++;
                    break;
                }

                if (originalSeatLayout[rowIndex, movingColIndex] == 'L')
                {
                    break;
                }
                movingColIndex--;
            }

            // left
            for (var colIndex = seatColIndex - 1; colIndex >= 0; colIndex--)
            {
                if (originalSeatLayout[seatRowIndex, colIndex] == '#')
                {
                    occupiedVisibleSeats++;
                    break;
                }

                if (originalSeatLayout[seatRowIndex, colIndex] == 'L')
                {
                    break;
                }
            }

            // up left (diagonal)
            movingColIndex = seatColIndex - 1;
            for (var rowIndex = seatRowIndex - 1; rowIndex >= 0; rowIndex--)
            {
                if (movingColIndex == -1)
                {
                    break;
                }

                if (originalSeatLayout[rowIndex, movingColIndex] == '#')
                {
                    occupiedVisibleSeats++;
                    break;
                }

                if (originalSeatLayout[rowIndex, movingColIndex] == 'L')
                {
                    break;
                }

                movingColIndex--;
            }

            return occupiedVisibleSeats;
        }
    }
}