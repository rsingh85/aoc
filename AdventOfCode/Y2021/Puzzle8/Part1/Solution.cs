namespace AdventOfCode.Y2021.Puzzle8.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this));
            var count = 0;

            foreach (var line in input)
            {
                var splitLine = line.Split("|");
                var digits = splitLine[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var digit in digits)
                {
                    if (digit.Length == 2 || digit.Length == 4 || digit.Length == 3 || digit.Length == 7)
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }
    }
}