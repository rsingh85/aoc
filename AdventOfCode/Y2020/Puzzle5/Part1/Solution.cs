using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020.Puzzle5.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var boardingPasses = File.ReadAllLines(Helper.GetInputFilePath(this));
            var seatIdentifiers = new List<int>();

            foreach (var boardingPass in boardingPasses)
            {
                var rowLocationCharacters = Regex.Match(boardingPass, "^[FB]{7}").Value;
                var seatRow = FindSeatRow(rowLocationCharacters);

                var columnLocationCharacters = Regex.Match(boardingPass, "[LR]{3}$").Value;
                var seatColumn = FindSeatColumn(columnLocationCharacters);

                var seatId = (seatRow * 8) + seatColumn;

                seatIdentifiers.Add(seatId);
            }

            Console.WriteLine(seatIdentifiers.Max());
        }

        private int FindSeatRow(string rowLocationCharacters)
        {
            int min = 0, max = 127;
            var currentCharacterIndex = 0;

            while (min != max - 1)
            {
                var currentCharacter = rowLocationCharacters[currentCharacterIndex];

                switch (currentCharacter)
                {
                    case 'F':
                        max = (int)Math.Floor((double)(min + max) / 2);
                        break;
                    case 'B':
                        min = (int)Math.Ceiling((double)(min + max) / 2);
                        break;
                }

                currentCharacterIndex++;
            }

            return rowLocationCharacters[rowLocationCharacters.Length - 1] == 'F' ?
                min : max;
        }

        private int FindSeatColumn(string columnLocationCharacters)
        {
            int min = 0, max = 7;
            var currentCharacterIndex = 0;

            while (min != max - 1)
            {
                var currentCharacter = columnLocationCharacters[currentCharacterIndex];

                switch (currentCharacter)
                {
                    case 'L':
                        max = (int)Math.Floor((double)(min + max) / 2);
                        break;
                    case 'R':
                        min = (int)Math.Ceiling((double)(min + max) / 2);
                        break;
                }

                currentCharacterIndex++;
            }

            return columnLocationCharacters[columnLocationCharacters.Length - 1] == 'L' ?
                min : max;
        }
    }
}
