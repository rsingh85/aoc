using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public static class Helper
    {
        public static string GetInputFilePath(Type solutionType)
        {
            var parsedNamespace = 
                solutionType
                    .Namespace
                        .Replace("AdventOfCode.", string.Empty)
                        .Replace(".", "\\");

            return $"{parsedNamespace}\\Input.txt";
        }
    }
}
