namespace AdventOfCode
{
    public static class Helper
    {
        public static string GetInputFilePath(ISolution solution)
        {
            var solutionPath = 
                solution.GetType()
                    .Namespace
                        .Replace("AdventOfCode.", string.Empty)
                        .Replace(".", @"\");

            return $"{solutionPath}\\Input.txt";
        }
    }
}
