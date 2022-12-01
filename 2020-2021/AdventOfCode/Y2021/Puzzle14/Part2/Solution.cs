namespace AdventOfCode.Y2021.Puzzle14.Part2
{
    public class Solution : ISolution
    {
        private Dictionary<string, long> _pairFrequencies;
        private Dictionary<char, long> _charFrequencies;

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var polymer = lines.First();
            var ruleDictionary = new Dictionary<string, char>();

            _pairFrequencies = new();
            _charFrequencies = new();

            foreach (var rule in lines.Skip(2))
            {
                var ruleSplit = rule.Split(" -> ");
                ruleDictionary.Add(ruleSplit[0], char.Parse(ruleSplit[1]));
            }

            var cursor = 0;
            var currentPair = string.Empty;

            while (cursor < polymer.Length - 1)
            {
                currentPair = $"{polymer[cursor]}{polymer[cursor + 1]}";
                IncreasePairCount(currentPair, 1);
                IncreaseCharCount(currentPair[0], 1);
                cursor++;
            }

            IncreaseCharCount(currentPair[1], 1);

            var step = 1;
            var pairsToRemove = new List<PairFrequency>();
            var pairsToAdd = new List<PairFrequency>();

            while (step <= 40)
            {
                pairsToRemove.Clear();
                pairsToAdd.Clear();

                foreach (var rulePairKey in ruleDictionary.Keys)
                {
                    if (_pairFrequencies.ContainsKey(rulePairKey))
                    {
                        var frequency = _pairFrequencies[rulePairKey];
                        pairsToRemove.Add(new PairFrequency { Pair = rulePairKey, Frequency = frequency });
                        pairsToAdd.Add(new PairFrequency { Pair = $"{rulePairKey[0]}{ruleDictionary[rulePairKey]}", Frequency = frequency });
                        pairsToAdd.Add(new PairFrequency { Pair = $"{ruleDictionary[rulePairKey]}{rulePairKey[1]}", Frequency = frequency });

                        IncreaseCharCount(ruleDictionary[rulePairKey], frequency);
                    }
                }

                foreach (var pairFrequency in pairsToRemove)
                {
                    DecreasePairCount(pairFrequency.Pair, pairFrequency.Frequency);
                }

                foreach (var pairFrequency in pairsToAdd)
                {
                    IncreasePairCount(pairFrequency.Pair, pairFrequency.Frequency);
                }

                step++;
            }

            Console.WriteLine(_charFrequencies.Values.Max() - _charFrequencies.Values.Min());
        }

        private void IncreasePairCount(string pair, long count)
        {
            if (!_pairFrequencies.ContainsKey(pair))
            {
                _pairFrequencies.Add(pair, count);
            }
            else
            {
                _pairFrequencies[pair] += count;
            }
        }

        private void DecreasePairCount(string pair, long count)
        {
            if (_pairFrequencies.ContainsKey(pair))
            {
                _pairFrequencies[pair] -= count;

                if (_pairFrequencies[pair] == 0)
                {
                    _pairFrequencies.Remove(pair);
                }
            }
        }

        private void IncreaseCharCount(char c, long count)
        {
            if (!_charFrequencies.ContainsKey(c))
            {
                _charFrequencies.Add(c, count);
            }
            else
            {
                _charFrequencies[c] += count;
            }
        }
    }

    public struct PairFrequency
    {
        public string Pair { get; set; }
        public long Frequency { get; set; }
    }
}