using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle22.Part2
{
    public class Solution : ISolution
    {
        private readonly List<int> _playerOneInitialDeck = new List<int>();
        private readonly List<int> _playerTwoInitialDeck = new List<int>();

        public void Run()
        {
            ParseInput(File.ReadAllLines(Helper.GetInputFilePath(this)));

            var playerOneWins = PlayGame(_playerOneInitialDeck, _playerTwoInitialDeck, game: 1);

            Console.WriteLine(playerOneWins ? "Player 1 wins!" : "Player 2 wins!");

            long score = playerOneWins ? GetDeckHash(_playerOneInitialDeck) : GetDeckHash(_playerTwoInitialDeck);
            Console.WriteLine(score);
        }

        private bool PlayGame(List<int> playerOneDeck, List<int> playerTwoDeck, int game)
        {
            Console.WriteLine("=== Game {0} ==={1}", game, Environment.NewLine);

            // each game has a separate cache
            var gameCache = new List<GameRoundPlayerDeckCache>();

            // round counter for this game
            var round = 1;

            while (true)
            {
                Log(string.Format($"-- Round {round} (Game {game}) --"));

                if (gameCache.Any(c =>
                        playerOneDeck.SequenceEqual(c.PlayerOneDeck)
                        && playerTwoDeck.SequenceEqual(c.PlayerTwoDeck)))
                {
                    Log(string.Format("The winner of game {0} is player 1!", game));
                    return true;
                }

                // cache this configuration
                gameCache.Add(new GameRoundPlayerDeckCache
                {
                    PlayerOneDeck = playerOneDeck.GetRange(0, playerOneDeck.Count),
                    PlayerTwoDeck = playerTwoDeck.GetRange(0, playerTwoDeck.Count)
                });

                Log(string.Format("Player 1's deck: {0}", string.Join(", ", playerOneDeck)));
                Log(string.Format("Player 2's deck: {0}", string.Join(", ", playerTwoDeck)));

                // play round
                var playerOneTopCard = playerOneDeck.First();
                var playerTwoTopCard = playerTwoDeck.First();

                playerOneDeck.RemoveAt(0);
                playerTwoDeck.RemoveAt(0);

                Log(string.Format("Player 1 plays: {0}", playerOneTopCard));
                Log(string.Format("Player 2 plays: {0}", playerTwoTopCard));

                var playerOneWins = false;

                if (playerOneDeck.Count >= playerOneTopCard
                        && playerTwoDeck.Count >= playerTwoTopCard)
                {
                    Log(string.Format("Playing a sub-game to determine the winner..."));

                    playerOneWins = PlayGame(
                        playerOneDeck.GetRange(0, playerOneTopCard),
                        playerTwoDeck.GetRange(0, playerTwoTopCard),
                        game + 1);

                    Log(string.Format("... anyway back to game {0}", game));

                    if (playerOneWins)
                    {
                        Log(string.Format("Player 1 wins round {0} of game {1}!", round, game));
                        playerOneDeck.Add(playerOneTopCard);
                        playerOneDeck.Add(playerTwoTopCard);
                    }
                    else
                    {
                        Log(string.Format("Player 2 wins round {0} of game {1}!", round, game));
                        playerTwoDeck.Add(playerTwoTopCard);
                        playerTwoDeck.Add(playerOneTopCard);
                    }
                }
                else
                {
                    if (playerOneTopCard > playerTwoTopCard)
                    {
                        Log(string.Format("Player 1 wins round {0} of game {1}!", round, game));
                        playerOneWins = true;
                        playerOneDeck.Add(playerOneTopCard);
                        playerOneDeck.Add(playerTwoTopCard);
                    }
                    else if (playerTwoTopCard > playerOneTopCard)
                    {
                        Log(string.Format("Player 2 wins round {0} of game {1}!", round, game));
                        playerOneWins = false;
                        playerTwoDeck.Add(playerTwoTopCard);
                        playerTwoDeck.Add(playerOneTopCard);
                    }
                }

                if (!playerOneDeck.Any())
                {
                    Log(string.Format("The winner of game {0} is player 2!", game));
                    return false;
                }

                if (!playerTwoDeck.Any())
                {
                    Log(string.Format("The winner of game {0} is player 1!", game));
                    return true;
                }

                round++;
            }
        }

        private long GetDeckHash(List<int> deck)
        {
            long winningPlayersScore = 0;

            for (var i = deck.Count - 1; i >= 0; i--)
            {
                winningPlayersScore += deck[i] * (deck.Count - i);
            }

            return winningPlayersScore;
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
                    _playerOneInitialDeck.Add(int.Parse(line));
                }
                else
                {
                    _playerTwoInitialDeck.Add(int.Parse(line));
                }
            }
        }

        private void Log(string message)
        {
            const bool Debug = false;

            if (Debug)
            {
                Console.WriteLine(message);
            }
        }

        private class GameRoundPlayerDeckCache
        {
            public List<int> PlayerOneDeck { get; set; }
            public List<int> PlayerTwoDeck { get; set; }
        }
    }
}