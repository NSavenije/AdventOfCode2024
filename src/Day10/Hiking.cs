#nullable disable

static class Day10
{
    public static void Solve1() => Solve(true);

    public static void Solve2() => Solve(false);

    static void Solve(bool distinct)
    {
        int[,] grid = ParseInput("src/Day10/10.in", out List<(int,int)> trailHeads);
        int res = FindAllPaths(grid, trailHeads, distinct);
        Console.WriteLine(res);
    }   

    static int[,] ParseInput(string filePath, out List<(int,int)> trailHeads)
    {
        string[] inputList = File.ReadAllLines(filePath);
        int size = inputList.Length;

        int[,] grid = new int[size, size];
        trailHeads = [];

        // Transform the list to a grid of ints 
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                int val = int.Parse(inputList[i][j].ToString());
                grid[i, j] = val;
                if (val == 0)
                    trailHeads.Add((i, j));
            }
        }
        return grid;
    }

    static int FindAllPaths(int[,] grid, List<(int i, int j)> trailHeads, bool distinct)
    {
        int res = 0;
        foreach (var trailHead in trailHeads)
        {
            List<(int,int)> score = FindPaths(grid, trailHead);
            if(distinct)
                score = score.Distinct().ToList();
            res += score.Count;
        }
        return res;
    }

    static List<(int,int)> FindPaths(int[,] grid, (int i, int j) start)
    {
        List<(int,int)> res = [];
        (int i, int j) = start;
        int val = grid[i,j];
        if (val == 9)
            return [(i,j)];

        // Recursively follow all legal paths
        foreach((int ni, int nj) in GetNeighbours(grid, i, j))
            if (grid[ni,nj] - 1 == val)
                res.AddRange(FindPaths(grid, (ni,nj)));

        return res;
    }

    static List<(int, int)> GetNeighbours(int[,] grid, int x, int y)
    {
        List<(int, int)> neighbours = [];
        int[] dx = [-1, 0, 1, 0];
        int[] dy = [0, 1, 0, -1];
        int size = grid.GetLength(0);
        for (int i = 0; i < 4; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];
            if (newX >= 0 && newX < size && newY >= 0 && newY < size)
            {
                neighbours.Add((newX, newY));
            }
        }
        return neighbours;
    }
}