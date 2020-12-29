using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            ISolution solution = new Puzzle13.Part1.Solution();
            solution.Run();

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
