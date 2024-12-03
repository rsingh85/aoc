using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Puzzle3.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var memoryLines = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();
            var memory = string.Join(string.Empty, memoryLines);
            var matches = Regex.Matches(memory, @"mul\((?<n1>\d+),(?<n2>\d+)\)");

            long sum = 0;

            foreach (Match match in matches)
            {
                var lastIndexOfDo = memory.Substring(0, match.Index).LastIndexOf("do()");
                var lastIndexOfDont = memory.Substring(0, match.Index).LastIndexOf("don't()");
                var isInstructionEnabled = true;

                if (lastIndexOfDont > lastIndexOfDo)
                    isInstructionEnabled = false;

                if (isInstructionEnabled)
                {
                    sum += int.Parse(match.Groups["n1"].Value) * int.Parse(match.Groups["n2"].Value);
                }
            }
            
            Console.WriteLine(sum);
        }
    }
}