using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle24.Part1
{
    public class Solution : ISolution
    {
        /*
         * Hex tiles coordinate system:
         * 
         *      (-1,1) (0,1)
         * (-1,0 )  (0,0)  (1,0)
         *      (0,-1) (1,-1)
         */
        public void Run()
        {
            // white == false, black == true
            var tiles = new Dictionary<string, bool>();

            var directions = File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution)));

            foreach (var direction in directions)
            {
                int x = 0, y = 0;

                for (var d = 0; d < direction.Length; d++)
                {
                    var directionChar = direction[d];

                    switch (directionChar)
                    {
                        case 'e':
                            x++;
                            break;
                        case 'w':
                            x--;
                            break;
                        case 's':
                            switch (direction[++d])
                            {
                                case 'e':
                                    x++;
                                    y--;
                                    break;
                                case 'w':
                                    y--;
                                    break;
                            }
                            break;
                        case 'n':
                            switch (direction[++d])
                            {
                                case 'e':
                                    y++;
                                    break;
                                case 'w':
                                    x--;
                                    y++;
                                    break;
                            }
                            break;
                    }
                }

                var tileKey = $"{{{x},{y}}}";
                tiles[tileKey] = tiles.ContainsKey(tileKey) ?
                    !tiles[tileKey] : true;
            }

            Console.WriteLine(tiles.Values.Where(v => v).Count());
        }
    }
}