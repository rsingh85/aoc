using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle12.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var instructions =
                File.ReadAllLines(@"Y2020\Puzzle12\Part1\Input.txt")
                    .Select(i => new Instruction(i));

            var currentDirection = Direction.East;

            var eastWestPosition = 0; // east = positive, west = negative
            var northSouthPosition = 0; // north = positive, south = negative

            foreach (var instruction in instructions)
            {
                switch (instruction.Action)
                {
                    case 'N':
                        northSouthPosition += instruction.Value;
                        break;
                    case 'S':
                        northSouthPosition -= instruction.Value;
                        break;
                    case 'E':
                        eastWestPosition += instruction.Value;
                        break;
                    case 'W':
                        eastWestPosition -= instruction.Value;
                        break;
                    case 'L':
                        var leftTurns = instruction.Value / 90;
                        currentDirection = GetNewDirection(currentDirection, instruction.Action, leftTurns);
                        break;
                    case 'R':
                        var rightTurns = instruction.Value / 90;
                        currentDirection = GetNewDirection(currentDirection, instruction.Action, rightTurns);
                        break;
                    case 'F':
                        if (currentDirection == Direction.East || currentDirection == Direction.West)
                        {
                            eastWestPosition += (currentDirection == Direction.East) ?
                                instruction.Value :
                                -instruction.Value;
                        }

                        if (currentDirection == Direction.North || currentDirection == Direction.South)
                        {
                            northSouthPosition += (currentDirection == Direction.North) ?
                                instruction.Value :
                                -instruction.Value;
                        }

                        break;
                }
            }

            var manhattenDistance = Math.Abs(eastWestPosition) + Math.Abs(northSouthPosition);

            Console.WriteLine(manhattenDistance);
        }

        private Direction GetNewDirection(Direction currentDirection, char leftOrRight, int turns)
        {
            var newDirection = currentDirection;

            for (var i = 0; i < turns; i++)
            {
                switch (newDirection)
                {
                    case Direction.North:
                        newDirection = (leftOrRight == 'L') ? Direction.West : Direction.East;
                        break;
                    case Direction.East:
                        newDirection = (leftOrRight == 'L') ? Direction.North : Direction.South;
                        break;
                    case Direction.South:
                        newDirection = (leftOrRight == 'L') ? Direction.East : Direction.West;
                        break;
                    case Direction.West:
                        newDirection = (leftOrRight == 'L') ? Direction.South : Direction.North;
                        break;
                }
            }

            return newDirection;
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