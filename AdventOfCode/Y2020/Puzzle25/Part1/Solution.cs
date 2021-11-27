using System;

namespace AdventOfCode.Y2020.Puzzle25.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            long cardPublicKey = 11404017;
            long doorPublicKey = 13768789;

            int cardLoopSize = -1;
            int doorLoopSize = -1;

            int loopSize = 1;
            long value = 1;
            long subjectNumber = 7;

            while (cardLoopSize == -1 || doorLoopSize == -1)
            {
                value *= subjectNumber;
                value = value % 20201227;

                if (cardLoopSize == -1 && cardPublicKey == value)
                {
                    cardLoopSize = loopSize;
                }

                if (doorLoopSize == -1 && doorPublicKey == value)
                {
                    doorLoopSize = loopSize;
                }

                loopSize++;
            }

            Console.WriteLine("Card loop size: {0}", cardLoopSize);
            Console.WriteLine("Door loop size: {0}", doorLoopSize);

            subjectNumber = doorPublicKey;
            value = 1;
            for (var i = 1; i <= cardLoopSize; i++)
            {
                value *= subjectNumber;
                value = value % 20201227;
            }

            Console.WriteLine("Encryption key: {0}", value);
        }
    }
}