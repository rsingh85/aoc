namespace AdventOfCode.Y2024.Puzzle11.Part1
{
    public class Solution : ISolution
    {
        public void Run()
        {
            var stones = File.ReadAllLines(Helper.GetInputFilePath(this)).First().Split(' ').Select(long.Parse).ToList();

            const int MaxBlinks = 25;

            for (var blinks = 0; blinks < MaxBlinks; blinks++)
            {
                var newStones = new List<long>();

                foreach (var stone in stones)
                {
                    if (stone == 0)
                    {
                        newStones.Add(1);
                    }
                    else if (stone.ToString().Length % 2 == 0)
                    {
                        var stoneEngraving = stone.ToString();
                        var leftStone = long.Parse(stoneEngraving.Substring(0, stoneEngraving.Length / 2));
                        var rightStone = long.Parse(stoneEngraving.Substring(stoneEngraving.Length / 2));

                        newStones.Add(leftStone);
                        newStones.Add(rightStone);
                    }
                    else
                    {
                        newStones.Add(stone * 2024);
                    }
                }

                stones = newStones;
            }

            Console.WriteLine(stones.Count());
        }
    }
}