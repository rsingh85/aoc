namespace AdventOfCode.Y2024.Puzzle2.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var reports = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();
            var safeReports = 0;

            foreach (var report in reports)
            {
                var levels = report.Split(' ').Select(int.Parse).ToArray();
                
                if (IsReportSafe(levels))
                    safeReports++;
                else
                {
                    foreach (var permutation in GetLevelPermutations(levels))
                    {
                        if (IsReportSafe(permutation))
                        {
                            safeReports++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(safeReports);
        }

        private static bool IsReportSafe(int[] levels)
        {
            bool isIncreasingReport = levels[1] > levels[0];

            for (var i = 0; i < levels.Length - 1; i++)
            {
                var diff = levels[i + 1] - levels[i];

                if (isIncreasingReport && diff <= 0)
                    return false;

                if (!isIncreasingReport && diff >= 0)
                    return false;

                var absDiff = Math.Abs(diff);
                if (!(absDiff >= 1 && absDiff <= 3))
                    return false;
            }

            return true;
        }

        private static List<int[]> GetLevelPermutations(int[] levels)
        {
            var result = new List<int[]>();

            for (int i = 0; i < levels.Length; i++)
            {
                var tempArray = new int[levels.Length - 1];
                var index = 0;

                for (var j = 0; j < levels.Length; j++)
                {
                    if (j != i)
                        tempArray[index++] = levels[j];
                }

                result.Add(tempArray);
            }

            return result;
        }
    }
}