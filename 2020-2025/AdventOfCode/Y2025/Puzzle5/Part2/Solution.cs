namespace AdventOfCode.Y2025.Puzzle5.Part2
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var ranges = new List<(long Start, long End)>();

            foreach (var line in lines)
            {
                if (line.Contains('-'))
                {
                    var split = line.Split('-');
                    ranges.Add((long.Parse(split[0]), long.Parse(split[1])));
                }
            }

            ranges.Sort((a, b) => a.Start.CompareTo(b.Start));

            // Merge overlapping ranges
            var mergedRanges = new List<(long Start, long End)>();

            foreach (var range in ranges)
            {
                if (!mergedRanges.Any() || range.Start > mergedRanges[^1].End + 1)
                    mergedRanges.Add(range);
                else
                {
                    var lastRange = mergedRanges[^1];
                    mergedRanges[^1] = (lastRange.Start, Math.Max(lastRange.End, range.End));
                }
            }

            var totalFreshIngredients = 0L;
            foreach (var mergedRange in mergedRanges)
                totalFreshIngredients += mergedRange.End - mergedRange.Start + 1;

            Console.WriteLine(totalFreshIngredients);
        }
    }
}