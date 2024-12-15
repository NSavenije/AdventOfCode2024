#nullable disable

static class Day15
{
    static readonly (int x,int y)[] Dirs = [(0,-1),(1,0),(0,1),(-1,0)];

    public static void Solve1()
    {
        string filePath = "C:/Users/nouds/Repos/AdventOfCode2024/src/Day15/15.in";
        string[] file = File.ReadAllText(filePath).Split("\r\n\r\n");
        string[,] warehouse = ParseWarehouse(file[0], out (int, int) init);
        List<int> dirs = ParseDirs(file[1]);
        (int x, int y) = init;
        int stepCounter = 0;
        foreach(int dir in dirs)
        {
            stepCounter++;
            // Print(warehouse);
            (int dx, int dy) = Dirs[dir];
            string newTile = warehouse[x + dx, y + dy];
            if (newTile == "#")
                continue;
            if (newTile == ".")
            {
                warehouse[x,y] = ".";
                warehouse[x + dx, y + dy] = "@";
                x += dx;
                y += dy;
                continue;
            }
            if (newTile != "O")
            {
                Console.WriteLine(stepCounter + " " + newTile);
                Print(warehouse);
                throw new NotImplementedException();
            }

            // we'll push some O's.
            // If the line of O's ends free space. Remove the O from start, place O at end.
            // If the line of O's ends in wall, continue.
            int tempX = x + dx; int tempY = y + dy;
            int boxCount = 0;
            while(warehouse[tempX,tempY] == "O")
            {
                boxCount++;
                tempX += dx;
                tempY += dy;
            }

            // Can't push those boxes into a wall!
            if (warehouse[tempX, tempY] == "#")
                continue;

            warehouse[tempX, tempY] = "O";
            warehouse[x + dx, y + dy] = "@";
            warehouse[x, y] = ".";
            x += dx;
            y += dy;
        }

        Print(warehouse);
        Console.WriteLine(CalcScore(warehouse));
    }

    public static void Solve2()
    {
        
    }

    static int CalcScore(string[,] warehouse)
    {
        int score = 0;

        for(int x = 1; x < warehouse.GetLength(0) - 1; x++)
        {
            for(int y = 1; y < warehouse.GetLength(1) - 1; y++)
            {
                if(warehouse[x,y] == "O")
                    score += (100 * y) + x;
            }
        }

        return score;
    }

    static string[,] ParseWarehouse(string input, out (int x,int y) init)
    {
        string[] lines = input.Split("\r\n");
        init = (-1,-1);
        string[,] warehouse = new string[lines[0].Length,lines.Length];
        for(int i = 0; i < lines.Length; i++)
        {
            for(int j = 0; j < lines[0].Length; j++)
            {
                warehouse[j,i] = lines[i][j].ToString(); 
                if (lines[i][j] == '@')
                    init = (j,i);
            }
        }
        return warehouse;
    }
    
    static List<int> ParseDirs(string input)
    {
        input = input.Replace("\r\n","");
        List<int> dirs = [];
        foreach(var c in input)
        {
            switch(c)
            {
                case '^':
                    dirs.Add(0);
                    break;
                case '>':
                    dirs.Add(1);
                    break;
                case 'v':
                    dirs.Add(2);
                    break;
                case '<':
                default:
                    dirs.Add(3);
                    break;
            }
        }
        return dirs;
    }

    static void Print(string[,] warehouse)
    {
        for(int x = 0; x < warehouse.GetLength(0); x++)
        {
            for(int y = 0; y < warehouse.GetLength(1); y++)
            {
                Console.Write(warehouse[y, x]);
            }
            Console.WriteLine();
        }
    }
}

