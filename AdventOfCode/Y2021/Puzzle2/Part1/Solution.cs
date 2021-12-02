using System;
using System.IO;

namespace AdventOfCode.Y2021.Puzzle2.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var commands = File.ReadAllLines(Helper.GetInputFilePath(this));
            int h = 0, d = 0;

            foreach (var command in commands)
            {
                var commandSplit = command.Split(' ');
                var direction = commandSplit[0];
                var amount = int.Parse(commandSplit[1]);

                switch (direction)
                {
                    case "forward":
                        h += amount;
                        break;
                    case "down":
                        d += amount;
                        break;
                    case "up":
                        d -= amount;
                        break;
                }
            }

            Console.WriteLine(h * d);
        }
    }
}