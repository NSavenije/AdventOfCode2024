#nullable disable

using System;
using System.Text.RegularExpressions;

static class Day3 {
    public static void Solve1() =>
        ParseInput(true);
        
    public static void Solve2() => 
        ParseInput(false);

    private static void ParseInput(bool part1)
    {
        string filePath = "src/Day3/3.in";
        using StreamReader sr = new(filePath);
        string line;

        var pattern = @"(mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\))";
        Regex regex = new(pattern); 
        bool enabled = true;
        int res = 0;

        while ((line = sr.ReadLine()) != null)
        {
            MatchCollection matches = regex.Matches(line);
            foreach(Match match in matches)
            {
                string mulExp = match.Value;
                if (mulExp == "do()")
                    enabled = true;
                else if (mulExp == "don't()")
                    enabled = false;
                else if (enabled || part1) 
                {
                    string mul = mulExp[4..^1];
                    int[] nums = mul.Split(',').Select(int.Parse).ToArray();
                    res += nums[0] * nums[1];
                }
            }
        }
        Console.WriteLine(res);
    }
}