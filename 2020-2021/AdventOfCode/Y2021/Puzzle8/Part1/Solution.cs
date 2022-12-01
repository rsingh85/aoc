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
                var digitLengthsOfInterest = new List<int> { 2, 4, 3, 7 };

                foreach (var digit in digits)
                {
                    if (digitLengthsOfInterest.Contains(digit.Length))
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
        }
    }
}