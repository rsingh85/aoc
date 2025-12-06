namespace AdventOfCode.Y2025.Puzzle4.Part1
{
    public partial class Solution : ISolution
    {
        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            const int AdjacentRollsThreshold = 4;
            var accessibleCount = 0;

            for (var r = 0; r < lines.Length; r++)
            {
                for (var c = 0; c < lines[r].Length; c++)
                {
                    var current = lines[r][c];

                    if (current != '@')
                        continue;

                    var adjacents = new List<char>
                    {
                        r == 0 ? '.' : lines[r - 1][c], // top
                        r == 0 || c == lines[r].Length - 1 ? '.' : lines[r - 1][c + 1], // top-right
                        c == lines[r].Length - 1 ? '.' : lines[r][c + 1], // right
                        r == lines.Length - 1 || c == lines[r].Length - 1 ? '.' : lines[r + 1][c + 1], // bottom-right
                        r == lines.Length - 1 ? '.' : lines[r + 1][c], // bottom
                        r == lines.Length - 1 || c == 0 ? '.' : lines[r + 1][c - 1], // bottom-left
                        c == 0 ? '.' : lines[r][c - 1], // left
                        r == 0 || c == 0 ? '.' : lines[r - 1][c - 1], // top-left
                    };

                    if (adjacents.Count(a => a == '@') < AdjacentRollsThreshold)
                        accessibleCount++;
                }
            }

            Console.WriteLine(accessibleCount);
        }
    }
}