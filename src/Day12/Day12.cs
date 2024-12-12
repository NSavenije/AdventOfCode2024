#nullable disable

static class Day12 
{
    public static void Solve1() => Solve(true);

    public static void Solve2()
    {
        string filePath = "C:\\Users\\nouds\\Repos\\AdventOfCode2024\\src\\Day12\\12.in";
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

        long res = 0;
        foreach(var r in regions)
        {
            int area = r.Count;
            int sides = CalcSides(r);
            int score = area * sides;
            res += score;
        }
        Console.WriteLine(res);
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
            // Am I top left? i.e. is the thing above me and the thing to the left not part of region
            if (!region.Contains(dirs[0]) && !region.Contains(dirs[3]))
                vertices++;
            
            // Am I top right? i.e. is the thing above me and the thing to the right not part of region
            if (!region.Contains(dirs[0]) && !region.Contains(dirs[1]))
                vertices++;

            // Am I bottom left? i.e. is the thing below me and the thing to the left not part of region
            if (!region.Contains(dirs[2]) && !region.Contains(dirs[3]))
                vertices++;

            // Am I bottom left? i.e. is the thing below me and the thing to the right not part of region
            if (!region.Contains(dirs[2]) && !region.Contains(dirs[1]))
                vertices++;

            // Inside corners...
            // Top left elbow?
            if (region.Contains(dirs[0]) && region.Contains(dirs[3]) && !region.Contains(dirs[4]))
                vertices++;
            // Top right elbow?
            if (region.Contains(dirs[0]) && region.Contains(dirs[1]) && !region.Contains(dirs[5]))
                vertices++;
            // Bottom right elbow?
            if (region.Contains(dirs[2]) && region.Contains(dirs[1]) && !region.Contains(dirs[6]))
                vertices++;
            // Bottom left elbow?
            if (region.Contains(dirs[2]) && region.Contains(dirs[3]) && !region.Contains(dirs[7]))
                vertices++;
        }

        return vertices;
    }

    static void Solve(bool part1)
    {
        string filePath = "C:\\Users\\nouds\\Repos\\AdventOfCode2024\\src\\Day12\\12.in";
        char[,] garden = ParseInput(filePath);
        List<List<int>> regions = []; 
        HashSet<(int,int)> partOfRegion = [];
        int currentRegion = 0;
        for (int i = 0; i < garden.GetLength(0); i++)
        {
            for (int j = 0; j < garden.GetLength(1); j++)
            {
                if(partOfRegion.Contains((i,j)))
                    continue;
                
                List<int> neighbourCounts = [];
                Queue<(int,int)> q = [];
                q.Enqueue((i,j));
                
                while (q.Count != 0)
                {
                    var plot = q.Dequeue();
                    if (partOfRegion.Contains(plot))
                        continue;
                    partOfRegion.Add(plot);
                    var neighbours = GetNeighbours(garden,plot.Item1,plot.Item2);
                    neighbourCounts.Add(4 - neighbours.Count);
                    foreach(var n in neighbours)
                        if (!partOfRegion.Contains(n))
                            q.Enqueue(n);
                }

                regions.Add(neighbourCounts);
                currentRegion++;
            }
        }

        long res = 0;
        foreach(var r in regions)
        {
            int area = r.Count;
            int perimiter = r.Sum();
            int score = area * perimiter;
            res += score;
        }
        Console.WriteLine(res);
    }

    static char[,] ParseInput(string filePath)
    {
        string[] inputList = File.ReadAllLines(filePath);
        int size = inputList.Length;

        char[,] grid = new char[size, size];

        // Transform the list to a grid of ints 
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
}