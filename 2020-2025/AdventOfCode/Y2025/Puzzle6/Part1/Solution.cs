namespace AdventOfCode.Y2025.Puzzle6.Part1
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this))
                .Select(l => l.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                .ToList();

            // Maps colIndex => working result
            var columnIndexResultsDict = new Dictionary<int, long>();

            for (var colIndex = 0; colIndex < lines.First().Length; colIndex++)
                columnIndexResultsDict.Add(colIndex, long.Parse(lines[0][colIndex]));

            for (var colIndex = 0; colIndex < lines.First().Length; colIndex++)
            {
                for (var lineIndex = 1; lineIndex < lines.Count - 1; lineIndex++)
                {
                    var workingResult = columnIndexResultsDict[colIndex];
                    var currentNum = long.Parse(lines[lineIndex][colIndex]);
                    var currentOp = lines[^1][colIndex][0];
                    
                    columnIndexResultsDict[colIndex] = ApplyOperation(workingResult, currentNum, currentOp);
                }
            }

            Console.WriteLine(columnIndexResultsDict.Values.Sum());
        }

        private static long ApplyOperation(long a, long b, char op) => op switch
        {
            '*' => a * b,
            '+' => a + b,
            _ => throw new ArgumentException($"Unsupported operation '{op}'"),
        };
    }
}