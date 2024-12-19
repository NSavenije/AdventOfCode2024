#nullable disable

static class Day19
{
    static HashSet<string> Towels;

    public static void Solve1()
    {
        string filePath = "C:/Users/nouds/Repos/AdventOfCode2024/src/Day19/19.in";
        string[] lines = File.ReadAllLines(filePath);

        Towels = lines[0].Split(", ").ToHashSet();
        List<string> reachable = [];
        for(int i = 2; i < lines.Length; i++)
        {
            if (IsPatternPossible(lines[i], 0)) 
                reachable.Add(lines[i]);
        }
        Console.WriteLine(reachable.Count);
    }

    static bool IsPatternPossible(string pattern, int cursor)
    {
        string remainder = pattern[cursor ..];
        if (Towels.Contains(remainder))
            return true;

        return Towels.Any(towel => remainder.StartsWith(towel) && IsPatternPossible(pattern, cursor + towel.Length));
    }

    public static void Solve2()
    {
    }
}
