using System.Text.RegularExpressions;

namespace AdventOfCode.Y2025.Puzzle2.Part1
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var ranges = File.ReadAllLines(Helper.GetInputFilePath(this))[0]
                .Split(",")
                .Select(r => {
                    var parts = r.Split("-");
                    return (start: long.Parse(parts[0]), end: long.Parse(parts[1]));
                })
                .ToList();

            long sum = 0;

            foreach (var range in ranges)
            {
                for (long number = range.start; number <= range.end; number++)
                    if (IsInvalid(number))
                        sum += number;
            }

            Console.WriteLine(sum);
        }

        private static bool IsInvalid(long number)
        {
            var match = RepeatingNumbersPattern().Match(number.ToString());

            return match.Success && 
                string.IsNullOrEmpty(number.ToString().Replace(match.Value, string.Empty));
        }

        [GeneratedRegex(@"^(\d+)\1")]
        private static partial Regex RepeatingNumbersPattern();
    }
}