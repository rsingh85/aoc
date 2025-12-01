using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Puzzle3.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var memory = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();
            var sum = 0;

            foreach (var line in memory)
            {
                var matches = Regex.Matches(line, @"mul\((?<n1>\d+),(?<n2>\d+)\)");

                foreach (Match match in matches)
                {
                    sum += int.Parse(match.Groups["n1"].Value) * int.Parse(match.Groups["n2"].Value);
                }
            }


            Console.WriteLine(sum);
        }
    }
}