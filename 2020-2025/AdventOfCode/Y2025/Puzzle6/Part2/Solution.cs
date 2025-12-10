namespace AdventOfCode.Y2025.Puzzle6.Part2
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var results = new List<long>();
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            var numbers = lines.Take(lines.Count() - 1).ToList();
            var ops = lines.Last().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            
            var opsPointer = ops.Length - 1;
            var colPointer = numbers[0].Length - 1;

            while (opsPointer >= 0)
            {
                var currentOp = ops[opsPointer];
                var numbersForOp = new List<long>();
                var allNumbersForOpScanned = false;

                while (!allNumbersForOpScanned && colPointer >= 0)
                {
                    var currentColumnNumber = GetNumberInColumn(colPointer, numbers);

                    if (currentColumnNumber.HasValue)
                        numbersForOp.Add(currentColumnNumber.Value);
                    else
                        allNumbersForOpScanned = true;
                        
                    colPointer--;
                }

                if (numbersForOp.Count > 0)
                {
                    var opResult = numbersForOp[0];

                    for (var i = 1; i < numbersForOp.Count; i++)
                        opResult = ApplyOperation(opResult, numbersForOp[i], currentOp[0]);
                    
                    results.Add(opResult);
                }

                opsPointer--;
            }

            Console.WriteLine(results.Sum());
        }

        private static long? GetNumberInColumn(int colIndex, List<string> numbers)
        {       
            var columnNumber = string.Empty;

            foreach (var number in numbers)
                if (char.IsDigit(number[colIndex]))
                    columnNumber += number[colIndex];

            return string.IsNullOrEmpty(columnNumber) ? 
                null : long.Parse(columnNumber);
        }

        private static long ApplyOperation(long a, long b, char op) => op switch
        {
            '*' => a * b,
            '+' => a + b,
            _ => throw new ArgumentException($"Unsupported operation '{op}'"),
        };
    }
}