#nullable disable

using System.Collections.Specialized;
using System.Data;

using System.Text;


static class Day10
{

    public static void Solve1()
    {
        string filePath = "C:\\Users\\nouds\\Repos\\AdventOfCode2024\\src\\Day10\\10.in";
        string[] inputList = File.ReadAllLines(filePath);
        int size = inputList.Length;
        // Size of the grid 
        int[,] grid = new int[size, size];
        List<(int i, int j)> trailHeads = [];

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

        int res = FindAllPaths(grid, trailHeads);
        Console.WriteLine(res);

        // Print the grid to verify the result 
        // for (int i = 0; i < size; i++)
        // {
        //     for (int j = 0; j < size; j++)
        //     {
        //         Console.Write(grid[i, j] + " ");
        //     }
        //     Console.WriteLine();
        // }
    }
    public static void Solve2()
    {

    }

    static int FindAllPaths(int[,] grid, List<(int i, int j)> starts)
    {
        int res = 0;
        foreach (var trailHead in starts)
        {
            int score = FindPaths(grid, trailHead);
            res += score;
        }
        return res;
    }

    static int FindPaths(int[,] grid, (int i, int j) start)
    {
        int res = 0;
        (int i, int j) = start;
        int val = grid[i,j];
        List<(int, int)> neighbours = GetNeighbours(grid, i, j);
        if (val == 9)
        {
            return 1;
        }
        foreach((int ni, int nj) in neighbours)
        {
            int nval = grid[ni,nj];
            if (nval - 1 == val)
            {
                res += FindPaths(grid, (ni,nj));
            }
        }

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