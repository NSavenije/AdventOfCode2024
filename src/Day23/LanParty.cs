#nullable disable

static class Day23
{

    public static void Solve1()
    {
        const string filePath = "src/Day23/23.in";
        var connStrings = File.ReadAllLines(filePath);

        Dictionary<string,HashSet<string>> connections = [];

        foreach (string conn in connStrings)
        {
            var computers = conn.Split('-');

            if (!connections.TryGetValue(computers[0], out _))
            {
                connections[computers[0]] = [];
            }

            connections[computers[0]].Add(computers[1]);

            if (!connections.TryGetValue(computers[1], out _))
            {
                connections[computers[1]] = [];
            }

            connections[computers[1]].Add(computers[0]);
        }

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
                            var triplet = new[] { computer1, neighbor, neighborOfNeighbor };
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
        {
            if (a[0] == 't' || b[0] == 't' || c[0] == 't')
            {
                tripletsCount++;
                // Console.WriteLine($"({a}, {b}, {c})");
            }
        }
        Console.WriteLine(tripletsCount);
    }

    public static void Solve2()
    {
    }

}