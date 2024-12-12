#nullable disable

using System.Text;

static class Day6 {
    static readonly (int y,int x)[] Dirs = [(-1,0),(0,1),(1,0),(0,-1)];
    static (int x,int y) Loc;
    public static void Solve1() {
        char[][] maze = ParseInput();
        FindPath(maze, out var path);
        HashSet<(int,int)> res = path.Select(p => (p.y,p.x)).ToHashSet();
        Console.WriteLine(res.Count);
    }

    public static void Solve2()
    {
        char[][] maze = ParseInput();
        FindPath(maze, out var path);
        HashSet<(int,int)> res = path.Select(p => (p.y,p.x)).ToHashSet();
        int timeLoops = 0;
        foreach(var (y, x) in res)
        {
            if (maze[y][x] != '.') continue;

            maze[y][x] = '#';
            if (!FindPath(maze, out var _))
                timeLoops++;

            maze[y][x] = '.';
        }


        Console.WriteLine(timeLoops);
    }

    private static bool FindPath(char[][] maze, out HashSet<(int y, int x, int d)> path)
    {
        path = [];
        var (x, y) = Loc;
        int dirIndex = 0;
        path.Add((y, x, dirIndex));

        while (true)
        {
            var (dy, dx) = Dirs[dirIndex];

            if (!IsWithinBounds(maze, y + dy, x + dx))
                break;
            if (maze[y + dy][x + dx] == '#')
            {
                dirIndex = (dirIndex + 1) % 4;
            }
            else
            {
                x += dx;
                y += dy;
                if (path.Contains((y, x, dirIndex)))
                {
                    return false;
                }
                path.Add((y, x, dirIndex));
            }
        }

        return true;
    }

    private static bool IsWithinBounds(char[][] maze, int y, int x) =>
        y >= 0 && y < maze.Length && x >= 0 && x < maze[0].Length;

    private static char[][] ParseInput()
    {
        string fp = "src/Day6/6.in";
        
        List<string> lines = File.ReadAllLines(fp).ToList();
        char[][] maze = new char[lines.Count][];

        for (int i = 0; i < lines.Count; i++) 
        { 
            maze[i] = lines[i].ToCharArray(); 
            if (lines[i].Contains('^'))
                Loc = (lines[i].IndexOf('^'), i);
        }
        return maze;
    }

    static string CharArrayToString(char[][] charArray) 
    { 
        StringBuilder sb = new();
        foreach (char[] row in charArray) 
        { 
            sb.AppendLine(new string(row));
        } 
        return sb.ToString();
    }
}

