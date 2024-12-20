#nullable disable

static class Day20
{
    static readonly (int x, int y)[] Dirs = [(0, -1), (1, 0), (0, 1), (-1, 0)];

    public static void Solve1()
    {
        const string filePath = "src/Day20/20.in";
        string[,] maze = ParseMaze(File.ReadAllLines(filePath), out var nodes);
        // Print(maze);
        maze = LabelMaze(maze, nodes.start, nodes.end);
        List<int> cheats = [];
        for(int x = 1; x < maze.GetLength(0) - 1; x++)
        {
            for(int y = 1; y < maze.GetLength(1) - 1; y++)
            {
                if(maze[x,y] != "#")
                    cheats.AddRange(Cheat(maze,x,y,2));
            }
        }
        Console.WriteLine(cheats.Where(x => x >= 100).Count());
    }

    public static void Solve2()
    {
        const string filePath = "src/Day20/20.in";
        string[,] maze = ParseMaze(File.ReadAllLines(filePath), out var nodes);
        // Print(maze);
        maze = LabelMaze(maze, nodes.start, nodes.end);
        List<int> cheats = [];
        for(int x = 1; x < maze.GetLength(0) - 1; x++)
        {
            for(int y = 1; y < maze.GetLength(1) - 1; y++)
            {
                if(maze[x,y] != "#")
                    cheats.AddRange(Cheat(maze,x,y,20));
            }
        }
        Console.WriteLine(cheats.Where(x => x >= 100).Count());
    }

    static List<int> Cheat(string[,] maze, int x, int y, int d)
    {
        List<int> cheats = [];
        Dictionary<(int,int),int> reachableTiles = GetReachableSquares(x,y,d,maze.GetLength(0));
        foreach(var (nx,ny) in reachableTiles.Keys)
        {
            if (maze[nx, ny] == "#")
                continue;
            int from = int.Parse(maze[x, y]);
            int to = int.Parse(maze[nx, ny]);
            int dist = reachableTiles[(nx,ny)];
            if (from + dist < to)
                cheats.Add(to - from - dist);
        }
        return cheats;
    }

    public static Dictionary<(int, int),int> GetReachableSquares(int x, int y, int n, int size)
    {
        Dictionary<(int, int),int> reachableSquares = [];

        for (int dx = -n; dx <= n; dx++)
        {
            for (int dy = -n; dy <= n; dy++)
            {
                int newX = x + dx;
                int newY = y + dy;
                int dist = Math.Abs(dx) + Math.Abs(dy);
                if (dist <= n && newX >= 0 && newX < size && newY >= 0 && newY < size)
                {
                    reachableSquares.Add((newX, newY),dist);
                }
            }
        }

        return reachableSquares;
    }

    static string[,] LabelMaze(string[,] maze, (int x, int y) start, (int x,int y) end)
    {
        Queue<(int,int)> q = [];
        q.Enqueue(start);
        maze[start.x,start.y] = "0";
        while(q.Count != 0)
        {
            var (x,y) = q.Dequeue();
            for(int i = 0; i < 4; i++)
            {
                var (dx,dy) = Dirs[i];
                if (maze[x+dx,y+dy] == "." || maze[x+dx,y+dy] == "E")
                {
                    maze[x+dx,y+dy] = (int.Parse(maze[x,y]) + 1).ToString();
                    q.Enqueue((x+dx,y+dy));
                }
            }
        }
        return maze;
    }

    static string[,] ParseMaze(string[] lines, out ((int x, int y) start, (int x, int y) end) nodes)
    {
        (int x, int y) start = (-1, -1);
        (int x, int y) end = (-1, -1);
        var maze = new string[lines[0].Length, lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                maze[j, i] = lines[i][j].ToString();
                if (lines[i][j] == 'S') start = (j, i);
                if (lines[i][j] == 'E') end = (j, i);
            }
        }
        nodes = (start, end);
        return maze;
    }

    static void Print(string[,] maze)
    {
        for (int x = 0; x < maze.GetLength(1); x++)
        {
            for (int y = 0; y < maze.GetLength(0); y++)
            {
                Console.Write(maze[y, x]);
            }
            Console.WriteLine();
        }
    }
}
