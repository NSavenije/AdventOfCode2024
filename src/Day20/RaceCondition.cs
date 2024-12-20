public static class Day20
{
    public static void Solve1()
    {
        var lines = File.ReadAllLines("src/Day20/20b.in").ToArray();
        Tree mazeTree = MazeParser.ParseMaze(lines);
        // Console.WriteLine(MazeTraversal.DFS(mazeTree.Root));
        MazeTraversal mazeTraversal = new();
        Console.WriteLine(mazeTraversal.DFS(mazeTree));
    }



    class Node((int,int) pos, int cols)
    {
        public Dictionary<(int,int),int> DistanceFromNode { get; set; } = [];
        public int DistanceFromRoot { get; set; }
        public int Neigbours;
        public Dictionary<Node, int> AdjacentNodes { get; } = [];

        public (int r,int c) Pos {get; set;} = pos;

        public List<Node> Path = [];
        public int Cols = cols;

        public void AddChild(Node node, int distance)
        {
            AdjacentNodes.Add(node, distance);
        }
    }

    class Tree(Node node, (int,int) end)
    {
        public Node Root = node;

        public (int i,int j) End = end;
    }

    class MazeParser
    {
        public static Tree ParseMaze(string[] mazeRows)
        {
            int rows = mazeRows.Length;
            int cols = mazeRows[0].Length;

            Dictionary<(int,int), Node> nodes = [];

            // Check if a position is a junction (has more than 2 accessible paths)
            int CountNeighbours(int x, int y)
            {
                int count = 0;
                if (x > 0 && mazeRows[x - 1][y] != '#') count++;
                if (x < rows - 1 && mazeRows[x + 1][y] != '#') count++;
                if (y > 0 && mazeRows[x][y - 1] != '#') count++;
                if (y < cols - 1 && mazeRows[x][y + 1] != '#') count++;
                return count;
            }

            (int,int) start = (-1,-1);
            (int,int) end = (-1,-1);
            // Create nodes for each traversable position
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int neighbours = CountNeighbours(i,j);
                    if ((mazeRows[i][j] != '#' && neighbours != 2) || mazeRows[i][j] == 'S' || mazeRows[i][j] == 'E')
                    {
                        nodes.Add((i,j), new Node((i, j), cols)
                        {
                            Neigbours = neighbours
                        });
                    }
                    if (mazeRows[i][j] == 'S')
                    {
                        start = (i,j);
                    }
                    if (mazeRows[i][j] == 'E')
                    {
                        end = (i,j);
                    }
                }
            }

            foreach(var junction in nodes)
            {
                HashSet<(int,int)> visitedNodes = [];
                FloodFill(junction.Key,  junction, 0, visitedNodes);
                Console.WriteLine($"{junction.Key}: {string.Join(',',junction.Value.AdjacentNodes.Select(n => $"{n.Key.Pos}[{n.Value}]"))}");
            }

            void FloodFill((int i, int j) pos, KeyValuePair<(int,int),Node> junction, int depth, HashSet<(int,int)> visitedNodes)
            {
                // Console.WriteLine(pos);
                int i = pos.i;
                int j = pos.j;
                if (!visitedNodes.Add((i,j)))
                {
                    // Console.WriteLine("stopped: backtracking");
                    return;
                }

                if (pos != junction.Value.Pos && nodes.TryGetValue((i,j), out var node))
                {
                    // If I found another junction. STOP looking further
                    node.DistanceFromNode[junction.Key] = depth;
                    junction.Value.AddChild(node, depth);
                    // Console.WriteLine("stopped: node found");
                    return;
                }
                if (mazeRows[i + 1][j] != '#')
                {
                    // Console.WriteLine("track: down");
                    FloodFill((i + 1, j), junction, depth + 1, visitedNodes);
                }
                if (mazeRows[i - 1][j] != '#')
                {
                    // Console.WriteLine("track: up");
                    FloodFill((i - 1, j), junction, depth + 1, visitedNodes);
                }
                if (mazeRows[i][j + 1] != '#')
                {
                    // Console.WriteLine("track: right");
                    FloodFill((i, j + 1), junction, depth + 1, visitedNodes);
                }
                if (mazeRows[i][j - 1] != '#')
                {
                    // Console.WriteLine("track: left");
                    FloodFill((i, j - 1), junction, depth + 1, visitedNodes);
                }
            }


            Tree maze = new(nodes[start], end);

            return maze;
        }
    }

    class MazeTraversal
    {
        List<Node> Path = [];
        List<List<Node>> Paths = [];
        public int DFS(Tree maze)
        {
            if (maze.Root == null)
                return 0;

            HashSet<(int,int)> visited = [];
            DFSHelper(maze.Root, visited);
            int minDistance = int.MaxValue;
            foreach(List<Node> path in Paths)
            {
                // Console.WriteLine($"{string.Join(',', Path.Select(n => n.Pos))}");
                if (path.Count != 0 && path.Last().Pos == maze.End)
                {
                    int distance = GetDistance(path);
                    minDistance = Math.Min(distance, minDistance);
                }
            }
            return minDistance;
        }

        private int DFSHelper(Node node, HashSet<(int,int)> visited)
        {
            if (node == null)
                return 0;

            if(!visited.Add(node.Pos))
            {
                return 0;
            }
            else
            {
                Path.Add(node);
                Paths.Add(Path.ToArray().Select(n => n).ToList());
            }

            int minDistance = GetDistance(); // Will always be replaced by child.
            foreach (var adjacentNode in node.AdjacentNodes)
            {

                int distance = DFSHelper(adjacentNode.Key, visited);
                minDistance = Math.Min(minDistance, distance);
            }

            visited.Remove(node.Pos);
            Path.Remove(node);

            return minDistance;
        }

        private int GetDistance()
        {
            int distance = 0;
            for(int i = 1; i < Path.Count; i++)
            {
                distance += Path[i].DistanceFromNode[Path[i - 1].Pos];
            }
            return distance;
        }

        private int GetDistance(List<Node> path)
        {
            int distance = 0;
            for(int i = 1; i < path.Count; i++)
            {
                distance += path[i].DistanceFromNode[path[i - 1].Pos];
            }
            return distance;
        }

    }

}