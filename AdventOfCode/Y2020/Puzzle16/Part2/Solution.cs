using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Puzzle16.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var input = ReadInput(@"Y2020\Puzzle16\Part2\Input.txt");
            input.NearbyTickets.Add(input.YourTicket);

            foreach (var nearbyTicket in input.NearbyTickets)
            {
                nearbyTicket.IsValid = true;

                for (var i = 0; i < nearbyTicket.Values.Count; i++)
                {
                    var value = nearbyTicket.Values[i];
                    if (!IsValueValid(input.FieldToRangesDictionary, nearbyTicket, i, value))
                    {
                        nearbyTicket.IsValid = false;
                    }
                }
            }

            // discard invalid tickets
            input.NearbyTickets = input.NearbyTickets.Where(t => t.IsValid).ToList();

            // Now figure out the positions that belong to each ticker value index!
            // e.g. <"row", 0>, <"class,row", 1>
            var fieldToValueIndexDictionary = new Dictionary<List<string>, int>();

            // go through each nearby ticket and work out the common fields per position
            for (var position = 0; position < input.YourTicket.Values.Count; position++)
            {
                var currentPositionPossibleFieldsLists = input.NearbyTickets.Select(t => t.ValueIndexToValidFields[position]);
                var fieldIntersection = currentPositionPossibleFieldsLists
                                   .Skip(1)
                                   .Aggregate(new HashSet<string>(currentPositionPossibleFieldsLists.First()), (h, e) => { h.IntersectWith(e); return h; });
                
                //fieldToValueIndexDictionary.Add(string.Join(",", fieldIntersection), position);
                fieldToValueIndexDictionary.Add(fieldIntersection.ToList(), position);
            }

            while (fieldToValueIndexDictionary.Keys.Any(k => k.Count > 1))
            {
                var singleKeys = fieldToValueIndexDictionary.Keys.Where(k => k.Count == 1).ToList();
                var multiKeys = fieldToValueIndexDictionary.Keys.Where(k => k.Count > 1).ToList();
                foreach (var multiKey in multiKeys)
                {
                    foreach (var singleKey in singleKeys.SelectMany(sk => sk))
                    {
                        multiKey.Remove(singleKey);
                    }
                }
            }

            var departureFieldKeys = fieldToValueIndexDictionary.Keys.Where(k => k.First().StartsWith("departure")).ToList();
            long multiplier = input.YourTicket.Values[fieldToValueIndexDictionary[departureFieldKeys.First()]];
            
            for (var i = 1; i < departureFieldKeys.Count; i++)
            {
                var currentDepartureFieldKey = fieldToValueIndexDictionary[departureFieldKeys[i]];
                multiplier *= input.YourTicket.Values[currentDepartureFieldKey];
            }

            Console.WriteLine(multiplier);
        }
        private bool IsValueValid(Dictionary<string, List<Range>> fieldToRangesDictionary, Ticket nearByTicket, int valueIndex, int value)
        {
            if (nearByTicket.ValueIndexToValidFields == null)
            {
                nearByTicket.ValueIndexToValidFields = new Dictionary<int, List<string>>();
            }

            foreach (var fieldKey in fieldToRangesDictionary.Keys)
            {
                var ranges = fieldToRangesDictionary[fieldKey];

                var valid =
                    value >= ranges[0].Min && value <= ranges[0].Max ||
                    value >= ranges[1].Min && value <= ranges[1].Max;

                if (valid)
                {
                    if (nearByTicket.ValueIndexToValidFields.ContainsKey(valueIndex))
                    {
                        nearByTicket.ValueIndexToValidFields[valueIndex].Add(fieldKey);
                    }
                    else
                    {
                        nearByTicket.ValueIndexToValidFields.Add(valueIndex, new List<string> { fieldKey });
                    }
                }
            }

            return nearByTicket.ValueIndexToValidFields.ContainsKey(valueIndex) && nearByTicket.ValueIndexToValidFields[valueIndex].Any();
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
            public bool IsValid { get; set; }
            public List<int> Values { get; set; }
            public Dictionary<int, List<string>> ValueIndexToValidFields { get; set; }
        }
    }
}