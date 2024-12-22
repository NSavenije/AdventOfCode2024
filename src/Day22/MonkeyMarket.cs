#nullable disable

public static class Day22
{
    public static void Solve1()
    {
        string filePath = "src/Day22/22.in";
        long[] numbers = File.ReadAllLines(filePath).Select(long.Parse).ToArray();

        for(int i = 0; i < numbers.Length; i++)
            for(int c = 0; c < 2000; c++)
                numbers[i] = GenerateNextSecret(numbers[i]);

        Console.WriteLine(numbers.Sum());  
    }

    public static void Solve2()
    {
        string filePath = "src/Day22/22.in";
        long[] numbers = File.ReadAllLines(filePath).Select(long.Parse).ToArray();
        Dictionary<(int,int,int,int),int> sequenceSums = [];
        
        for(int i = 0; i < numbers.Length; i++)
        {
            List<(int price, int change)> ps = [((int)(numbers[i] % 10), int.MinValue)];
            HashSet<(int,int,int,int)> seqs = [];
            for(int c = 0; c < 2000; c++)
            {
                numbers[i] = GenerateNextSecret(numbers[i]);
                int p = (int)(numbers[i] % 10);
                ps.Add((p, p - ps[c].price));
                if (c < 3) continue;

                var key = (ps[c-3].change, ps[c-2].change, ps[c-1].change, ps[c].change);
                if (seqs.Add(key))
                    sequenceSums[key] = sequenceSums.TryGetValue(key, out var val) ? val + ps[c].price : ps[c].price;
            }
        }
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