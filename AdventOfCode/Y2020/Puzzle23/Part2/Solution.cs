using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Puzzle23.Part2
{
    public class Solution : ISolution
    {
        private LinkedList<int> _cups = new LinkedList<int>();
        private readonly int MaximumCupNumber = 1000000;
        private Dictionary<int, LinkedListNode<int>> _nodeCache = new Dictionary<int, LinkedListNode<int>>();

        public void Run()
        {
            var startTime = DateTime.Now;

            ParseInput(File.ReadAllLines(@"Y2020\Puzzle23\Part2\Input.txt"));

            // add the numbers up to 1 million (from max input number)!
            var maxCupNumber = _cups.Max(); // Using Max() is fine as the list will be small at this point

            for (var i = maxCupNumber + 1; i <= MaximumCupNumber; i++)
            {
                var addedNode = _cups.AddLast(i);
                _nodeCache.Add(addedNode.Value, addedNode);
            }

            var currentCup = _cups.First;
            var currentMove = 1;
            const int MaxMoves = 10000000;

            while (currentMove <= MaxMoves)
            {
                if (currentMove % 1000000 == 0)
                {
                    Console.WriteLine("Reached {0} moves", currentMove);
                }

                var firstCup = (currentCup.Next == null) ? _cups.First : currentCup.Next;
                var secondCup = (firstCup.Next == null) ? _cups.First : firstCup.Next;
                var thirdCup = (secondCup.Next == null) ? _cups.First : secondCup.Next;

                _cups.Remove(firstCup);
                _cups.Remove(secondCup);
                _cups.Remove(thirdCup);

                var destinationCupValue = currentCup.Value - 1;

                if (destinationCupValue == 0)
                {
                    destinationCupValue = GetMaxCupNumber(firstCup.Value, secondCup.Value, thirdCup.Value);
                }

                while (destinationCupValue == firstCup.Value ||
                    destinationCupValue == secondCup.Value ||
                    destinationCupValue == thirdCup.Value)
                {
                    destinationCupValue--;

                    if (destinationCupValue < GetMinCupNumber(firstCup.Value, secondCup.Value, thirdCup.Value))
                    {
                        destinationCupValue = GetMaxCupNumber(firstCup.Value, secondCup.Value, thirdCup.Value);
                    }
                }

                var destinationCupNode = _nodeCache[destinationCupValue];

                var firstCupNode = _cups.AddAfter(destinationCupNode, firstCup.Value);
                var secondCupNode = _cups.AddAfter(firstCupNode, secondCup.Value);
                var thirdCupNode = _cups.AddAfter(secondCupNode, thirdCup.Value);

                // update cache
                _nodeCache[firstCup.Value] = firstCupNode;
                _nodeCache[secondCup.Value] = secondCupNode;
                _nodeCache[thirdCup.Value] = thirdCupNode;

                currentCup = currentCup.Next;

                if (currentCup == null)
                {
                    currentCup = _cups.First;
                }

                currentMove++;
            }

            var nodeWithValueOne = _nodeCache[1];

            var nextNode = nodeWithValueOne.Next;

            if (nextNode == null)
            {
                nextNode = _cups.First;
            }

            var nextNextNode = nextNode.Next;

            if (nextNextNode == null)
            {
                nextNextNode = _cups.First;
            }

            long result = (long)nextNode.Value * nextNextNode.Value;

            Console.WriteLine("Finished!");
            Console.WriteLine("It took (secs): {0}", (DateTime.Now - startTime).TotalSeconds);
            Console.WriteLine("{0} x {1}", nextNode.Value, nextNextNode.Value);
            Console.WriteLine(result);
        }

        private int GetMinCupNumber(int firstCup, int secondCup, int thirdCup)
        {
            var minCupNumber = 1;

            if (firstCup == minCupNumber)
                minCupNumber++;

            if (secondCup == minCupNumber)
                minCupNumber++;

            if (thirdCup == minCupNumber)
                minCupNumber++;

            return minCupNumber;
        }

        private int GetMaxCupNumber(int firstCup, int secondCup, int thirdCup)
        {
            var maxCupNumber = MaximumCupNumber;

            if (firstCup == maxCupNumber)
                maxCupNumber--;

            if (secondCup == maxCupNumber)
                maxCupNumber--;

            if (thirdCup == maxCupNumber)
                maxCupNumber--;

            return maxCupNumber;
        }

        private void ParseInput(string[] lines)
        {
            if (lines.Length != 1)
            {
                throw new ArgumentException("Only one line expected in the input");
            }

            var cups = lines.First();

            foreach (var cup in cups)
            {
                var addedNode = _cups.AddLast(int.Parse(cup.ToString()));
                _nodeCache.Add(addedNode.Value, addedNode);
            }
        }
    }
}