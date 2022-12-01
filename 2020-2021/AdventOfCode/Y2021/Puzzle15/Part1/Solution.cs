namespace AdventOfCode.Y2021.Puzzle15.Part1
{
    /// <summary>
    /// Re-learned and used Dijkstra's Shorting Path algorithm.
    /// Useful video with pseudo code: 
    /// https://www.youtube.com/watch?v=pVfj6mxhdMw&ab_channel=ComputerScience 
    /// </summary>
    public class Solution : ISolution
    {
        private int[,] _grid;
        private int _gridMaxC, _gridMaxR;

        public void Run()
        {
            var lines = File.ReadAllLines(Helper.GetInputFilePath(this));

            _gridMaxR = lines.Length;
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

        private List<VertexInfo> ComputePaths()
        {
            var visited = new HashSet<Vertex>();
            var unvisited = new HashSet<Vertex>();
            var vertexInfos = new List<VertexInfo>();

            for (var r = 0; r < _gridMaxR; r++)
            {
                for (var c = 0; c < _gridMaxC; c++)
                {
                    var vertex = new Vertex(r, c);

                    unvisited.Add(vertex);
                    vertexInfos.Add(new VertexInfo
                    {
                        Vertex = vertex,
                        ShortestRiskDistanceFromStart = r == 0 && c == 0 ? 0 : int.MaxValue
                    });
                }
            }

            while (unvisited.Any())
            {
                var currentVertexInfo = 
                    vertexInfos
                        .Where(i => unvisited.Contains(i.Vertex))
                        .OrderBy(i => i.ShortestRiskDistanceFromStart).First();

                var currentVertex = currentVertexInfo.Vertex;

                var unvisitedNeighbourVertices =
                    unvisited
                        .Where(uv =>
                            (uv.R == currentVertex.R - 1 && uv.C == currentVertex.C) || // top
                            (uv.R == currentVertex.R && uv.C == currentVertex.C + 1) || // right
                            (uv.R == currentVertex.R + 1 && uv.C == currentVertex.C) || // bottom
                            (uv.R == currentVertex.R && uv.C == currentVertex.C - 1)    // left
                        );

                foreach (var vertex in unvisitedNeighbourVertices)
                {
                    var distanceFromStartVertex =
                        currentVertexInfo.ShortestRiskDistanceFromStart + 
                            _grid[vertex.R, vertex.C];

                    var unvisitedNeighbourVertexInfo = vertexInfos.Single(vi => vi.Vertex == vertex);

                    if (distanceFromStartVertex < unvisitedNeighbourVertexInfo.ShortestRiskDistanceFromStart)
                    {
                        unvisitedNeighbourVertexInfo.ShortestRiskDistanceFromStart = distanceFromStartVertex;
                        unvisitedNeighbourVertexInfo.PreviousVertex = currentVertex;
                    }
                }

                visited.Add(currentVertex);
                unvisited.Remove(currentVertex);
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
        public int ShortestRiskDistanceFromStart { get; set; }
        public Vertex? PreviousVertex { get; set; }
        public override string ToString() => Vertex.ToString();
    }
}