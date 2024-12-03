#nullable disable

using System;
using System.Text.RegularExpressions;

static class Day3 {
    public static void Solve1() {
        ParseInput();
    }
        
    public static void Solve2() 
    {
        
    }

    private static void ParseInput()
    {
        string filePath = "src/Day3/3.in";
        using StreamReader sr = new(filePath);
        string line;
        List<string> muls = [];

        // var pattern = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don\'t\(\)";
        var pattern = @"(mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\))";

        
        Regex regex = new(pattern); 
        
        int res = 0;

        while ((line = sr.ReadLine()) != null)
        {
            MatchCollection matches = regex.Matches(line);
            foreach(Match match in matches)
            {
                muls.Add(match.Value);
            }
            
        }

        bool enabled = true;
        foreach(string mulExp in muls)
        {
            Console.WriteLine(mulExp);
            
            if (mulExp == "do()")
                enabled = true;
            else if (mulExp == "don't()")
                enabled = false;
            else if (enabled) {
            string mul = mulExp[4..^1];
            // Console.WriteLine(mul + " " + muls.Count);

            int[] nums = mul.Split(',').Select(int.Parse).ToArray();
            res += nums[0] * nums[1];
            }
        }
        Console.WriteLine(res);
    }
}