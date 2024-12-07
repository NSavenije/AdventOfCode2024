#nullable disable

using System.Data;
using System.Text;

static class Day6 {
    static readonly (int y,int x)[] Dirs = [(-1,0),(0,1),(1,0),(0,-1)];
    static (int x,int y) Loc;
    public static void Solve1() {
        char[][] maze = ParseInput();
        var (x, y) = Loc;
        int res = 0;
        int dirIndex = 0;
        while(true)
        {
            // paint & count
            if (maze[y][x] != 'o')
                res++;
            maze[y][x] = 'o';

            // move or escape
            var(dy, dx) = Dirs[dirIndex];
            if (y + dy > -1 && y + dy < maze.Length && x + dx > -1 && x + dx < maze[0].Length)
            {
                if (maze[y + dy][x + dx] == '#')
                {
                    // turn
                    dirIndex = (dirIndex + 1) % 4;
                }
                //Move
                else
                {
                    x += dx; 
                    y += dy;
                }
            }
            //Escape
            else
            {
                break;
            }

            
        }
        Console.WriteLine(CharArrayToString(maze));
        Console.WriteLine(res);
    }

    public static void Solve2()
    {
        char[][] maze = ParseInput();

        int loops = 0;
        for(int m = 0 ; m < maze.Length * maze.Length; m++)
        {
            int my = m / maze.Length;
            int mx = m % maze.Length;
            if(maze[my][mx] != '#' && maze[my][mx] != '^')
            {
                maze[my][mx] = '#';
            }
            else
            {
                continue;
            }

            HashSet<int>[][] mazeDirs = new HashSet<int>[maze.Length][];
            for (int i = 0; i < mazeDirs.Length; i++) 
            { 
                mazeDirs[i] = new HashSet<int>[maze.Length];
                for (int j = 0; j < mazeDirs[i].Length; j++) 
                {
                    mazeDirs[i][j] = []; 
                    if(maze[i][j] != '#' && maze[i][j] != '^')
                        maze[i][j] = '.';
                }
            }
            var (x, y) = Loc;
            int dirIndex = 0;
            // Console.WriteLine(CharArrayToString(maze));

            
            while(true)
            {
                if (maze[y][x] == '.')
                    maze[y][x] = dirIndex % 2 == 0 ? '|' : '-';
                else if (maze[y][x] != '^' && maze[y][x] != 'O')
                    maze[y][x] = '+';
                
                var(dy, dx) = Dirs[dirIndex];


                // move or escape
                if (y + dy > -1 && y + dy < maze.Length && x + dx > -1 && x + dx < maze[0].Length)
                {
                    if (maze[y + dy][x + dx] == '#')
                    {
                        // turn
                        dirIndex = (dirIndex + 1) % 4;
                    }
                    //Move
                    else
                    {
                        x += dx; 
                        y += dy;
                        if (mazeDirs[y][x].Contains(dirIndex))
                        {
                            loops++;
                            break;
                        }
                    }
                }
                //Escape
                else
                {
                    break;
                }
                mazeDirs[y][x].Add(dirIndex);
                // Console.WriteLine(CharArrayToString(maze));
            }
            maze[my][mx] = '.';
        }
        Console.WriteLine(loops);
    }

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

