#nullable disable

static class Day23
{

    public static void Solve1()
    {
        const string filePath = "src/Day23/23.in";
        var connStrings = File.ReadAllLines(filePath);

        Dictionary<string,HashSet<string>> connections = ConstructGraph(connStrings);

        var triplets = new List<(string, string, string)>();

        foreach (var (computer1, neighbors1) in connections)
        {
            foreach (var neighbor in neighbors1)
            {
                if (connections.TryGetValue(neighbor, out var neighbors2))
                {
                    foreach (var neighborOfNeighbor in neighbors2)
                    {
                        if (neighborOfNeighbor != computer1 && 
                            connections[computer1].Contains(neighborOfNeighbor) && 
                            connections[neighborOfNeighbor].Contains(computer1))
                        {
                            string[] triplet = [computer1, neighbor, neighborOfNeighbor];
                            Array.Sort(triplet);
                            var tripletTuple = (triplet[0], triplet[1], triplet[2]);

                            if (!triplets.Contains(tripletTuple))
                            {
                                triplets.Add(tripletTuple);
                            }
                        }
                    }
                }
            }
        }
        int tripletsCount = 0;
        foreach (var (a, b, c) in triplets)
            if (a[0] == 't' || b[0] == 't' || c[0] == 't')
                tripletsCount++;

        Console.WriteLine(tripletsCount);
    }

    public static void Solve2()
    {
        const string filePath = "src/Day23/23.in";
        var connStrings = File.ReadAllLines(filePath);
        Dictionary<string,HashSet<string>> connections = ConstructGraph(connStrings);
        
        List<string> maxClique = FindMaximumClique(connections).ToList();
        maxClique.Sort();

        Console.WriteLine(string.Join(',', maxClique));
    }

    private static Dictionary<string,HashSet<string>> ConstructGraph(string[] vertices)
    {
        Dictionary<string,HashSet<string>> graph = [];
        foreach (string v in vertices)
        {
            var nodes = v.Split('-');

            if (!graph.TryGetValue(nodes[0], out _))
            {
                graph[nodes[0]] = [];
            }

            graph[nodes[0]].Add(nodes[1]);

            if (!graph.TryGetValue(nodes[1], out _))
            {
                graph[nodes[1]] = [];
            }

            graph[nodes[1]].Add(nodes[0]);
        }
        return graph;
    }

    private static HashSet<string> FindMaximumClique(Dictionary<string,HashSet<string>> connections)
    {
        var r = new HashSet<string>();
        var p = new HashSet<string>(connections.Keys);
        var x = new HashSet<string>();

        var maxClique = BronKerbosch(connections, r, p, x);

        return maxClique;
    }

    //https://en.wikipedia.org/wiki/Bron%E2%80%93Kerbosch_algorithm#With_pivoting
    private static HashSet<string> BronKerbosch(Dictionary<string,HashSet<string>> connections, HashSet<string> r, HashSet<string> p, HashSet<string> x)
    {
        HashSet<string> maxClique = [];
        // if P and X are both empty then report R as a maximal clique
        if (p.Count == 0 && x.Count == 0)
        {
            return r.Count > maxClique.Count ? [.. r] : maxClique;
        }

        // choose a pivot vertex u in P ⋃ X
        var pivot = p.Concat(x).First();
        // N(u)
        var neighbors = connections.TryGetValue(pivot, out var conn) ? conn : [];
        //for each vertex v in P \ N(u) do
        foreach (var vertex in p.Except(neighbors).ToList())
        {
            // BronKerbosch2(R ⋃ {v}, P ⋂ N(v), X ⋂ N(v))
            HashSet<string> newp = [.. p.Intersect(connections[vertex])];
            HashSet<string> newx = [.. x.Intersect(connections[vertex])];

            var clique = BronKerbosch(connections, r.Union([vertex]).ToHashSet(), newp, newx);
            maxClique = clique.Count > maxClique.Count ? clique : maxClique;

            // P := P \ {v}
            p.Remove(vertex);
            // X := X ⋃ {v}
            x.Add(vertex);
        }
        return maxClique;
    }
}
