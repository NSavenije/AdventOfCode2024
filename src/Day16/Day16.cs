#nullable disable

static class Day16
{
    static readonly (int x, int y)[] Dirs = [(0, -1), (1, 0), (0, 1), (-1, 0)];
    
    public static void Solve1()
    {
        const string filePath = "src/Day16/16.in";
        char[,] maze = ParseMaze(File.ReadAllLines(filePath), out var nodes);
        const int dirIndex = 1;
        int score = SolveMazePaths(nodes, dirIndex, maze).Min(p => p.cost);
        Console.WriteLine(score);
    }

    public static void Solve2()
    {
        const string filePath = "src/Day16/16.in";
        char[,] maze = ParseMaze(File.ReadAllLines(filePath), out var nodes);
        const int dirIndex = 1;
        var paths = SolveMazePaths(nodes, dirIndex, maze);
        int minCost = paths.Min(p => p.cost);
        paths = paths.Where(p => p.cost == minCost).ToList();
        var pathTiles = new HashSet<(int, int)>();
        
        foreach (var (path, _) in paths)
        {
            foreach (var (x, y) in path)
            {
                maze[x, y] = 'o';
                pathTiles.Add((x, y));
            }
        }
        // Print(maze);
        Console.WriteLine(pathTiles.Count);
    }

    static List<(List<(int x, int y)> path, int cost)> SolveMazePaths(((int x, int y) start, (int x, int y) end) nodes, int d, char[,] maze)
    {
        var visitedTiles = new Dictionary<(int x, int y, int d), int>();
        var queue = new Queue<(int x, int y, int d, int cost, List<(int x, int y)> path)>();

        int x = nodes.start.x;
        int y = nodes.start.y;
        var startPath = new List<(int x, int y)> { (x, y) };
        queue.Enqueue((x, y, d, 0, startPath));
        visitedTiles[(x, y, d)] = 0;

        var allShortestPaths = new List<(List<(int x, int y)> path, int cost)>();

        while (queue.Count > 0)
        {
            var (cx, cy, cd, cc, cpath) = queue.Dequeue();
            var (dx, dy) = Dirs[cd];

            // Move forward
            ProcessMove(cx + dx, cy + dy, cd, cc + 1, maze, queue, visitedTiles, cpath, allShortestPaths, nodes.end);

            // Turn right
            ProcessMove(cx, cy, (cd + 1) % 4, cc + 1000, maze, queue, visitedTiles, cpath, allShortestPaths, nodes.end);

            // Turn left
            ProcessMove(cx, cy, (cd + 3) % 4, cc + 1000, maze, queue, visitedTiles, cpath, allShortestPaths, nodes.end);
        }
        return allShortestPaths;
    }

    static void ProcessMove(int x, int y, int d, int cost, char[,] maze, 
                Queue<(int x, int y, int d, int cost, List<(int x, int y)> path)> queue, 
                Dictionary<(int x, int y, int d), int> visitedTiles, 
                List<(int x, int y)> path, 
                List<(List<(int x, int y)> path, int cost)> allShortestPaths, 
                (int x, int y) end)
    {
        if (maze[x, y] != '#' && (!visitedTiles.TryGetValue((x, y, d), out int minCost) || cost <= minCost))
        {
            visitedTiles[(x, y, d)] = cost;
            var newPath = new List<(int x, int y)>(path) { (x, y) };
            queue.Enqueue((x, y, d, cost, newPath));

            if (x == end.x && y == end.y)
            {
                allShortestPaths.Add((newPath, cost));
            }
        }
    }

    static char[,] ParseMaze(string[] lines, out ((int x, int y) start, (int x, int y) end) nodes)
    {
        (int x, int y) start = (-1, -1);
        (int x, int y) end = (-1, -1);
        var maze = new char[lines[0].Length, lines.Length];
        
        for (int i = 0; i < lines.Length; i++)
        {
            for (int j = 0; j < lines[0].Length; j++)
            {
                maze[j, i] = lines[i][j];
                if (lines[i][j] == 'S') start = (j, i);
                if (lines[i][j] == 'E') end = (j, i);
            }
        }
        nodes = (start, end);
        return maze;
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
}
