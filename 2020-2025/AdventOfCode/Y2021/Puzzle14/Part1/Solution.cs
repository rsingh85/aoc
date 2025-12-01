namespace AdventOfCode.Y2021.Puzzle14.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var polymer = lines.First();

            var ruleDictionary = new Dictionary<string, string>();

            foreach (var rule in lines.Skip(2))
            {
                var ruleSplit = rule.Split(" -> ");
                ruleDictionary.Add(ruleSplit[0], ruleSplit[1]);
            }

            var step = 1;

            while (step <= 10)
            {
                var cursor = 0;
                var polymerLength = polymer.Length;

                while (cursor < polymerLength - 1)
                {
                    var currentPair = $"{polymer[cursor]}{polymer[cursor + 1]}";
                    var insertionChar = ruleDictionary[currentPair];

                    if (insertionChar != null)
                    {
                        polymer = polymer.Insert(cursor + 1, insertionChar);
                        polymerLength++;
                        cursor += 2;
                    }
                    else
                    {
                        cursor += 1;
                    }
                }
                step++;
            }

            var characterCountMap =
                polymer
                .GroupBy(x => x)
                .ToDictionary(g => g.Key, g => g.Count());

            var minCharKey = characterCountMap.First(c => c.Value == characterCountMap.Values.Min()).Key;
            var maxCharKey = characterCountMap.First(c => c.Value == characterCountMap.Values.Max()).Key;

            Console.WriteLine(characterCountMap[maxCharKey] - characterCountMap[minCharKey]);
        }
    }
}