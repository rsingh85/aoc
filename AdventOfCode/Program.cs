using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            ISolution solution = new Y2021.Puzzle5.Part2.Solution();

            Console.WriteLine(solution.GetType().Namespace);

            solution.Run();

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
