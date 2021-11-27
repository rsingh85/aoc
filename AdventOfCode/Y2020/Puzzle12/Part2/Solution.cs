using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle12.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var instructions =
                File.ReadAllLines(Helper.GetInputFilePath(this))
                    .Select(i => new Instruction(i));

            var eastWestPosition = 0; // east = positive, west = negative
            var northSouthPosition = 0; // north = positive, south = negative

            var waypointEastWestPosition = 10; // east = positive, west = negative
            var waypointNorthSouthPosition = 1; // north = positive, south = negative

            foreach (var instruction in instructions)
            {
                switch (instruction.Action)
                {
                    case 'N':
                        waypointNorthSouthPosition += instruction.Value;
                        break;
                    case 'S':
                        waypointNorthSouthPosition -= instruction.Value;
                        break;
                    case 'E':
                        waypointEastWestPosition += instruction.Value;
                        break;
                    case 'W':
                        waypointEastWestPosition -= instruction.Value;
                        break;
                    case 'L':
                        if (instruction.Value == 90)
                        {
                            var newWaypointEastWestPosition = -waypointNorthSouthPosition;
                            var newWaypointNorthSouthPosition = waypointEastWestPosition;

                            waypointEastWestPosition = newWaypointEastWestPosition;
                            waypointNorthSouthPosition = newWaypointNorthSouthPosition;
                        }
                        else if (instruction.Value == 180)
                        {
                            var newWaypointEastWestPosition = -waypointEastWestPosition;
                            var newWaypointNorthSouthPosition = -waypointNorthSouthPosition;

                            waypointEastWestPosition = newWaypointEastWestPosition;
                            waypointNorthSouthPosition = newWaypointNorthSouthPosition;
                        }
                        else if (instruction.Value == 270)
                        {
                            var newWaypointEastWestPosition = waypointNorthSouthPosition;
                            var newWaypointNorthSouthPosition = -waypointEastWestPosition;

                            waypointEastWestPosition = newWaypointEastWestPosition;
                            waypointNorthSouthPosition = newWaypointNorthSouthPosition;
                        }
                        break;
                    case 'R':
                        if (instruction.Value == 90)
                        {
                            var newWaypointEastWestPosition = waypointNorthSouthPosition;
                            var newWaypointNorthSouthPosition = -waypointEastWestPosition;

                            waypointEastWestPosition = newWaypointEastWestPosition;
                            waypointNorthSouthPosition = newWaypointNorthSouthPosition;
                        }
                        else if (instruction.Value == 180)
                        {
                            var newWaypointEastWestPosition = -waypointEastWestPosition;
                            var newWaypointNorthSouthPosition = -waypointNorthSouthPosition;

                            waypointEastWestPosition = newWaypointEastWestPosition;
                            waypointNorthSouthPosition = newWaypointNorthSouthPosition;
                        }
                        else if (instruction.Value == 270)
                        {
                            var newWaypointEastWestPosition = -waypointNorthSouthPosition;
                            var newWaypointNorthSouthPosition = waypointEastWestPosition;

                            waypointEastWestPosition = newWaypointEastWestPosition;
                            waypointNorthSouthPosition = newWaypointNorthSouthPosition;
                        }
                        break;
                    case 'F':
                        eastWestPosition += waypointEastWestPosition * instruction.Value;
                        northSouthPosition += waypointNorthSouthPosition * instruction.Value;

                        break;
                }
            }

            var manhattenDistance = Math.Abs(eastWestPosition) + Math.Abs(northSouthPosition);

            Console.WriteLine(manhattenDistance);
        }

        private enum Direction
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }
        private class Instruction
        {
            public char Action { get; set; }
            public int Value { get; set; }

            public Instruction(string instructionAsString)
            {
                Action = instructionAsString[0];
                Value = int.Parse(Regex.Match(instructionAsString, @"\d+$").Value);
            }

            public override string ToString()
            {
                return $"{Action}{Value}";
            }
        }
    }
}