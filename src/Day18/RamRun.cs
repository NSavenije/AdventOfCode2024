#nullable disable

static class Day18
{
    public static void Solve1()
    {
        string filePath = "src/Day18/18.in";
        string[] lines = File.ReadAllLines(filePath);
        int size = 73;
        char[,] maze = PrefillMaze(size);
        for (int i = 0; i < 1024; i++)
        {
            int[] coords = lines[i].Split(',').Select(int.Parse).ToArray();
            int x = coords[0] + 1; // because of the added boundary
            int y = coords[1] + 1; 
            maze[x,y] = '#';
        }

        // Print(maze);

        var distances = BFS(maze);
        var distance = distances[(size - 2, size - 2)];
        Console.WriteLine(distance);
    }

    public static void Solve2()
    {
    }

    static Dictionary<(int,int),int> BFS(char[,] maze, bool terminateOnExit = false)
    {
        (int x,int y)[] dirs = [(0,-1),(1,0),(0,1),(-1,0)];
        Dictionary<(int, int), int> distances = [];
        Queue<(int x,int y, int d)> q = [];
        q.Enqueue((1, 1, 1));
        while (q.Count != 0)
        {
            (int x, int y, int d) = q.Dequeue();
            for(int i = 0; i < 4; i++)
            {
                int dx = dirs[i].x;
                int dy = dirs[i].y;
                if (maze[x + dx, y + dy] == '#')
                    continue;
                
                if (!distances.TryGetValue((x+dx,y+dy), out var minDist) || d < minDist)
                {
                    distances[(x+dx,y+dy)] = d;
                    maze[x+dx,y+dy] = 'o';
                    q.Enqueue((x + dx, y + dy, d + 1));
                }
            }
        }
        return distances;
    }

    static void Print(char[,] maze)
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

    static char[,] PrefillMaze(int size)
    {
        char[,] maze = new char[size,size];
        for(int x = 0; x < size; x++)
        {
            for(int y = 0; y < size; y++)
            {
                if (x == 0 || y == 0 || x == size - 1 || y == size - 1)
                    maze[x,y] = '#';
                else
                    maze[x,y] = '.';
            }
        }
        return maze;
    }
}

