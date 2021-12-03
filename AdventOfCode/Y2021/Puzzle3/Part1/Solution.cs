using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2021.Puzzle3.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var binaries = File.ReadAllLines(Helper.GetInputFilePath(this));
            string gRate = string.Empty, eRate = string.Empty;

            for (var i = 0; i < binaries.First().Length; i++)
            {
                var zeroes = binaries.Select(b => b[i]).Count(b => b == '0');
                var ones = binaries.Select(b => b[i]).Count(b => b == '1');

                gRate += (zeroes > ones) ? "0" : "1";
                eRate += (zeroes > ones) ? "1" : "0";
            }

            var powerConsumption = Convert.ToInt32(gRate, 2) * Convert.ToInt32(eRate, 2);

            Console.WriteLine(powerConsumption);
        }
    }
}