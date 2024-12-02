#nullable disable

static class Day2 {
    public static void Solve1() {
        var levels = ParseInput();
        Console.WriteLine(levels.Count(IsSafe));
    }
        
    public static void Solve2() 
    {
        List<int[]> levels = ParseInput();
        int safeCount = 0;
        foreach (int[] level in levels)
        {
            var subLevels = level.Select((_, i1) => level.Where((_, i2) => i1 != i2).ToArray()).ToList();  
            subLevels.Add(level);
            safeCount = subLevels.Any(IsSafe) ? safeCount + 1 : safeCount;          
        }

        Console.WriteLine(safeCount);
    }

    private static List<int[]> ParseInput()
    {
        string filePath = "src/Day2/2.in";
        var levels = File.ReadAllLines(filePath);
        return levels.Select( l => l.Split().Select(int.Parse).ToArray()).ToList();
    }

    private static bool IsSafe(int[] report)
    {
        bool incrementing = report[0] < report[1]; 
        for (int i = 1; i < report.Length; i++) 
        { 
            if ((incrementing && report[i] <= report[i - 1]) 
                || (!incrementing && report[i] >= report[i - 1]) 
                || (Math.Abs(report[i] - report[i - 1]) > 3))
            { 
                return false; 
            } 
        } 
        
        return true;
    }
}