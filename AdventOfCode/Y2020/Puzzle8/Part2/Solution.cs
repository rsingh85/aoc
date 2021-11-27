using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzle8.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var program = ParseProgram(File.ReadAllLines(Helper.GetInputFilePath(this)));

            while (!ExecuteProgram(program))
            {
                program = ResetProgramCommands(File.ReadAllLines(Helper.GetInputFilePath(this)), program);
                
                for (var i = 0; i < program.Count; i++)
                {
                    var instruction = program[i];

                    if ((instruction.Command == "nop" || instruction.Command == "jmp") && !instruction.InverseTried)
                    {
                        if (instruction.Command == "nop" && instruction.SignedNumber == 0)
                        {
                            instruction.InverseTried = true;
                            break;
                        }

                        switch (instruction.Command)
                        {
                            case "nop":
                                instruction.Command = "jmp";
                                break;
                            case "jmp":
                                instruction.Command = "nop";
                                break;
                        }

                        instruction.InverseTried = true;
                        break;
                    }
                }
            }
        }

        private List<Instruction> ParseProgram(string[] instructions)
        {
            var program = new List<Instruction>();

            foreach (var instruction in instructions)
            {
                var command = instruction.Substring(0, 3);
                var signedNumber = int.Parse(Regex.Match(instruction, @"[\+\-]\d+$").Value);
                program.Add(new Instruction { Command = command, SignedNumber = signedNumber });
            }

            program.Add(new Instruction { Command = "fin", SignedNumber = 0 });
            return program;
        }

        private List<Instruction> ResetProgramCommands(string[] originalInstructions, List<Instruction> currentInstructions)
        {
            for (var i = 0; i < originalInstructions.Length; i++)
            {
                var command = originalInstructions[i].Substring(0, 3);
                currentInstructions[i].Command = command;
            }

            return currentInstructions;
        }

        private bool ExecuteProgram(List<Instruction> program)
        {
            var executedLines = new List<bool>(program.Count);

            for (var i = 0; i < program.Count; i++)
            {
                executedLines.Add(false);
            }

            var accumulator = 0;
            var finished = false;

            for (var i = 0; i < program.Count; i++)
            {
                if (executedLines[i])
                {
                    return false;
                }

                executedLines[i] = true;
                var instruction = program[i];

                switch (instruction.Command)
                {
                    case "acc":
                        accumulator += instruction.SignedNumber;
                        break;
                    case "jmp":
                        i += instruction.SignedNumber - 1;
                        break;
                    case "fin":
                        finished = true;
                        break;
                }

                if (finished)
                {
                    break;
                }
            }

            Console.WriteLine(accumulator);
            return true;
        }

        private class Instruction
        {
            public string Command { get; set; }
            public int SignedNumber { get; set; }
            public bool InverseTried { get; set; }
        }
    }
}