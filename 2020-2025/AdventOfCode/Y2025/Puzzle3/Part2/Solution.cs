namespace AdventOfCode.Y2025.Puzzle3.Part2
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var banks = File.ReadAllLines(Helper.GetInputFilePath(this));
            var largestJoltagesPerBank = new long[banks.Length];
            const int MaxBatteriesToConsiderPerBank = 12;

            for (var bankIndex = 0; bankIndex < banks.Length; bankIndex++)
            {
                var bank = banks[bankIndex];
                var batteriesStack = new Stack<int>();
                batteriesStack.Push(int.Parse(bank[0].ToString()));

                for (var currentBatteryIndex = 1; currentBatteryIndex < bank.Length; currentBatteryIndex++)
                {
                    var currentBatteryInt = int.Parse(bank[currentBatteryIndex].ToString());

                    while (batteriesStack.Any() && batteriesStack.Peek() < currentBatteryInt)
                    {
                        var stackCountAfterPop = batteriesStack.Count - 1;

                        if (stackCountAfterPop + (bank.Length - currentBatteryIndex) < MaxBatteriesToConsiderPerBank)
                            break;

                        batteriesStack.Pop();
                    }

                    if (batteriesStack.Count < MaxBatteriesToConsiderPerBank)
                        batteriesStack.Push(currentBatteryInt);
                }

                var highestJoltageForBank = long.Parse(string.Join(string.Empty, batteriesStack.Reverse()));
                largestJoltagesPerBank[bankIndex] = highestJoltageForBank;
            }

            Console.WriteLine(largestJoltagesPerBank.Sum());
        }
    }
}