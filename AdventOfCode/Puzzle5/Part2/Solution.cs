using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzle5.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var boardingPasses = File.ReadAllLines(@"Puzzle5\Part2\Input.txt");
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

            var mySeatId = FindMySeatId(seatIdentifiers);
            Console.WriteLine(mySeatId);
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

        private int FindMySeatId(IEnumerable<int> seatIdentifiers)
        {
            var orderedSeatIds = seatIdentifiers.OrderBy(i => i).ToList();
            var mySeatId = -1;

            for (var i = 1; i < orderedSeatIds.Count - 1 && mySeatId == -1; i++)
            {
                if (orderedSeatIds[i] != (orderedSeatIds[i - 1] + 1))
                {
                    mySeatId = orderedSeatIds[i] - 1;
                }
            }

            return mySeatId;
        }
    }
}
