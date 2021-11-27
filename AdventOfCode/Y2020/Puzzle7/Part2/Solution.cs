using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzle7.Part2
{
    public class Solution : ISolution
    {
        private Dictionary<string, Dictionary<string, int>> rules;

        public void Run()
        {
            var rulesInEnglish = File.ReadAllLines(Helper.GetInputFilePath(typeof(Solution)));
            rules = ParseEnglishRules(rulesInEnglish);

            var totalBags = FindBagsInside(rules["shiny gold bag"]);
            Console.WriteLine(totalBags);
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

        public int FindBagsInside(Dictionary<string, int> bagsInside)
        {
            int total = 0;

            foreach (string bagKey in bagsInside.Keys)
            {
                int amount = bagsInside[bagKey];
                total += amount;

                if (bagKey != "no other bag")
                {
                    total += amount * FindBagsInside(rules[bagKey]);
                }
            }

            return total;
        }
    }
}