namespace adventofcode2022 {
    using Day12Items;

    namespace Day12Items {
        public readonly struct Vector2 {
            public readonly int X { get; }
            public readonly int Y { get; }

            public Vector2(int x, int y) {
                this.X = x;
                this.Y = y;
            }

            public override string ToString() => $"({X}, {Y})";
        }

        public abstract class Grid<T> {
            protected readonly IReadOnlyList<T> array;
            protected readonly int width, height;
            protected Grid(IReadOnlyList<T> array, int width) {
                this.array = array;
                this.width = width;
                this.height = array.Count / width;
            }
            public T this[int x, int y] => array[x + y * width];
            public T this[Vector2 position] => this[position.X, position.Y];
        }

        public class HeightMap : Grid<int> {
            protected HeightMap(IReadOnlyList<int> array, int width) : base(array, width) { }

            public static HeightMap Parse(IEnumerable<string> input) {
                int width = input.First().Length;
                int height = input.Count();
                int[] array = new int[width * height];
                int y = 0;
                foreach (var line in input) {
                    for (int x = 0; x < line.Length; x++) {
                        var c = line[x] switch {
                            'S' => 'a',
                            'E' => 'z',
                            var other => other
                        };
                        int elevation = c - 'a';
                        array[x + y * width] = elevation;
                    }
                    y++;
                }
                return new HeightMap(array, width);
            }

            private IEnumerable<Vector2> GetNeighbours(Vector2 current) {
                int x = current.X;
                int y = current.Y;
                if (x + 1 < width) yield return new Vector2(x + 1, y);
                if (x - 1 >= 0) yield return new Vector2(x - 1, y);
                if (y + 1 < height) yield return new Vector2(x, y + 1);
                if (y - 1 >= 0) yield return new Vector2(x, y - 1);
            }

            public Graph ToGraph(bool reverse = false) {
                Graph graph = new();
                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        Vector2 current = new(x, y);
                        foreach (var neighbour in GetNeighbours(current))
                            if (this[neighbour] <= this[current] + 1)
                                if (reverse)
                                    graph.AddEdge(neighbour, current);
                                else
                                    graph.AddEdge(current, neighbour);
                    }
                }
                return graph;
            }

            public override string ToString() {
                System.Text.StringBuilder sb = new();
                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        sb.Append(this[x, y]);
                        sb.Append(' ');
                    }
                    sb.Append('\n');
                }
                return sb.ToString();
            }
        }

        public class Graph {
            Dictionary<Vector2, HashSet<Vector2>> adj = new();

            public void AddVertex(Vector2 vertex) {
                if (!adj.ContainsKey(vertex)) {
                    adj.Add(vertex, new HashSet<Vector2>());
                }
            }

            public void AddEdge(Vector2 from, Vector2 to) {
                AddVertex(from);
                AddVertex(to);
                adj[from].Add(to);
            }

            public IReadOnlySet<Vector2> GetNeighbours(Vector2 vertex) => adj[vertex];

            public Dictionary<Vector2, int> GetDistances(Vector2 from, Vector2? to = null) {
                HashSet<Vector2> unvisited = new(adj.Keys);
                Dictionary<Vector2, int> distances = adj.Keys.ToDictionary(v => v, v => int.MaxValue);
                distances[from] = 0;
                Vector2 current = from;
                while (true) {
                    if (distances[current] == int.MaxValue)
                        break; // no path found
                    int distance = distances[current] + 1;
                    foreach (var neighbour in GetNeighbours(current)) {
                        if (distances[neighbour] > distance)
                            distances[neighbour] = distance;
                    }
                    unvisited.Remove(current);
                    if (to.HasValue && !unvisited.Contains(to.Value))
                        break; // found target
                    if (unvisited.Count == 0)
                        break; // target not found
                    current = distances.Where(kv => unvisited.Contains(kv.Key)).MinBy(kv => kv.Value).Key;
                }
                return distances;
            }

            public int FindShortestPathCost(Vector2 from, Vector2 to) {
                var distances = GetDistances(from, to);
                return distances[to];
            }
        }
    }

    public class Day12 : IDay {
        HeightMap heightmap;
        Vector2 start;
        Vector2 end;

        public Day12() : this(Utils.GetInputLines(12)) { }
        public Day12(IEnumerable<string> input) {
            heightmap = HeightMap.Parse(input);
            int y = 0;
            bool foundStart = false,
                 foundEnd = false;
            foreach (var line in input) {
                if (!foundStart) {
                    int x = line.IndexOf('S');
                    if (x > -1) start = new(x, y);
                }
                if (!foundEnd) {
                    int x = line.IndexOf('E');
                    if (x > -1) end = new(x, y);
                }
                if (foundStart && foundEnd) break;
                y++;
            }
        }

        public object Part1() {
            var graph = heightmap.ToGraph();
            return graph.FindShortestPathCost(start, end);
        }

        public object Part2() {
            var graph = heightmap.ToGraph(reverse: true);
            var distances = graph.GetDistances(end);
            return distances.Where(kv => heightmap[kv.Key] == 0).MinBy(kv => kv.Value).Value;
        }
    }
}