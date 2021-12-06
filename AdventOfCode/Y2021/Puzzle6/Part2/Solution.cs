namespace AdventOfCode.Y2021.Puzzle6.Part2
{
    public class Solution : ISolution
    {
        // Optimised solution as my Puzzle6.Part1 implementation would take too long as MaxDays is increased
        public void Run()
        {
            const int MaxDays = 256;
            const int MaxTimer = 8;

            var timers = File.ReadAllLines(Helper.GetInputFilePath(this)).First().Split(",").Select(int.Parse);
            var timerCounts = new long[MaxTimer + 1];

            foreach (var timer in timers)
            {
                timerCounts[timer]++;
            }

            for (var day = 1; day <= MaxDays; day++)
            {
                var zeroTimersCount = timerCounts[0];

                for (var timer = 0; timer <= MaxTimer; timer++)
                {
                    if (timer == 8)
                    {
                        timerCounts[timer] = zeroTimersCount;
                        continue;
                    }

                    timerCounts[timer] = timerCounts[timer + 1];

                    if (timer == 6)
                    {
                        timerCounts[timer] += zeroTimersCount;
                    }
                }
            }

            Console.WriteLine(timerCounts.Sum());
        }
    }
}