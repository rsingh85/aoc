using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle6.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var groups = string.Join(string.Empty, File.ReadAllLines(Helper.GetInputFilePath(this))
                .Select(l => l == string.Empty ? "-" : l))
                .Split('-');

            var yesAnswerCount = 0;

            foreach (var group in groups)
            {
                var uniqueQuestionsAnsweredInGroup = new List<char>();

                foreach (var question in group)
                {
                    if (!uniqueQuestionsAnsweredInGroup.Contains(question))
                    {
                        uniqueQuestionsAnsweredInGroup.Add(question);
                        yesAnswerCount++;

                    }
                }
            }

            Console.WriteLine(yesAnswerCount);
        }
    }
}