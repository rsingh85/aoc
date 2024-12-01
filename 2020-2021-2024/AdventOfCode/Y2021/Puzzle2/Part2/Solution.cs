using System;
using System.IO;

namespace AdventOfCode.Y2021.Puzzle2.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var commands = File.ReadAllLines(Helper.GetInputFilePath(this));
            int h = 0, d = 0, a = 0;

            foreach (var command in commands)
            {
                var commandSplit = command.Split(' ');
                var direction = commandSplit[0];
                var amount = int.Parse(commandSplit[1]);

                switch (direction)
                {
                    case "forward":
                        h += amount;
                        d += a * amount;
                        break;
                    case "down":
                        a += amount;
                        break;
                    case "up":
                        a -= amount;
                        break;
                }
            }

            Console.WriteLine(h * d);
        }
    }
}