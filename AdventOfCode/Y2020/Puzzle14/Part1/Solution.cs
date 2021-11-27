using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle14.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));
            var memoryAddresses = new Dictionary<string, long>();
            var currentMask = string.Empty;

            foreach (var line in lines)
            {
                if (line.StartsWith("mask"))
                {
                    currentMask = Regex.Match(line, @"[X01]+$").Value;
                }
                else
                {
                    var memoryAddress = Regex.Match(line, @"^mem\[(\d+)\]").Groups[1].Value;
                    var value = long.Parse(Regex.Match(line, @"\d+$").Value);
                    var binaryValue = Convert.ToString(value, 2).PadLeft(currentMask.Length, '0');
                    var result = string.Empty;
            
                    for (var i = 0; i < currentMask.Length; i++)
                    {
                        var currentMaskChar = currentMask[i];
                    
                        switch (currentMaskChar)
                        {
                            case '0':
                                result += "0";
                                break;
                            case '1':
                                result += "1";
                                break;
                            default:
                                result += binaryValue[i];
                                break;
                        }
                    }
                    memoryAddresses[memoryAddress] = Convert.ToInt64(result, 2);
                }
            }

            Console.WriteLine(memoryAddresses.Values.Sum());
        }
    }
}