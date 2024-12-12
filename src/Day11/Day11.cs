#nullable disable

static class Day11
{
    public static void Solve1() => Solve(25);

    public static void Solve2() => Solve(75);

    static void Solve(int its)
    {
        Dictionary<(long,long),long> cache = [];
        long stones = File.ReadAllLines("src/Day11/11.in")[0].Split(' ')
                            .Select(long.Parse)
                            .Select(x => Count(x, its))
                            .Sum();
        Console.WriteLine(stones);
        
        long Count(long stone, int d) 
        { 
            if (d == 0) 
                return 1;

            if (stone == 0) 
                return Count(1, d - 1); 

            if (cache.ContainsKey((stone, d))) 
                return cache[(stone, d)]; 
                
            int numLen = stone.ToString().Length;
            if (numLen % 2 == 0) 
            { 
                long left  = Count((long)(stone / Math.Pow(10, numLen / 2)), d - 1);
                long right = Count((long)(stone % Math.Pow(10, numLen / 2)), d - 1);
                long result = left + right;
                cache[(stone, d)] = result; 
                return result; 
            } 
            long res = Count(stone * 2024, d - 1); 
            cache[(stone, d)] = res; 
            return res;
        }
    }
}