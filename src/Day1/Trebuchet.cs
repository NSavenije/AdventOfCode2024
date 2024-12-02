#nullable disable

static class Day1 {
    public static void Solve1() =>
        ParseInput(true);
    public static void Solve2() =>
        ParseInput(false);

    static void ParseInput(bool part1)
    {
        string filePath = "src/Day1/1.in";
        using StreamReader sr = new(filePath);
        string line;
        int res = 0;
        List<int> list1 = [];
        List<int> list2 = [];
        while ((line = sr.ReadLine()) != null)
        {
            var parts = line.Split();
            list1.Add(int.Parse(parts[0]));
            list2.Add(int.Parse(parts[3])); 
        }
        
        if (part1) {
            list1.Sort();
            list2.Sort();
            for(int i = 0; i < list1.Count; i++)
                res += Math.Abs(list1[i] - list2[i]);
        }

        else {
            
            var g1 = list1.GroupBy( i => i );
            var g2 = list2.GroupBy( i => i ).ToDictionary(g => g.Key, g => g.Count());
            res = g1.Select( i => i.Key * i.Count() * (g2.TryGetValue(i.Key, out int count) ? count : 0))
                    .Sum();
        }
        Console.WriteLine(res);
    }
}