using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle22.Part1
{
    public class Solution : ISolution
    {
        private List<int> _playerOneDeck = new List<int>();
        private List<int> _playerTwoDeck = new List<int>();

        // top card index == deck.First()
        // bottom card inde == Ldeck.ast()

        public void Run()
        {
            ParseInput(File.ReadAllLines(Helper.GetInputFilePath(this)));

            var round = 1;

            while (_playerOneDeck.Any() && _playerTwoDeck.Any())
            {
                var playerOneTopCard = _playerOneDeck.First();
                var playerTwoTopCard = _playerTwoDeck.First();

                _playerOneDeck.RemoveAt(0);
                _playerTwoDeck.RemoveAt(0);

                if (playerOneTopCard > playerTwoTopCard)
                {
                    // player one wins round
                    _playerOneDeck.Add(playerOneTopCard);
                    _playerOneDeck.Add(playerTwoTopCard);
                }
                else if (playerTwoTopCard > playerOneTopCard)
                {
                    // player two wins round
                    _playerTwoDeck.Add(playerTwoTopCard);
                    _playerTwoDeck.Add(playerOneTopCard);
                }

                round++;
            }

            long winningPlayersScore = 0;
            var winningPlayersDeck = _playerOneDeck.Any() ? _playerOneDeck : _playerTwoDeck;

            for (var i = winningPlayersDeck.Count - 1; i >= 0; i--)
            {
                winningPlayersScore += winningPlayersDeck[i] * (winningPlayersDeck.Count - i);
            }

            Console.WriteLine(winningPlayersScore);
        }

        private void ParseInput(string[] lines)
        {
            var playerTwoDeckLine = false;

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line) || line == "Player 2:")
                {
                    playerTwoDeckLine = true;
                    continue;
                }

                if (!playerTwoDeckLine)
                {
                    _playerOneDeck.Add(int.Parse(line));
                }
                else
                {
                    _playerTwoDeck.Add(int.Parse(line));
                }
            }
        }
    }
}