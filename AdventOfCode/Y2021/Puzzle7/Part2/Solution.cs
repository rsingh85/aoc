namespace AdventOfCode.Y2021.Puzzle7.Part2
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var positions = File.ReadAllLines(Helper.GetInputFilePath(this)).First().Split(",").Select(int.Parse);
            var maxTargetPosition = positions.Max();
            var targetPositions = new List<TargetPosition>();

            for (var targetPosition = 0; targetPosition < maxTargetPosition; targetPosition++)
            {
                var totalFuelForPosition = 0;

                foreach (var currentPosition in positions)
                {
                    var fuelCost = Math.Abs(targetPosition - currentPosition);
                    totalFuelForPosition += fuelCost * (fuelCost + 1) / 2;
                }

                targetPositions.Add(new TargetPosition { Position = targetPosition, TotalFuelCost = totalFuelForPosition });
            }

            Console.WriteLine(targetPositions.OrderBy(tp => tp.TotalFuelCost).First().TotalFuelCost);
        }
    }

    public struct TargetPosition
    {
        public int Position { get; set; }
        public int TotalFuelCost { get; set; }
    }
}