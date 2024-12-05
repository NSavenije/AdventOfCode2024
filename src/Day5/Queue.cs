#nullable disable

using System.Data;

static class Day5 {
    public static void Solve1() {
        string filePath = "src/Day5/5.in";
        using StreamReader sr = new(filePath);
        string line;
        Dictionary<int, HashSet<int>> rules = [];
        int res = 0;

        while((line = sr.ReadLine()) != "")
        {
            int key = int.Parse(line.Split('|')[0]);
            int val = int.Parse(line.Split('|')[1]);
            if (rules.TryGetValue(val, out var rs))
            {
                rs.Add(key);
            }
            else
            {
                rules.Add(val, [ key ]);
            }
        }
    
        while((line = sr.ReadLine()) != null)
        {
            var updates = line.Split(',').Select(int.Parse).ToList();
            bool healthy = CheckHealth(updates, rules);
            
            if (healthy)
            {
                res += updates[updates.Count / 2];
            }
        }
        Console.WriteLine(res);
    }

    public static void Solve2()
    {
        string filePath = "src/Day5/5.in";
        using StreamReader sr = new(filePath);
        string line;
        Dictionary<int, HashSet<int>> rules = [];
        int res = 0;

        while((line = sr.ReadLine()) != "")
        {
            int key = int.Parse(line.Split('|')[0]);
            int val = int.Parse(line.Split('|')[1]);
            if (rules.TryGetValue(val, out var rs))
            {
                rs.Add(key);
            }
            else
            {
                rules.Add(val, [ key ]);
            }
        }
    
        while((line = sr.ReadLine()) != null)
        {
            List<int> list = line.Split(',').Select(int.Parse).ToList();
            bool healthy = true;
            while (!CheckHealth(list, rules))
            {   
                healthy = false;
                for(int i = 0; i < list.Count; i++)
                {
                    var afters = list[(i+1)..];
                    if (rules.TryGetValue(list[i], out var after))
                    {
                        foreach(var illegal in afters.Intersect(after))
                        {
                            list.Remove(illegal);
                            list.Insert(i,illegal);
                            i++;
                        }
                    }
                }
            }
            if (!healthy) res += list[list.Count / 2];
        }
        Console.WriteLine(res);
    }



    private static bool CheckHealth(List<int> updates, Dictionary<int, HashSet<int>> rules) {
        for (int i = 0; i < updates.Count; i++)
        {
            var befores = updates[(i+1)..];
            if (rules.TryGetValue(updates[i], out var before))
            {
                if(befores.Intersect(before).Any())
                { 
                    return false; 
                }
            }
        }
        return true;
    }
    
}

