using System.Text.RegularExpressions;

namespace AdventOfCode.Y2021.Puzzle12.Part1
{
    public class Solution : ISolution
    {
        private List<Connection> _connections = new List<Connection>();
        private List<string> _foundPaths = new List<string>();

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            foreach (var line in lines)
            {
                var splitLine = line.Split("-");
                _connections.Add(new Connection(splitLine[0], splitLine[1]));
            }

            FindPathsFrom("start", Enumerable.Empty<string>().ToList());

            Console.WriteLine(_foundPaths.Count);
        }

        private void FindPathsFrom(string currentCave, List<string> currentPath)
        {
            if (IsSmallCave(currentCave) && currentPath.Contains(currentCave))
            {
                return;
            }

            currentPath.Add(currentCave);

            if (currentCave == "end")
            {
                _foundPaths.Add(string.Join(',', currentPath));
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

        private bool IsSmallCave(string cave)
        {
            if (cave == "end")
            {
                return false;
            }    

            return Regex.IsMatch(cave, "[a-z]+");
        }
    }

    public record Connection(string Start, string End)
    {
        public override string ToString()
        {
            return $"{Start}-{End}";
        }
    }
}