using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Puzzle6.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var groups = string.Join(",", File.ReadAllLines(Helper.GetInputFilePath(this))
                .Select(l => l == string.Empty ? "-" : l))
                .Split('-');

            var everyoneAnsweredYesToQuestionsCount = 0;

            foreach (var group in groups)
            {
                var answersByPerson = group.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var questionCounts = new Dictionary<char, int>();

                foreach (var answerByPerson in answersByPerson)
                {
                    foreach (var question in answerByPerson)
                    {
                        questionCounts[question] = questionCounts.ContainsKey(question) ?
                            questionCounts[question] + 1 : 1;
                    }
                }

                foreach (var questionKey in questionCounts.Keys)
                {
                    var countOfYesAnswersToQuestionInGroup = questionCounts[questionKey];

                    if (countOfYesAnswersToQuestionInGroup == answersByPerson.Length)
                    {
                        everyoneAnsweredYesToQuestionsCount++;
                    }
                }
            }

            Console.WriteLine(everyoneAnsweredYesToQuestionsCount);
        }
    }
}
