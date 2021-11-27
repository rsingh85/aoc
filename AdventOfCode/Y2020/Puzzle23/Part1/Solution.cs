using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle23.Part1
{
    public class Solution : ISolution
    {
        private List<int> _cups = new List<int>();

        public void Run()
        {
            ParseInput(File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution))));

            var currentCup = _cups[0];
            var currentMove = 1;
            const int MaxMoves = 100;

            while (currentMove <= MaxMoves)
            {
                var firstCupIndex = (_cups.IndexOf(currentCup) + 1) % _cups.Count;
                var secondCupIndex = (_cups.IndexOf(currentCup) + 2) % _cups.Count;
                var thirdCupIndex = (_cups.IndexOf(currentCup) + 3) % _cups.Count;

                var firstCup = _cups[firstCupIndex];
                var secondCup = _cups[secondCupIndex];
                var thirdCup = _cups[thirdCupIndex];

                _cups.Remove(firstCup);
                _cups.Remove(secondCup);
                _cups.Remove(thirdCup);

                var destinationCup = currentCup - 1;

                if (destinationCup == 0)
                {
                    destinationCup = _cups.Max();
                }

                while (destinationCup == firstCup ||
                    destinationCup == secondCup ||
                    destinationCup == thirdCup)
                {
                    destinationCup--;

                    if (destinationCup < _cups.Min())
                    {
                        destinationCup = _cups.Max();
                    }
                }

                var indexOfDestinationCup = _cups.IndexOf(destinationCup);

                _cups.Insert(indexOfDestinationCup + 1, firstCup);
                _cups.Insert(indexOfDestinationCup + 2, secondCup);
                _cups.Insert(indexOfDestinationCup + 3, thirdCup);

                var nextCupIndex = _cups.IndexOf(currentCup) + 1;

                if (nextCupIndex >= _cups.Count())
                {
                    nextCupIndex = 0;
                }

                currentCup = _cups[nextCupIndex];
                currentMove++;
            }

            Console.WriteLine("Complete! Manually input the numbers that appear 'after' 1!");
            Console.WriteLine(string.Join(" ", _cups));
        }

        private void ParseInput(string[] lines)
        {
            if (lines.Length != 1)
            {
                throw new ArgumentException("Only one line expected in the input");
            }

            var cups = lines.First();

            foreach (var cup in cups)
            {
                _cups.Add(int.Parse(cup.ToString()));
            }
        }
    }
}