﻿namespace AdventOfCode.Y2021.Puzzle12.Part2
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

            FindPathsFrom("start", new());

            Console.WriteLine(_foundPaths);
        }

        private void FindPathsFrom(string currentCave, List<string> currentPath)
        {
            if (currentCave != "end" && 
                IsSmallCave(currentCave) && 
                currentPath.Contains(currentCave) && 
                (currentCave == "start" || AnySmallCaveHasBeenVisitedTwice(currentPath)))
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
                    FindPathsFrom(connection.Start, new(currentPath));
                }

                if (connection.End != currentCave)
                {
                    FindPathsFrom(connection.End, new(currentPath));
                }
            }
        }

        private bool IsSmallCave(string cave) => char.IsLower(cave[0]);

        private bool AnySmallCaveHasBeenVisitedTwice(List<string> path)
        {
            var visitedSmallCaves = new List<string>();

            foreach (var cave in path.Where(c => IsSmallCave(c)))
            {
                if (visitedSmallCaves.Contains(cave))
                {
                    return true;
                }

                visitedSmallCaves.Add(cave);
            }

            return false;
        }
    }

    public record Connection(string Start, string End);
}