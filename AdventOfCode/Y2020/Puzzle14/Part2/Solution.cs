using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace AdventOfCode.Puzzle14.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(@"Y2020\Puzzle14\Part2\Input.txt");
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
                    var memoryAddress = long.Parse(Regex.Match(line, @"^mem\[(\d+)\]").Groups[1].Value);
                    var binaryMemoryAddress = Convert.ToString(memoryAddress, 2).PadLeft(currentMask.Length, '0');
                    var value = long.Parse(Regex.Match(line, @"\d+$").Value);

                    var binaryMaskedMemoryAddress = string.Empty;

                    for (var i = 0; i < currentMask.Length; i++)
                    {
                        var currentMaskChar = currentMask[i];

                        switch (currentMaskChar)
                        {
                            case '1':
                                binaryMaskedMemoryAddress += "1";
                                break;
                            case 'X':
                                binaryMaskedMemoryAddress += "X";
                                break;
                            default:
                                binaryMaskedMemoryAddress += binaryMemoryAddress[i];
                                break;
                        }
                    }

                    var allPossibleMemoryAddresses = GetAllPossibleMemoryAddresses(binaryMaskedMemoryAddress);

                    foreach (var decodedMemeoryAddress in allPossibleMemoryAddresses)
                    {
                        memoryAddresses[decodedMemeoryAddress] = value;
                    }
                }
            }

            Console.WriteLine(memoryAddresses.Values.Sum());
        }

        private List<string> GetAllPossibleMemoryAddresses(string binaryMaskedMemoryAddress)
        {
            var memoryAddresses = new List<string>();

            var floatingBitsCount = binaryMaskedMemoryAddress.ToCharArray().Count(c => c == 'X');

            var floatingBitPermutations = new List<string>();
            GeneratePermutations(floatingBitPermutations, floatingBitsCount);

            foreach (var permutation in floatingBitPermutations)
            {
                var currentIndexOfFloatingBit = binaryMaskedMemoryAddress.IndexOf('X', 0);
                var memoryAddress = new StringBuilder(binaryMaskedMemoryAddress);

                foreach (var bit in permutation)
                {
                    memoryAddress[currentIndexOfFloatingBit] = bit;
                    currentIndexOfFloatingBit = binaryMaskedMemoryAddress.IndexOf('X', currentIndexOfFloatingBit + 1);
                }

                memoryAddresses.Add(memoryAddress.ToString());
            }

            return memoryAddresses;
        }

        private void GeneratePermutations(List<string> strings, int n, string cur = "")
        {
            if (cur.Length == n)
            {
                strings.Add(cur);
            }
            else
            {
                GeneratePermutations(strings, n, cur + "0");
                GeneratePermutations(strings, n, cur + "1");
            }
        }
    }
}