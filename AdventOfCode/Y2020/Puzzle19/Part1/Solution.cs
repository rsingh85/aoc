using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle19.Part1
{
    public class Solution : ISolution
    {
        private Dictionary<int, string> _rules = new Dictionary<int, string>();
        private List<string> _messages = new List<string>();

        public void Run()
        {
            ParseInput(File.ReadAllLines(Helper.GetInputFilePath(this)));
            var regex = BuildRegexFromRules(ruleIndex: 0);

            var validMessageCount = 0;

            foreach (var message in _messages)
            {
                validMessageCount += Regex.IsMatch(message, $"^{regex}$") ? 1 : 0;
            }

            Console.WriteLine(validMessageCount);
        }

        private string BuildRegexFromRules(int ruleIndex)
        {
            var currentRule = _rules[ruleIndex];

            if (currentRule.Contains("\""))
            {
                return currentRule.Replace("\"", string.Empty);
            }
            else if (currentRule.Contains("|"))
            {
                var currentRulesSplit = currentRule.Split('|');
                var subAlternationRegex = string.Empty;

                foreach (var subRules in currentRulesSplit)
                {
                    var subRulesSplit = subRules.Trim().Replace(" ", ",").Split(',');

                    foreach (var subRule in subRulesSplit)
                    {
                        subAlternationRegex += BuildRegexFromRules(int.Parse(subRule));
                    }

                    subAlternationRegex += "|";
                }


                return $"({subAlternationRegex.Trim('|')})";
            }

            var regex = string.Empty;
            var subRuleIndices = currentRule.Trim().Replace(" ", ",").Split(',').Select(int.Parse);

            foreach (var subRuleIndex in subRuleIndices)
            {
                regex += BuildRegexFromRules(subRuleIndex);
            }

            return $"{regex}";
        }

        private void ParseInput(string[] lines)
        {
            foreach (var line in lines)
            {
                var ruleMatch = Regex.Match(line, @"^(\d+)\:");

                if (ruleMatch.Success)
                {
                    var rule = line.Split(':')[1].Trim();
                    _rules.Add(int.Parse(ruleMatch.Groups[1].Value), rule);
                }
                else if (!string.IsNullOrWhiteSpace(line))
                {
                    _messages.Add(line);
                }
            }
        }
    }
}