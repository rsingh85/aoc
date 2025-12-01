namespace AdventOfCode.Y2024.Puzzle2.Part1
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
            }

            Console.WriteLine(safeReports);
        }

        private bool IsReportSafe(int[] levels)
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
    }
}