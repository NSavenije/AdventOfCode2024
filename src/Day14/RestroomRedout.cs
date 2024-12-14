#nullable disable

using System.Text.RegularExpressions;

static class Day14
{
    public static void Solve1() => Solve(true);

    public static void Solve2() => Solve(false);

    static void Solve(bool part1)
    {
        var robots = ParseInput("src/Day14/14.in", part1);
        
        for(int i = 0; true; i++)
        {
            int tl = 0;
            int tr = 0;
            int bl = 0;
            int br = 0;

            int[] xCounts = new int[101];

            for(int r = 0; r < robots.Count; r++)
            {
                (int x,int y, int dx, int dy) = robots[r];
                x = Mod(x + dx, 101);
                y = Mod(y + dy, 103);
                robots[r] = (x,y,dx,dy);
                xCounts[x]++;
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
            if (part1 && i == 99)
            {
                Console.WriteLine(score);
                break;
            }
            int x1 = 0;
            for(int r = 0; r < 101; r++)
            {
                if(xCounts[r] >= 30)
                {
                    int[] ys = new int[103];

                    foreach(var rob in robots)
                    {
                        if(rob.x == r)
                            ys[rob.y]++;
                    }
                    int streak = 0;
                    int longestStreak = 0;
                    for(int yy = 0; yy < 103; yy++)
                    {
                        if (ys[yy] > 0)
                        {
                            streak++;
                            if (streak > longestStreak)
                                longestStreak = streak;
                        }
                        else
                        {
                            streak = 0;
                        }
                    }
                    if (longestStreak >= 30)
                    {
                        x1 = r;
                        break;
                    }
                }
            }
            if (x1 == 0)
                continue;
            for(int r = x1; r < 103; r++)
            {
                if(xCounts[r] >= 30)
                {
                    int[] ys = new int[103];

                    foreach(var rob in robots)
                    {
                        if(rob.x == r)
                            ys[rob.y]++;
                    }
                    int streak = 0;
                    int longestStreak = 0;
                    for(int yy = 0; yy < 103; yy++)
                    {
                        if (ys[yy] > 0)
                        {
                            streak++;
                            if (streak > longestStreak)
                                longestStreak = streak;
                        }
                        else
                        {
                            streak = 0;
                        }
                    }
                    if (longestStreak >= 30)
                    {
                        Console.WriteLine(i + 1);
                        // ToString(robots);
                        return;
                    }
                }
            }
        }
    }

    static int Mod(int x, int m) => (x % m + m) % m;

    static void ToString(List<(int x, int y, int, int)> rs)
    {
        int rows = 101;
        int columns = 103;
        char[,] grid = new char[rows, columns];
        // Initialize the grid with '.' 
        for (int i = 0; i < rows; i++) 
        { 
            for (int j = 0; j < columns; j++) 
            { 
                grid[i, j] = '.';
            } 
        } 
        // List of coordinates (x, y) to replace '.' with '1' 
        // Replace specified coordinates with '1' 
        foreach (var (x, y, _, _) in rs) 
        { 
            if (grid[x,y] == '.') 
            { 
                grid[x, y] = '1';
            }
            else
            {
                int c = (int)grid[x,y] - 48;
                c++;
                grid[x,y] = char.Parse(c.ToString());
            } 
        } 
        // Print the grid 
        for (int i = 0; i < rows; i++) 
        { 
            for (int j = 0; j < columns; j++) 
            { 
                Console.Write(grid[i, j]); 
            } 
            Console.WriteLine(); 
        }
    }

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