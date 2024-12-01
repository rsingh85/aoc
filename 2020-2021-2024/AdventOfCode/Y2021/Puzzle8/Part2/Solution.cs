namespace AdventOfCode.Y2021.Puzzle8.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = File.ReadAllLines(Helper.GetInputFilePath(this));
            var sum = 0;

            foreach (var line in input)
            {
                var splitLine = line.Split("|");
                var signals = splitLine[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => string.Concat(s.OrderBy(c => c)));
                var decodedDigits = splitLine[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => string.Concat(s.OrderBy(c => c)));

                var digitToSignalMap = new Dictionary<int, string>();
                digitToSignalMap.Add(1, signals.Where(s => s.Length == 2).Single());
                digitToSignalMap.Add(4, signals.Where(s => s.Length == 4).Single());
                digitToSignalMap.Add(7, signals.Where(s => s.Length == 3).Single());
                digitToSignalMap.Add(8, signals.Where(s => s.Length == 7).Single());

                var signalsWithLengthSix = signals.Where(s => s.Length == 6);
                var signalsWithLengthFive = signals.Where(s => s.Length == 5);

                foreach (var signal in signalsWithLengthSix)
                {
                    if (GetIntersectCount(signal, digitToSignalMap[1]) == 1 && !digitToSignalMap.ContainsKey(6))
                    {
                        digitToSignalMap.Add(6, signal);
                    }
                    else if (GetIntersectCount(signal, digitToSignalMap[4]) == 3 && !digitToSignalMap.ContainsKey(0))
                    {
                        digitToSignalMap.Add(0, signal);
                    }
                    else if (GetIntersectCount(signal, digitToSignalMap[4]) == 4 && !digitToSignalMap.ContainsKey(9))
                    {
                        digitToSignalMap.Add(9, signal);
                    }
                }

                foreach (var signal in signalsWithLengthFive)
                {
                    if (GetIntersectCount(signal, digitToSignalMap[6]) == 5 && !digitToSignalMap.ContainsKey(5))
                    {
                        digitToSignalMap.Add(5, signal);
                    }
                    else if (GetIntersectCount(signal, digitToSignalMap[1]) == 2 && !digitToSignalMap.ContainsKey(3))
                    {
                        digitToSignalMap.Add(3, signal);
                    }
                    else if (!digitToSignalMap.ContainsKey(2))
                    {
                        digitToSignalMap.Add(2, signal);
                    }
                }

                var output = string.Empty;

                foreach (var decodedDigit in decodedDigits)
                { 
                    // todo: optimise to avoid reverse lookup from dictionary...
                    foreach (var keyDigit in digitToSignalMap.Keys)
                    {
                        if (decodedDigit == digitToSignalMap[keyDigit])
                        {
                            output += keyDigit;
                        }
                    }
                }

                // Console.WriteLine(output);
                sum += Convert.ToInt32(output);
            }

            Console.WriteLine(sum);
        }

        private int GetIntersectCount(string signalA, string signalB)
        {
            return signalA.ToCharArray().Intersect(signalB.ToCharArray()).Count();
        }
    }
}