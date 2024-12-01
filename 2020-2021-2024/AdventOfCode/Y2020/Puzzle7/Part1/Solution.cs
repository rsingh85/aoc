using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Y2020.Puzzle7.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var rulesInEnglish = File.ReadAllLines(Helper.GetInputFilePath(this));
            var rules = ParseEnglishRules(rulesInEnglish);
            var outerBagsThatCanContainShinyGoldBag = 0;

            foreach (var outerBagKey in rules.Keys)
            {
                if (ContainsBag(rules, outerBagKey, "shiny gold bag"))
                {
                    outerBagsThatCanContainShinyGoldBag++;
                }
            }

            Console.WriteLine(outerBagsThatCanContainShinyGoldBag);
        }

        private Dictionary<string, Dictionary<string, int>> ParseEnglishRules(string[] rulesInEnglish)
        {
            var rules = new Dictionary<string, Dictionary<string, int>>();

            foreach (var ruleInEnglish in rulesInEnglish)
            {
                var containSplit = ruleInEnglish.Replace(" contain ", "~").Split('~');
                var outerBag = containSplit[0].Replace("bags", "bag");
                var innerBagsInEnglish = containSplit[1].Split(',');
                var innerBags = new Dictionary<string, int>();

                foreach (var innerBagInEnglish in innerBagsInEnglish)
                {
                    var innerBagInEnglishSanitised = innerBagInEnglish.Trim().Replace(".", string.Empty);
                    var innerBagName = Regex.Match(innerBagInEnglishSanitised, @"[a-z\s]+$").Value.Trim();
                    var innerBagCount =
                        innerBagInEnglishSanitised == "no other bags"
                            ? 0
                            : int.Parse(Regex.Match(innerBagInEnglishSanitised, @"^\d+").Value);

                    innerBags.Add(innerBagName.Replace("bags", "bag"), innerBagCount);
                }

                rules.Add(outerBag, innerBags);
            }
            return rules;
        }

        private bool ContainsBag(Dictionary<string, Dictionary<string, int>> rules, string outerBagKey, string bagToSearchFor)
        {
            if (outerBagKey == "no other bag")
            {
                return false;
            }

            if (rules[outerBagKey].ContainsKey(bagToSearchFor))
            {
                return true;
            }

            foreach (var innerBagKey in rules[outerBagKey].Keys)
            {
                if (ContainsBag(rules, innerBagKey, bagToSearchFor))
                {
                    return true;
                }
            }

            return false;
        }
    }
}