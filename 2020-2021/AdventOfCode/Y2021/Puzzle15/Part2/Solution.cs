namespace AdventOfCode.Y2021.Puzzle15.Part2
{
    /// <summary>
    /// Requires optimising, currently very slow but gets the answer
    /// </summary>
    public class Solution : ISolution
    {
        private int[,] _grid;
        private int _gridMaxC, _gridMaxR;

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this)).ToList();

            lines = ExpandLines(lines);

            _gridMaxR = lines.Count;
            _gridMaxC = lines.First().Length;
            _grid = new int[_gridMaxR, _gridMaxC];

            for (var r = 0; r < _gridMaxR; r++)
            {
                for (var c = 0; c < _gridMaxC; c++)
                {
                    _grid[r, c] = int.Parse(lines[r][c].ToString());
                }
            }

            var vertexInfos = ComputePaths();
            var targetVertexInfo = vertexInfos.Single(vi => vi.Vertex.R == _gridMaxR - 1 && vi.Vertex.C == _gridMaxC - 1);

            Console.WriteLine(targetVertexInfo.ShortestRiskDistanceFromStart);
        }

        private List<string> ExpandLines(List<string> original)
        {
            var expanded = new List<string>(original);

            // expand right
            for (var i = 1; i < 5; i++)
            {
                for (var j = 0; j < original.Count; j++)
                {
                    expanded[j] += CreateNewLine(original[j], i);
                }
            }

            // expand down
            for (var i = 1; i < 5; i++)
            {
                for (var j = 0; j < original.Count; j++)
                {
                    expanded.Add(CreateNewLine(expanded[j], i));
                }
            }

            return expanded;
        }

        private string CreateNewLine(string line, int additions)
        {
            var newLine = string.Empty;

            foreach (var digitChar in line)
            {
                var digit = int.Parse(digitChar.ToString());

                for (var i = 1; i <= additions; i++)
                {
                    digit++;
                    digit = digit > 9 ? 1 : digit;
                }

                newLine += digit;
            }

            return newLine;
        }

        private List<VertexInfo> ComputePaths()
        {
            var vertexInfos = new List<VertexInfo>();

            for (var r = 0; r < _gridMaxR; r++)
            {
                for (var c = 0; c < _gridMaxC; c++)
                {
                    vertexInfos.Add(new VertexInfo
                    {
                        Vertex = new Vertex(r, c),
                        Risk = _grid[r, c],
                        ShortestRiskDistanceFromStart = r == 0 && c == 0 ? 0 : int.MaxValue
                    });
                }
            }

            var visited = 1;

            while (vertexInfos.Any(i => !i.Visited))
            {
                var unvisitedVertexInfos =
                    vertexInfos.Where(i => !i.Visited).ToList();

                var currentVertexInfo =
                    unvisitedVertexInfos
                        .OrderBy(i => i.ShortestRiskDistanceFromStart)
                        .First();

                var unvisitedNeighbourVertices =
                    unvisitedVertexInfos
                        .Where(uv =>
                            (uv.Vertex.R == currentVertexInfo.Vertex.R - 1 && uv.Vertex.C == currentVertexInfo.Vertex.C) || // top
                            (uv.Vertex.R == currentVertexInfo.Vertex.R && uv.Vertex.C == currentVertexInfo.Vertex.C + 1) || // right
                            (uv.Vertex.R == currentVertexInfo.Vertex.R + 1 && uv.Vertex.C == currentVertexInfo.Vertex.C) || // bottom
                            (uv.Vertex.R == currentVertexInfo.Vertex.R && uv.Vertex.C == currentVertexInfo.Vertex.C - 1)    // left
                        ).ToList();

                foreach (var vertexInfo in unvisitedNeighbourVertices)
                {
                    var distanceFromStartVertex =
                        currentVertexInfo.ShortestRiskDistanceFromStart + vertexInfo.Risk;

                    if (distanceFromStartVertex < vertexInfo.ShortestRiskDistanceFromStart)
                    {
                        vertexInfo.ShortestRiskDistanceFromStart = distanceFromStartVertex;
                        vertexInfo.PreviousVertex = currentVertexInfo.Vertex;
                    }
                }

                if (visited % 1000 == 0)
                {
                    Console.WriteLine("Visited {0} vertices", visited);
                }

                visited++;
                currentVertexInfo.Visited = true;
            }

            return vertexInfos;
        }
    }

    public record Vertex(int R, int C)
    {
        public override string ToString() => $"{R},{C}";
    }

    public class VertexInfo
    {
        public Vertex Vertex { get; set; }
        public int Risk { get; set; }
        public bool Visited { get; set; }
        public int ShortestRiskDistanceFromStart { get; set; }
        public Vertex? PreviousVertex { get; set; }
        public override string ToString() => Vertex.ToString();
    }
}