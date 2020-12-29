using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AdventOfCode.Puzzle13.Part2
{
    public class Solution : ISolution
    {
        private DateTime _startTime;

        private Dictionary<int, int> _indexToBusMinuteFrequencyDictionary = new Dictionary<int, int>();
        private int _highestBusMinuteFrequencyIndex = 0;
        private int _highestBusMinuteFrequency = 0;
        private List<Task> _tasks = new List<Task>();
        private bool _success = false;

        public void Run()
        {
            _startTime = DateTime.Now;

            var input = File.ReadAllLines(@"Puzzle13\Part2\Input.txt");
            var busMinuteFrequencies = input[1].Split(',');

            for (var i = 0; i < busMinuteFrequencies.Length; i++)
            {
                var currentBusMinuteFrequency = busMinuteFrequencies[i];

                if (currentBusMinuteFrequency != "x")
                {
                    var currentBusMinuteFrequencyAsInt = int.Parse(currentBusMinuteFrequency);
                    _indexToBusMinuteFrequencyDictionary.Add(i, currentBusMinuteFrequencyAsInt);

                    // save highest
                    if (currentBusMinuteFrequencyAsInt > _highestBusMinuteFrequency)
                    {
                        _highestBusMinuteFrequencyIndex = i;
                        _highestBusMinuteFrequency = currentBusMinuteFrequencyAsInt;
                    }
                }
            }


            _tasks.Add(new Task(() => FindEarliestTimestamp(100000000000000, 200000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(200000000000000, 300000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(300000000000000, 400000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(400000000000000, 500000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(500000000000000, 600000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(600000000000000, 700000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(700000000000000, 800000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(800000000000000, 900000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(900000000000000, 1000000000000000)));
            _tasks.Add(new Task(() => FindEarliestTimestamp(1000000000000000, 1100000000000000)));

            foreach (var task in _tasks)
            {
                task.Start();
            }

            Task.WaitAll(_tasks.ToArray());
        }

        private void FindEarliestTimestamp(long startingTimestamp, long endingTimestamp)
        {
            Console.WriteLine("FindEarliestTimestamp({0}, {1})", startingTimestamp, endingTimestamp);

            var startingTimestampFound = false;

            while (!startingTimestampFound)
            {
                if (startingTimestamp % _highestBusMinuteFrequency != 0)
                {
                    startingTimestamp++;
                }
                else
                {
                    startingTimestampFound = true;
                }
            }

            var earliestTimestampFound = false;

            for (long timestamp = startingTimestamp;
                !earliestTimestampFound || !_success || timestamp < endingTimestamp;
                timestamp += _highestBusMinuteFrequency)
            {
                //Console.WriteLine($"Current Timestamp = {timestamp} | Time taken (secs): {(DateTime.Now - startTime).TotalSeconds})");

                var sequenceFound = true;

                var actualStartTimestamp = timestamp - _highestBusMinuteFrequencyIndex;

                foreach (var indexToBusMinuteFrequency in _indexToBusMinuteFrequencyDictionary)
                {
                    if ((actualStartTimestamp + indexToBusMinuteFrequency.Key) % indexToBusMinuteFrequency.Value != 0)
                    {
                        sequenceFound = false;
                        break;
                    }
                }

                if (sequenceFound)
                {
                    var endTime = DateTime.Now;
                    earliestTimestampFound = true;
                    _success = true;

                    Console.WriteLine(actualStartTimestamp);
                    Console.WriteLine($"Time taken: {(endTime - _startTime).TotalSeconds}");
                }
            }
        }
    }
}