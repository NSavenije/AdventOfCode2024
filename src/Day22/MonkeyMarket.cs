#nullable disable

using System.Diagnostics;

public static class Day22
{
    public static void Solve1()
    {
        string filePath = "C:/Users/nouds/Repos/AdventOfCode2024/src/Day22/22.in";
        long[] numbers = File.ReadAllLines(filePath).Select(long.Parse).ToArray();
        Dictionary<(int,int,int,int),int> sequenceSums = [];
        
        for(int i = 0; i < numbers.Length; i++)
        {
            List<(int price, int change)> ps = [];
            ps.Add(((int)(numbers[i] % 10), int.MinValue));
            for(int c = 0; c < 2000; c++)
            {
                numbers[i] = GenerateNextSecret(numbers[i]);
                int p = (int)(numbers[i] % 10);
                int dp = p - ps[c].price;
                ps.Add((p, dp));
            }
            HashSet<(int,int,int,int)> seqs = [];
            for(int c = 4; c < 2001; c++)
            {
                var key = (ps[c-3].change, ps[c-2].change, ps[c-1].change, ps[c].change);
                if (seqs.Add(key))
                {
                    if (sequenceSums.TryGetValue(key, out var p))
                        sequenceSums[key] += ps[c].price;
                    else
                        sequenceSums[key] = ps[c].price;
                }
            }
        }
        Console.WriteLine(numbers.Sum());  
        Console.WriteLine(sequenceSums.Values.Max()); 
    }

    static long GenerateNextSecret(long num)
    {
        const long mask = (1 << 24) - 1;
        
        num = ((num << 6) ^ num) & mask;
        num = ((num >> 5) ^ num) & mask;
        num = ((num << 11) ^ num) & mask;
        
        return num;
    }
}