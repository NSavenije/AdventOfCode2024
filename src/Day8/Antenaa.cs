#nullable disable

using System.Data;
using System.Text;

static class Day8 {
    
    public static void Solve1() => ParseInput(false);

    public static void Solve2() => ParseInput(true);

    static void ParseInput(bool b)
    {
        Dictionary<char, List<(int x,int y)>> map = [];
        string filePath = "src/Day8/8.in";
        List<string> input = File.ReadAllLines(filePath).ToList();  
        for(int x = 0; x < input.Count; x++)
        {
            for(int y = 0; y < input.Count; y++)
            {
                char test = input[y][x];
                if (test != '.')
                {
                    if (map.TryGetValue(test, out var values))
                    {
                        values.Add((x,y));
                        map[test] = values;
                    }
                    else
                    {
                        map.Add(test, [(x,y)]);
                    }
                }
            }
        }

        HashSet<(int x, int y)> antiNodes = [];
        foreach(var key in map.Keys)
        {
            antiNodes.UnionWith(GetAntiNodes(map[key], 0, input.Count));
        }
        Console.WriteLine(antiNodes.Count);
    }

    static HashSet<(int,int)> GetAntiNodes(List<(int x, int y)> nodes, int min, int max)
    {
        HashSet<(int, int)> antiNodes = [];

        for(int i = 0; i < nodes.Count; i++)
        {
            for(int j = i + 1; j < nodes.Count; j++)
            {
                var n1 = nodes[i];
                var n2 = nodes[j];
                int dx1 = n1.x - n2.x;
                int dx2 = n2.x - n1.x;
                int dy1 = n1.y - n2.y;
                int dy2 = n2.y - n1.y;
                int x1 = n1.x + dx1;
                int y1 = n1.y + dy1;
                int x2 = n2.x + dx2;
                int y2 = n2.y + dy2;
                if (x1 >= min && x1 < max && y1 >= min && y1 < max)
                    antiNodes.Add((x1,y1));
                if (x2 >= min && x2 < max && y2 >= min && y2 < max)
                    antiNodes.Add((x2,y2));
            }
        }

        return antiNodes;
    }
}