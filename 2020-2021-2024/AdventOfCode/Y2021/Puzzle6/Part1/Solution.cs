namespace AdventOfCode.Y2021.Puzzle6.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            const int MaxDays = 80;

            var timers = File.ReadAllLines(Helper.GetInputFilePath(this)).First().Split(",").Select(int.Parse).ToList();

            for (var day = 1; day <= MaxDays; day++)
            {
                var timersCount = timers.Count();

                for (var i = 0; i < timersCount; i++)
                {
                    var timer = timers[i];

                    if (timer == 0)
                    {
                        timers[i] = 6;
                        timers.Add(8);
                        continue;
                    }

                    timers[i] = --timer;
                }
            }

            Console.WriteLine(timers.Count());
        }
    }
}