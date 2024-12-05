#nullable disable

using System;
using System.Data;
using System.Text.RegularExpressions;

static class Day5 {
    public static void Solve1() {
        string filePath = "src/Day5/5-small.in";
        using StreamReader sr = new("C:\\Users\\nouds\\Repos\\AdventOfCode2024\\src\\Day5\\5.in");
        string line;
        Dictionary<int, HashSet<int>> beforeRules = [];
        Dictionary<int, HashSet<int>> afterRules = [];
        int res = 0;

        while((line = sr.ReadLine()) != "")
        {
            int key = int.Parse(line.Split('|')[0]);
            int val = int.Parse(line.Split('|')[1]);
            if (beforeRules.TryGetValue(key, out var rules1))
            {
                rules1.Add(val);
            }
            else
            {
                beforeRules.Add(key, [ val ]);
            }

            if (afterRules.TryGetValue(val, out var rules2))
            {
                rules2.Add(key);
            }
            else
            {
                afterRules.Add(val, [ key ]);
            }
        }
    
        while((line = sr.ReadLine()) != null)
        {
            if(line == "61,13,29")
            {}
            int[] updates = line.Split(',').Select(int.Parse).ToArray();
            bool healthy = CheckHealth(updates, beforeRules);
            
            if (healthy)
            {
                Console.WriteLine(line);
                res += updates[updates.Length / 2];
            }
        }
        Console.WriteLine(res);
    }

    public static void Solve2()
    {
        string filePath = "src/Day5/5-small.in";
        using StreamReader sr = new("C:\\Users\\nouds\\Repos\\AdventOfCode2024\\src\\Day5\\5.in");
        string line;
        Dictionary<int, HashSet<int>> beforeRules = [];
        Dictionary<int, HashSet<int>> afterRules = [];
        int res = 0;

        while((line = sr.ReadLine()) != "")
        {
            int key = int.Parse(line.Split('|')[0]);
            int val = int.Parse(line.Split('|')[1]);
            if (beforeRules.TryGetValue(key, out var rules1))
            {
                rules1.Add(val);
            }
            else
            {
                beforeRules.Add(key, [ val ]);
            }

            if (afterRules.TryGetValue(val, out var rules2))
            {
                rules2.Add(key);
            }
            else
            {
                afterRules.Add(val, [ key ]);
            }
        }
    
        while((line = sr.ReadLine()) != null)
        {
            if(line == "61,13,29")
            {}
            List<int> list = line.Split(',').Select(int.Parse).ToList();
            bool healthy = true;
            while (!CheckHealth(list, beforeRules))
            {   
                healthy = false;
                Console.WriteLine("ILLEGAL: " + string.Join(',',list));
                
                // We know for sure something is wrong. Probably only one unique ordering is allowed.
                // before rule means key before value X|Y
                // Let's use after rules for now
                for(int i = 0; i < list.Count; i++)
                {
                    var afters = list[(i+1)..];
                    if (afterRules.TryGetValue(list[i], out var after))
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
            Console.WriteLine(string.Join(',',list));
            if (!healthy) res += list[list.Count / 2];
        }
        Console.WriteLine(res);
        
    }
    private static bool CheckHealth(int[] updates, Dictionary<int, HashSet<int>> beforeRules) {
        for (int i = 0; i < updates.Length; i++)
        {
            var befores = updates[..i];
            if (beforeRules.TryGetValue(updates[i], out var before))
            {
                if(befores.Intersect(before).Any())
                { 
                    return false; 
                }
            }
        }
        return true;
    }

    private static bool CheckHealth(List<int> updates, Dictionary<int, HashSet<int>> beforeRules) {
        for (int i = 0; i < updates.Count; i++)
        {
            var befores = updates[..i];
            if (beforeRules.TryGetValue(updates[i], out var before))
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

