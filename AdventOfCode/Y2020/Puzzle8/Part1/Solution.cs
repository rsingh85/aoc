using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020.Puzzle8.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var program = File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution)));
            var executedLines = new List<bool>(program.Length);

            for (var i = 0; i < program.Length; i++)
            {
                executedLines.Add(false);
            }

            var accumulator = 0;

            for (var i = 0; i < program.Length; i++)
            {
                if (executedLines[i])
                {
                    break;
                }

                var instruction = program[i];
                var command = instruction.Substring(0, 3);
                var signedNumber = int.Parse(Regex.Match(instruction, @"[\+\-]\d+$").Value);

                executedLines[i] = true;

                switch (command)
                {
                    case "acc":
                        accumulator += signedNumber;
                        break;
                    case "jmp":
                        signedNumber--;
                        i += signedNumber;
                        break;
                }
            }

            Console.WriteLine(accumulator);
        }
    }
}