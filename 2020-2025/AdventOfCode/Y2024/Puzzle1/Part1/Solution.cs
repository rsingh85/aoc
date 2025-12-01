namespace AdventOfCode.Y2024.Puzzle1.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();
            var left = new List<int>(lines.Length);
            var right = new List<int>(lines.Length);

            foreach (var line in lines)
            {
                var split = line.Split("   ");

                left.Add(int.Parse(split[0]));
                right.Add(int.Parse(split[1]));
            }

            left.Sort();
            right.Sort();
            var diffSum = 0;

            for (var i = 0; i < lines.Length; i++)
            {
                diffSum += Math.Max(left[i], right[i]) - Math.Min(left[i], right[i]);
            }

            Console.WriteLine(diffSum);
        }
    }
}