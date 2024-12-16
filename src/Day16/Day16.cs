#nullable disable

static class Day16
{
    static readonly (int x,int y)[] Dirs = [(0,-1),(1,0),(0,1),(-1,0)];
    public static void Solve1()
    {
        string filePath = "src/Day16/16.in";
        char[,] maze = ParseMaze(File.ReadAllLines(filePath), out var nodes);
        int dirIndex = 1;
        int score = SolveMaze(nodes, dirIndex, maze);
        Console.WriteLine(score);
    }

    public static void Solve2()
    {
        string filePath = "src/Day16/16c.in";
        char[,] maze = ParseMaze(File.ReadAllLines(filePath), out var nodes);
        int dirIndex = 1;
        var path = SolveMazePaths(nodes, dirIndex, maze);
        foreach(var (x,y) in path)
        {
            maze[x,y] = 'o';
        }
        Print(maze);
    }

    static List<(int x, int y)> SolveMazePaths(((int x, int y) start, (int x, int y) end) nodes, int d, char[,] maze)
    {
        var visitedTiles = new Dictionary<(int x, int y, int d), int>();
        var q = new Queue<(int x, int y, int d, int cost)>();
        var predecessors = new Dictionary<(int x, int y, int d), (int x, int y, int d)?>();

        int x = nodes.start.x;
        int y = nodes.start.y;
        q.Enqueue((x, y, d, 0));
        visitedTiles[(x, y, d)] = 0;
        predecessors[(x, y, d)] = null;

        while (q.Count != 0)
        {
            (x, y, d, int cost) = q.Dequeue();
            var (dx, dy) = Dirs[d];

            // Move forward
            ProcessMove(x + dx, y + dy, d, cost + 1, maze, q, visitedTiles, predecessors, (x, y, d));

            // Turn right
            ProcessMove(x, y, (d + 1) % 4, cost + 1000, maze, q, visitedTiles, predecessors, (x, y, d));

            // Turn left
            ProcessMove(x, y, (d + 3) % 4, cost + 1000, maze, q, visitedTiles, predecessors, (x, y, d));
        }

        return GetPath(nodes.end, visitedTiles, predecessors);
    }

    static void ProcessMove(int x, int y, int d, int cost, char[,] maze, Queue<(int x, int y, int d, int cost)> q, Dictionary<(int x, int y, int d), int> visitedTiles, Dictionary<(int x, int y, int d), (int x, int y, int d)?> predecessors, (int x, int y, int d) parent)
    {
        if (maze[x, y] != '#' && (!visitedTiles.TryGetValue((x, y, d), out int minCost) || cost < minCost))
        {
            visitedTiles[(x, y, d)] = cost;
            q.Enqueue((x, y, d, cost));
            predecessors[(x, y, d)] = parent;
        }
    }

    static List<(int x, int y)> GetPath((int x, int y) end, Dictionary<(int x, int y, int d), int> visitedTiles, Dictionary<(int x, int y, int d), (int x, int y, int d)?> predecessors)
    {
        int minCost = int.MaxValue;
        (int x, int y, int d)? endNode = null;

        for (int dir = 0; dir < 4; dir++)
        {
            if (visitedTiles.TryGetValue((end.x, end.y, dir), out int mc) && mc < minCost)
            {
                minCost = mc;
                endNode = (end.x, end.y, dir);
            }
        }

        var path = new List<(int x, int y)>();
        for (var node = endNode; node.HasValue; node = predecessors[node.Value])
        {
            path.Add((node.Value.x, node.Value.y));
        }
        path.Reverse();
        return path;
    }


    static int SolveMaze(((int x,int y) start,(int x,int y) end) nodes, int d, char[,] maze)
    {
        int cost = 0;
        Dictionary<(int x ,int y, int d), int> visitedTiles = [];
        Queue<(int,int,int,int)> q = [];
        int x = nodes.start.x;
        int y = nodes.start.y;
        q.Enqueue((x,y,d,cost));
        visitedTiles.Add((x,y,d),cost);
        while(q.Count != 0)
        {
            (x, y, d, cost) = q.Dequeue();
            var (dx, dy) = Dirs[d];

            // Move forward
            int tx = x + dx; int ty = y + dy;
            if (maze[tx, ty] != '#')
            {
                
                if (visitedTiles.TryGetValue((tx, ty, d), out int minCostFwd))
                {
                    if (cost + 1 < minCostFwd)
                    {
                        visitedTiles[(tx, ty, d)] = cost + 1;
                        q.Enqueue((tx, ty, d, cost + 1));

                    }
                }
                else
                {
                    visitedTiles[(tx, ty, d)] = cost + 1;
                    q.Enqueue((tx, ty, d, cost + 1));
                }
            }

            // Turn right
            int td = (d + 1) % 4;
            if (visitedTiles.TryGetValue((x, y, td), out int minCostRight))
            {
                if (cost + 1000 < minCostRight)
                {
                    visitedTiles[(x, y, td)] = cost + 1000;
                    q.Enqueue((x, y, td, cost + 1000));

                }
            }
            else
            {
                visitedTiles[(x, y, td)] = cost + 1000;
                q.Enqueue((x, y, td, cost + 1000));
            }

            // Turn left
            td = (d + 3) % 4;
            if (visitedTiles.TryGetValue((x, y, td), out int minCostLeft))
            {
                if (cost + 1000 < minCostLeft)
                {
                    visitedTiles[(x, y, td)] = cost + 1000;
                    q.Enqueue((x, y, td, cost + 1000));

                }
            }
            else
            {
                visitedTiles[(x, y, td)] = cost + 1000;
                q.Enqueue((x, y, td, cost + 1000));
            }

        }
        int minCost = int.MaxValue;
        for(int dir = 0; dir < 4; dir++)
        {
            if (visitedTiles.TryGetValue((nodes.end.x, nodes.end.y, dir), out int mc))
            {
                minCost = Math.Min(mc, minCost);
            }
        }
        return minCost;
    }

    static char[,] ParseMaze(string[] lines, out ((int x,int y) start,(int x,int y) end) nodes)
    {
        (int,int) init = (-1,-1);
        (int,int) end = (-1,-1);
        char[,] maze = new char[lines[0].Length,lines.Length];
        for(int i = 0; i < lines.Length; i++)
        {
            for(int j = 0; j < lines[0].Length; j++)
            {
                maze[j,i] = lines[i][j]; 
                if (lines[i][j] == 'S')
                    init = (j,i);
                if (lines[i][j] == 'E')
                    end = (j,i);
            }
        }
        nodes = (init,end);
        return maze;
    }

    static void Print(char[,] warehouse)
    {
        for(int x = 0; x < warehouse.GetLength(1); x++)
        {
            for(int y = 0; y < warehouse.GetLength(0); y++)
            {
                Console.Write(warehouse[y, x]);
            }
            Console.WriteLine();
        }
    }
}