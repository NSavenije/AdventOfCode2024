#nullable disable

static class Day8 {
    
    public static void Solve1() => Solve(false);

    public static void Solve2() => Solve(true);

    static void Solve(bool harmonics)
    {
        string filePath = "src/Day8/8.in";
        List<string> input = File.ReadAllLines(filePath).ToList();  

        Dictionary<char, List<(int x,int y)>> map = [];
        for(int x = 0; x < input.Count; x++)
        {
            for(int y = 0; y < input.Count; y++)
            {
                char test = input[y][x];
                if (test == '.')
                    continue;
                
                if (!map.TryGetValue(test, out var values))
                    values = [];

                values.Add((x,y));
                map[test] = values;
            }
        }

        HashSet<(int x, int y)> antiNodes = [];
        foreach(var key in map.Keys)
        {
            antiNodes.UnionWith(GetAntiNodes(map[key], 0, input.Count, harmonics));
        }
        Console.WriteLine(antiNodes.Count);
    }

    static HashSet<(int,int)> GetAntiNodes(List<(int x, int y)> nodes, int min, int max, bool harmonics)
    {
        HashSet<(int, int)> antiNodes = [];

        for(int i = 0; i < nodes.Count; i++)
        {
            for(int j = i + 1; j < nodes.Count; j++)
            {
                var n1 = nodes[i];
                var n2 = nodes[j];
                int[] dx = [n1.x - n2.x, n2.x - n1.x];
                int[] dy = [n1.y - n2.y, n2.y - n1.y];
                int[] x = [n1.x, n2.x];
                int[] y = [n1.y, n2.y];
                for (int k = 0; k < 2; k++) 
                { 
                    if(!harmonics)
                    {
                        x[k] += dx[k]; 
                        y[k] += dy[k];
                        if (x[k] >= min && x[k] < max && y[k] >= min && y[k] < max) 
                        { 
                            antiNodes.Add((x[k], y[k]));
                        }
                    }
                    else
                    {
                        while (x[k] >= min && x[k] < max && y[k] >= min && y[k] < max) 
                        { 
                            antiNodes.Add((x[k], y[k]));
                            x[k] += dx[k];
                            y[k] += dy[k];
                        }
                    }
                }
            }
        }
        return antiNodes;
    }
}