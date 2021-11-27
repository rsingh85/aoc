using System;

namespace AdventOfCode
{
    public static class Helper
    {
        public static string GetInputFilePath(Type solutionType)
        {
            var solutionPath = 
                solutionType
                    .Namespace
                        .Replace("AdventOfCode.", string.Empty)
                        .Replace(".", @"\");

            return $"{solutionPath}\\Input.txt";
        }
    }
}
