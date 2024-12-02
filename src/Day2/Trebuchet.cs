#nullable disable

static class Day2 {
    public static void Solve1() {
        ParseInput(true);
    }
        
    public static void Solve2() {
        string filePath = "src/Day2/2.in";
        using StreamReader sr = new(filePath);
        string line;
        int res = 0;
        
        while ((line = sr.ReadLine()) != null)
        {
            List<int> ls = [];
            var parts = line.Split();
            for (int i = 0; i < parts.Length; i++)
            {
                ls.Add(int.Parse(parts[i]));
            }
            List<List<int>> sublists = ls
            .Select((item, index) => ls.Where((_, i) => i != index).ToList())
            .ToList();
            sublists.Add(ls);
            
            int safeCounter = 0;
            foreach(var r in sublists)
            {
                bool inc;
                bool safe = true;
                if (r[0] > r[1])
                {
                    inc = false;
                } 
                else if (r[0] < r[1])
                {
                    inc = true;
                }
                else {
                    continue;
                }
                for(int l = 0; l < r.Count - 1; l++)
                {
                    if(inc && r[l] >= r[l + 1])
                    {
                        safe = false;
                    }
                    if(!inc && r[l] <= r[l + 1])
                    {
                        safe = false;
                    }
                    if(Math.Abs(r[l] - r[l + 1]) > 3)
                        safe = false;
                }
                if (safe == true)
                    safeCounter++;
            }
            if (safeCounter > 0)
                res++;
        }

        Console.WriteLine(res);
    }

    static void ParseInput(bool part1)
    {
        string filePath = "src/Day2/2.in";
        using StreamReader sr = new(filePath);
        string line;
        int res = 0;
        int nr = 0;
        
        while ((line = sr.ReadLine()) != null)
        {
            var r = line.Split();
            nr++;
            bool inc;
            bool safe = true;
            if (int.Parse(r[0]) > int.Parse(r[1]))
            {
                inc = false;
            } 
            else if (int.Parse(r[0]) < int.Parse(r[1]))
            {
                inc = true;
            }
            else {
                continue;
            }
            for(int l = 0; l < r.Length - 1; l++)
            {
                if(inc && int.Parse(r[l]) >= int.Parse(r[l + 1]))
                {
                    safe = false;
                }
                if(!inc && int.Parse(r[l]) <= int.Parse(r[l + 1]))
                {
                    safe = false;
                }
                if(Math.Abs(int.Parse(r[l]) - int.Parse(r[l + 1])) > 3)
                    safe = false;
            }
            if (safe == true)
            {
                res++;
                // Console.WriteLine((nr - 1) + "is safe");
            }
            
        }

        Console.WriteLine(res);
    }
}