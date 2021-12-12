using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            ISolution solution = new Y2021.Puzzle12.Part2.Solution();

            Console.WriteLine(solution.GetType().Namespace);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            solution.Run();
            stopwatch.Stop();

            Console.WriteLine("Complete - solution runtime {0}ms", stopwatch.Elapsed.TotalMilliseconds);
            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}