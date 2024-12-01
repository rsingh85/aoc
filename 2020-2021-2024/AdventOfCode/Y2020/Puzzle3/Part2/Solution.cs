using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Y2020.Puzzle3.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var grid = File.ReadAllLines(Helper.GetInputFilePath(this));

            var routes = new List<Route>
                         {
                            new Route { Right = 1, Down = 1 },
                            new Route { Right = 3, Down = 1 },
                            new Route { Right = 5, Down = 1 },
                            new Route { Right = 7, Down = 1 },
                            new Route { Right = 1, Down = 2 }
                         };

            foreach (var route in routes)
            {
                var colIndex = route.Right;
                var treesEncountered = 0;

                for (var rowIndex = route.Down; rowIndex < grid.Length; rowIndex += route.Down)
                {
                    var square = grid[rowIndex][colIndex];

                    if (square == '#')
                    {
                        treesEncountered++;
                    }

                    colIndex = (colIndex + route.Right) % grid[0].Length;
                }

                route.TreesEncountered = treesEncountered;
            }

            Console.WriteLine((long)routes[0].TreesEncountered *
                              routes[1].TreesEncountered *
                              routes[2].TreesEncountered *
                              routes[3].TreesEncountered *
                              routes[4].TreesEncountered);
        }

        private class Route
        {
            public int Right { get; set; }
            public int Down { get; set; }
            public long TreesEncountered { get; set; }
        }
    }
}