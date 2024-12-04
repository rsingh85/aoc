namespace AdventOfCode.Y2024.Puzzle4.Part2
{
    public class Solution : ISolution
    {

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this)).ToArray();
            var searchWordOne = "MAS";
            var searchWordTwo = "SAM";
            var occurrences = 0;

            for (var r = 0; r < lines.Length; r++)
            {
                for (var c = 0; c < lines[r].Length; c++)
                {
                    var searchedWords = new List<string> {
                        GetWordInDirection(r, c, searchWordOne.Length, lines, Direction.RD),
                        GetWordInDirection(r + 2, c, searchWordOne.Length, lines, Direction.UR),
                    };

                    if (searchedWords.All(sw => sw == searchWordOne || sw == searchWordTwo))
                        occurrences++;
                }
            }

            Console.WriteLine(occurrences);
        }

        private string GetWordInDirection(int r, int c, int length, string[] lines, Direction direction)
        {
            var word = string.Empty;

            if (r < 0 || r >= lines.Length)
                return word;

            if (c < 0 || c >= lines[r].Length)
                return word;
                
            word += lines[r][c];

            for (var i = 0; i < length - 1; i++)
            {
                var nextPosition = GetNextPositionByDirection(r, c, direction);
                r = nextPosition.r;
                c = nextPosition.c;

                if (r < 0 || r >= lines.Length)
                    return word;

                if (c < 0 || c >= lines[r].Length)
                    return word;

                word += lines[r][c];
            }

            return word;
        }

        private (int r, int c) GetNextPositionByDirection(int r, int c, Direction direction)
        {
            return direction switch
            {
                Direction.U => (r - 1, c),
                Direction.UR => (r - 1, c + 1),
                Direction.R => (r, c + 1),
                Direction.RD => (r + 1, c + 1),
                Direction.D => (r + 1, c),
                Direction.LD => (r + 1, c - 1),
                Direction.L => (r, c - 1),
                Direction.LU => (r - 1, c - 1),
                _ => (-1, -1),
            };
        }
        
        private enum Direction
        {
            U, UR, R, RD, D, LD, L, LU
        }
    }
}