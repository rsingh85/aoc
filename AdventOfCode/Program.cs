using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            ISolution solution = new Y2015.Puzzle1.Part2.Solution();
            solution.Run();

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
