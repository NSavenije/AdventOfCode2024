#nullable disable

public static class Day22
{
    public static void Solve1()
    {
        string filePath = "C:/Users/nouds/Repos/AdventOfCode2024/src/Day22/22.in";
        long[] numbers = File.ReadAllLines(filePath).Select(long.Parse).ToArray();

        for(int i = 0; i < numbers.Length; i++)
        {
            for(int c = 0; c < 2000; c++)
            {
                numbers[i] = GenerateNextSecret(numbers[i]);
            }
        }
        Console.WriteLine(numbers.Sum());   
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