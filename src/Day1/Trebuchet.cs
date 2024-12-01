#nullable disable

static class Day1 {
    public static void Solve1() =>
        ParseInput();
    public static void Solve2() =>
        ParseInput();

    static void ParseInput()
    {
        string filePath = "src/Day1/1.in";
        using StreamReader sr = new(filePath);
        string line;
        int res = 0;
        List<int> list1 = [];
        List<int> list2 = [];
        Dictionary<int, int> groups = [];
        while ((line = sr.ReadLine()) != null)
        {
            // Console.WriteLine(line);
            var parts = line.Split();
            // Console.WriteLine(parts.Length);
            list1.Add(int.Parse(parts[0]));
            list2.Add(int.Parse(parts[3])); 
        }
        list1.Sort();
        list2.Sort();
        for(int i = 0; i < list2.Count; i++)
        {
            if (groups.TryGetValue(list2[i], out _))
                groups[list2[i]]++;
            else
                groups[list2[i]] = 1;
        }
        for(int i = 0; i < list1.Count; i++)
        {
            // Console.WriteLine(list1[i]);
            // Console.WriteLine(list2[i]);
            // res += Math.Abs(list1[i] - list2[i]);
            if (groups.TryGetValue(list1[i], out var val))
                res += list1[i] * groups[list1[i]];
        }
        Console.WriteLine(res);
    }
}