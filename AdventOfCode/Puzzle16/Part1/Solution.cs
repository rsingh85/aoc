using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Puzzle16.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = ReadInput(@"Puzzle16\Part1\Input.txt");

            var invalidValues = new List<int>();

            foreach (var nearbyTicket in input.NearbyTickets)
            {
                foreach (var value in nearbyTicket.Values)
                {
                    if (!IsValueValid(input.FieldToRangesDictionary, value))
                    {
                        invalidValues.Add(value);
                    }
                }
            }

            Console.WriteLine(invalidValues.Sum());
        }

        private bool IsValueValid(Dictionary<string, List<Range>> fieldToRangesDictionary, int value)
        {

            foreach (var fieldKey in fieldToRangesDictionary.Keys)
            {
                var ranges = fieldToRangesDictionary[fieldKey];

                var valid =
                    value >= ranges[0].Min && value <= ranges[0].Max ||
                    value >= ranges[1].Min && value <= ranges[1].Max;

                if (valid)
                {
                    return true;
                }
            }

            return false;
        }

        private Input ReadInput(string dataFilePath)
        {
            var input = File.ReadAllLines(dataFilePath);
            var currentLineIndex = 0;
            var currentLine = string.Empty;
            var result = new Input();

            result.FieldToRangesDictionary = new Dictionary<string, List<Range>>();

            while ((currentLine = input[currentLineIndex]) != string.Empty)
            {
                var fieldName = currentLine.Split(':')[0];
                var ranges = currentLine.Split(':')[1].Trim().Replace(" or ", ",").Split(',');
                var rangesList = new List<Range>();

                foreach (var range in ranges)
                {
                    var rangeSplit = range.Split('-');

                    rangesList.Add(new Range { Min = int.Parse(rangeSplit[0]), Max = int.Parse(rangeSplit[1]) });
                }

                result.FieldToRangesDictionary.Add(fieldName, rangesList);
                currentLineIndex++;
            }


            currentLineIndex += 2;
            currentLine = input[currentLineIndex];

            result.YourTicket = new Ticket();
            result.YourTicket.Values = currentLine.Split(',').Select(int.Parse).ToList();

            currentLineIndex += 3;
            result.NearbyTickets = new List<Ticket>();

            while (currentLineIndex < input.Length)
            {
                currentLine = input[currentLineIndex];

                result.NearbyTickets.Add(new Ticket
                {
                    Values = currentLine.Split(',').Select(int.Parse).ToList()
                });

                currentLineIndex++;
            }
            return result;
        }

        private class Input
        {
            public Dictionary<string, List<Range>> FieldToRangesDictionary { get; set; }
            public Ticket YourTicket { get; set; }
            public List<Ticket> NearbyTickets { get; set; }
        }

        private class Range
        {
            public int Min { get; set; }
            public int Max { get; set; }
        }

        private class Ticket
        {
            public List<int> Values { get; set; }
        }
    }
}