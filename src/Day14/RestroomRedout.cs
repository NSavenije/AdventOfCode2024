#nullable disable

using System.Text.RegularExpressions;

static class Day14
{
    public static void Solve1() => Solve(true);

    public static void Solve2() => Solve(false);

    static void Solve(bool part1)
    {
        var robots = ParseInput("C:\\Users\\nouds\\Repos\\AdventOfCode2024\\src\\Day14\\14.in", part1);
        int tl = 0;
        int tr = 0;
        int bl = 0;
        int br = 0;
        for(int r = 0; r < robots.Count; r++)
        {
            (int x,int y, int dx, int dy) = robots[r];
            x = Mod(x + 100 * dx, 101);
            y = Mod(y + 100 * dy, 103);
            if (x < 50)
            {
                if (y < 51)
                    tl++;
                else if(y > 51)
                    tr++;
            }
            else if (x > 50)
            {
                if (y < 51)
                    bl++;
                else if(y > 51)
                    br++;
            }
        }
        
        int score = tl * tr * bl * br;
        Console.WriteLine(score);
    }

    static int Mod(int x, int m) => (x % m + m) % m;

    static List<(int x,int y, int dx, int dy)> ParseInput(string filePath, bool part1) 
    { 
        var lines = File.ReadAllLines(filePath); 
        string pattern = @"-?\d+"; 
        List<(int x,int y, int dx, int dy)> robots = [];
        foreach(string l in lines)
        {
            MatchCollection ms = Regex.Matches(l, pattern); 
            robots.Add((int.Parse(ms[0].Value), 
                        int.Parse(ms[1].Value), 
                        int.Parse(ms[2].Value), 
                        int.Parse(ms[3].Value))
                    );
        }
        return robots;
    }
}