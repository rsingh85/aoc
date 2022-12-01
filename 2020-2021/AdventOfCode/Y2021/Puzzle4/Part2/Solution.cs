using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2021.Puzzle4.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this));

            var draw = input.First().Split(',').Select(int.Parse);
            var boards = ReadBoards(input.Skip(1));
            Board lastWinningBoard = null;

            foreach (var number in draw)
            {
                foreach (var board in boards.Where(b => !b.Won))
                {
                    board.Mark(number);

                    if (board.Won)
                    {
                        lastWinningBoard = board;
                    }
                }
            }

            Console.WriteLine(lastWinningBoard.Score);
        }

        private List<Board> ReadBoards(IEnumerable<string> input)
        {
            var boards = new List<Board>();
            Board currentBoard = null;
            var currentRowIndex = 0;

            foreach (var row in input)
            {
                if (string.IsNullOrWhiteSpace(row))
                {
                    currentBoard = new Board(boardSize: 5);
                    currentRowIndex = 0;
                    boards.Add(currentBoard);
                }
                else
                {
                    currentBoard.AddRow(row.Replace("  ", " ").Trim(), currentRowIndex);
                    currentRowIndex++;
                }
            }

            return boards;
        }
    }

    public class Board
    {
        private BoardSquare[,] _board;
        private int _boardSize;
        
        public bool Won { get; private set; }
        public int Score { get; private set; }

        public Board(int boardSize)
        {
            _board = new BoardSquare[boardSize, boardSize];
            _boardSize = boardSize;
        }

        public void AddRow(string rowNumbers, int rowIndex)
        {
            var numbers = rowNumbers.Split(' ').Select(int.Parse).ToArray();

            for (var colIndex = 0; colIndex < _boardSize; colIndex++)
            {
                _board[rowIndex, colIndex] = new BoardSquare { Number = numbers[colIndex] };
            }
        }

        public void Mark(int drawNumber)
        {
            for (var row = 0; row < _boardSize; row++)
            {
                for (var col = 0; col < _boardSize; col++)
                {
                    var square = _board[row, col];

                    if (square.Number == drawNumber)
                    {
                        square.Marked = true;

                        if (IsRowComplete(row) || IsColumnComplete(col))
                        {
                            Won = true;
                            Score = SumUnmarked() * drawNumber;
                        }
                    }
                }
            }
        }

        private bool IsRowComplete(int row)
        {
            for (var col = 0; col < _boardSize; col++)
            {
                if (!_board[row, col].Marked)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsColumnComplete(int col)
        {
            for (var row = 0; row < _boardSize; row++)
            {
                if (!_board[row, col].Marked)
                {
                    return false;
                }
            }

            return true;
        }

        private int SumUnmarked()
        {
            var sum = 0;

            for (var row = 0; row < _boardSize; row++)
            {
                for (var col = 0; col < _boardSize; col++)
                {
                    var square = _board[row, col];

                    if (!square.Marked)
                    {
                        sum += square.Number;
                    }
                }
            }

            return sum;
        }
    }

    public class BoardSquare
    {
        public int Number { get; set; }
        public bool Marked { get; set; }
    }
}