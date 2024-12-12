#nullable disable

static class Day12 
{
    public static void Solve1() => Solve(true);

    public static void Solve2() => Solve(false);

    static void Solve(bool part1)
    {
        string filePath = "src/Day12/12.in";
        char[,] garden = ParseInput(filePath);
        List<HashSet<(int,int)>> regions = []; 
        HashSet<(int,int)> partOfRegion = [];
        int currentRegion = 0;
        for (int i = 0; i < garden.GetLength(0); i++)
        {
            for (int j = 0; j < garden.GetLength(1); j++)
            {
                if(partOfRegion.Contains((i,j)))
                    continue;
                
                HashSet<(int,int)> region = [];
                Queue<(int,int)> q = [];
                q.Enqueue((i,j));
                
                while (q.Count != 0)
                {
                    var plot = q.Dequeue();
                    if (partOfRegion.Contains(plot))
                        continue;
                    partOfRegion.Add(plot);
                    var neighbours = GetNeighbours(garden,plot.Item1,plot.Item2);
                    region.Add(plot);
                    foreach(var n in neighbours)
                        if (!partOfRegion.Contains(n))
                            q.Enqueue(n);
                }

                regions.Add(region);
                currentRegion++;
            }
        }

        int res = 0;
        foreach(var r in regions)
        {
            int area = r.Count;
            int sides = part1 ? r.Select(plot => 4 - GetNeighbours(garden,plot.Item1,plot.Item2).Count).Sum() : CalcSides(r);
            int score = area * sides;
            res += score;
        }
        Console.WriteLine(res);
    }

    static char[,] ParseInput(string filePath)
    {
        string[] inputList = File.ReadAllLines(filePath);
        int size = inputList.Length;

        char[,] grid = new char[size, size];

        for (int i = 0; i < size; i++)
            for (int j = 0; j < size; j++)
                grid[i, j] = inputList[i][j];
        return grid;
    }

    static List<(int,int)> GetNeighbours(char[,] grid, int x, int y)
    {
        List<(int,int)> neighbours = [];
        int[] dx = [-1, 0, 1, 0];
        int[] dy = [0, 1, 0, -1];
        int size = grid.GetLength(0);
        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];
            if (newX >= 0 && newX < size && newY >= 0 && newY < size)
            {
                if (grid[x,y] == grid[newX,newY])
                    neighbours.Add((newX,newY));
            }
        }
        return neighbours;
    }

    static int CalcSides(HashSet<(int, int)> region)
    {
        int vertices = 0;
        foreach((int i, int j) in region)
        {
            (int,int)[] dirs = 
            [
                (i - 1, j),(i, j + 1),(i + 1, j),(i, j - 1),
                (i - 1, j - 1),(i - 1, j + 1),(i + 1, j + 1),(i + 1, j - 1)
            ];

            bool top = !region.Contains(dirs[0]);
            bool right = !region.Contains(dirs[1]);
            bool bottom = !region.Contains(dirs[2]);
            bool left = !region.Contains(dirs[3]);
            bool topLeft = !region.Contains(dirs[4]);
            bool topRight = !region.Contains(dirs[5]);
            bool bottomRight = !region.Contains(dirs[6]);
            bool bottomLeft = !region.Contains(dirs[7]);
            
            if (top && left)     vertices++;
            if (top && right)    vertices++;
            if (bottom && right) vertices++;
            if (bottom && left)  vertices++;
            // Inside corners...
            if (!top && !left && topLeft)         vertices++;
            if (!top && !right && topRight)       vertices++;
            if (!bottom && !right && bottomRight) vertices++;
            if (!bottom && !left && bottomLeft)   vertices++;
        }

        return vertices;
    }
}