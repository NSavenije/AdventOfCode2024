#nullable disable

static class Day19
{
    static HashSet<string> Towels;
    static Dictionary<string, long> memo = [];

    public static void Solve1() => Solve(false);

    public static void Solve2() => Solve(true);

    static void Solve(bool count)
    {
        string filePath = "src/Day19/19.in";
        string[] lines = File.ReadAllLines(filePath);

        Towels = lines[0].Split(", ").ToHashSet();
        long total = 0;
        for(int i = 2; i < lines.Length; i++)
        {
            if (count)
                total += CountAllPatterns(lines[i], 0);
            else
                total += IsPatternPossible(lines[i], 0) ? 1 : 0;
        }
        Console.WriteLine(total);
    }

    static bool IsPatternPossible(string pattern, int cursor)
    {
        string remainder = pattern[cursor ..];
        if (Towels.Contains(remainder))
            return true;

        return Towels.Any(towel => remainder.StartsWith(towel) && IsPatternPossible(pattern, cursor + towel.Length));
    }

    static long CountAllPatterns(string pattern, int cursor)
    {
        string remainder = pattern[cursor ..];
        if (memo.TryGetValue(remainder, out long rem))
            return rem;
        
        long count = Towels.Contains(remainder) ? 1 : 0;
        count += Towels.Where(remainder.StartsWith).Sum(towel => CountAllPatterns(pattern, cursor + towel.Length));

        memo[remainder] = count;
        return count;
    }
}
