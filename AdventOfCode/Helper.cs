using System;

namespace AdventOfCode
{
    public static class Helper
    {
        public static string GetInputFilePath(Type solutionType)
        {
            var solutionNamespace = 
                solutionType
                    .Namespace
                        .Replace("AdventOfCode.", string.Empty)
                        .Replace(".", @"\");

            return $"{solutionNamespace}\\Input.txt";
        }
    }
}
