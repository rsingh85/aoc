namespace AdventOfCode.Y2025.Puzzle3.Part1
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var banks = File.ReadAllLines(Helper.GetInputFilePath(this));
            var largestJoltagesPerBank = new int[banks.Length];

            for (var bankIndex = 0; bankIndex < banks.Length; bankIndex++)
            {
                var bank = banks[bankIndex];

                for (var currentBatteryIndex = 0; currentBatteryIndex < bank.Length; currentBatteryIndex++)
                {
                    var currentBatteryJoltageChar = bank[currentBatteryIndex];

                    for (var otherBatteryIndex = currentBatteryIndex + 1; otherBatteryIndex < bank.Length; otherBatteryIndex++)
                    {
                        var otherBatteryJoltageChar = bank[otherBatteryIndex];

                        var totalJoltage = int.Parse($"{currentBatteryJoltageChar}{otherBatteryJoltageChar}");

                        if (totalJoltage > largestJoltagesPerBank[bankIndex])
                            largestJoltagesPerBank[bankIndex] = totalJoltage;
                    }
                }
            }

            Console.WriteLine(largestJoltagesPerBank.Sum());
        }
    }
}