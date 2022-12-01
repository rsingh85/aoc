using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode.Y2020.Puzzle19.Part2
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

                    foreach (var subRuleIndex in subRulesSplit)
                    {
                        var parsedSubRuleIndex = int.Parse(subRuleIndex);

                        if (ruleIndex == parsedSubRuleIndex) // this is one of the recursive rules
                        {
                            switch (ruleIndex)
                            {
                                case 8:
                                    subAlternationRegex += $"{BuildRegexFromRules(ruleIndex: 42)}+";
                                    break;
                                case 11:
                                    var rule42 = BuildRegexFromRules(ruleIndex: 42);
                                    var rule31 = BuildRegexFromRules(ruleIndex: 31);

                                    // rule 11 is rule 42 followed by rule 31 (the same number of times for both)
                                    // balancing groups regex taken from (see: https://www.regular-expressions.info/balancing.html)
                                    subAlternationRegex += $"(?'open'{rule42})+(?'-open'{rule31})+(?(open)(?!))";
                                    break;
                            }
                        }
                        else
                        {
                            subAlternationRegex += BuildRegexFromRules(parsedSubRuleIndex);
                        }
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