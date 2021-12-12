namespace AdventOfCode.Y2021.Puzzle12.Part1
{
    public class Solution : ISolution
    {
        private List<Connection> _connections = new();
        private int _foundPaths;

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            foreach (var line in lines)
            {
                var splitLine = line.Split("-");
                _connections.Add(new(splitLine[0], splitLine[1]));
            }

            FindPathsFrom("start", Enumerable.Empty<string>().ToList());

            Console.WriteLine(_foundPaths);
        }

        private void FindPathsFrom(string currentCave, List<string> currentPath)
        {
            if (currentCave != "end" && 
                IsSmallCave(currentCave) && 
                currentPath.Contains(currentCave))
            {
                return;
            }

            currentPath.Add(currentCave);

            if (currentCave == "end")
            {
                _foundPaths++;
                return;
            }

            var connectionsFromCurrentCave = _connections.Where(c => c.Start == currentCave || c.End == currentCave);

            foreach (var connection in connectionsFromCurrentCave)
            {
                if (connection.Start != currentCave)
                {
                    FindPathsFrom(connection.Start, new List<string>(currentPath));
                }

                if (connection.End != currentCave)
                {
                    FindPathsFrom(connection.End, new List<string>(currentPath));
                }
            }
        }

        private bool IsSmallCave(string cave) => char.IsLower(cave[0]);
    }

    public record Connection(string Start, string End);
}