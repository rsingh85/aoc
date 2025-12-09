namespace AdventOfCode.Y2025.Puzzle5.Part1
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            var ranges = new List<(long Start, long End)>();
            var ingredients = new List<long>();

            foreach (var line in lines)
            {
                if (line.Contains('-'))
                {
                    var split = line.Split('-');
                    ranges.Add((long.Parse(split[0]), long.Parse(split[1])));
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    ingredients.Add(long.Parse(line));
                }
            }

            var freshIngredients = 0;

            foreach (var ingredient in ingredients)
            {
                foreach (var range in ranges)
                {
                    if (ingredient >= range.Start && ingredient <= range.End)
                    {
                        freshIngredients++;
                        break;
                    }
                }
            }

            Console.WriteLine(freshIngredients);
        }
    }
}