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
        string filePath = "C:/Users/nouds/Repos/AdventOfCode2024/src/Day15/15.in";
        string[] file = File.ReadAllText(filePath).Split("\r\n\r\n");
        string[,] warehouse = ParseWideWarehouse(file[0], out (int, int) init);
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
            if (newTile != "[" && newTile != "]")
            {
                Console.WriteLine(stepCounter + " " + newTile);
                Print(warehouse);
                throw new NotImplementedException();
            }

            // we'll push some boxes's.
            // If left or right (1 || 3) normal box row.
            // If up or down, towers of boxes can exist.
            if (dir == 1 || dir == 3)
            {
                int tempX = x + dx;
                while(warehouse[tempX,y] == "]" || warehouse[tempX,y] == "[")
                    tempX += dx;
                // Can't push those boxes into a wall!
                if (warehouse[tempX, y] == "#")
                    continue;

                tempX = x + dx;
                while(warehouse[tempX,y] == "]" || warehouse[tempX,y] == "[")
                {
                    warehouse[tempX,y] = warehouse[tempX,y] == "]" ? "[" : "]";
                    tempX += dx;
                }
                warehouse[tempX,y] = warehouse[tempX - dx ,y] == "]" ? "[" : "]";
                warehouse[x + dx, y] = "@";
                warehouse[x, y] = ".";
                x += dx;
            }
            // Push the boxes up or down
            else
            {
                Queue<(int,int)> boxQ = [];
                boxQ.Enqueue((x, y + dy));
                int tdx = warehouse[x,y + dy] == "[" ? 1 : -1;
                boxQ.Enqueue((x + tdx, y + dy));
                List<(int,int)> boxes = [];
                bool moveable = true;
                while(boxQ.Count != 0)
                {
                    var (bx,by) = boxQ.Dequeue();
                    boxes.Add((bx,by));
                    // push into #, stop entire move
                    if (warehouse[bx,by + dy] == "#")
                    {
                        moveable = false;
                        break;
                    }

                    string tile = warehouse[bx,by]; 
                    // If [ above [                    
                    if (tile == warehouse[bx,by + dy])
                    {
                        boxQ.Enqueue((bx,by + dy));
                        continue;
                    }
                    // If . above [
                    if (warehouse[bx,by + dy] == ".")
                        continue;

                    // If ] above [
                    tdx = warehouse[bx,by + dy] == "[" ? 1 : -1;
                    if (!boxQ.Contains((bx, by + dy)))
                        boxQ.Enqueue((bx, by + dy));
                    if (!boxQ.Contains((bx + tdx, by + dy)))
                        boxQ.Enqueue((bx + tdx, by + dy));
                }
                if (!moveable)
                    continue;

                boxes = boxes.Distinct().Reverse().ToList();
                foreach(var (bx,by) in boxes)
                {
                    warehouse[bx, by + dy] = warehouse[bx, by];
                    warehouse[bx, by] = ".";
                }
                warehouse[x, y + dy] = "@";
                warehouse[x, y] = ".";
                y += dy;
            }
            
        }

        // Print(warehouse);
        Console.WriteLine(CalcScore(warehouse, true));
    }

    static int CalcScore(string[,] warehouse, bool wide = false)
    {
        int score = 0;
        var boxTile = wide ? "[" : "O";
        for(int x = 1; x < warehouse.GetLength(0) - 1; x++)
        {
            for(int y = 1; y < warehouse.GetLength(1) - 1; y++)
            {
                if(warehouse[x,y] == boxTile)
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

    static string[,] ParseWideWarehouse(string input, out (int x,int y) init)
    {
        string[] lines = input.Split("\r\n");
        init = (-1,-1);
        string[,] warehouse = new string[lines[0].Length * 2,lines.Length];
        for(int i = 0; i < lines.Length; i++)
        {
            for(int j = 0; j < lines[0].Length; j++)
            {
                string tile = lines[i][j].ToString();
                if (tile == "O")
                {
                    warehouse[2 * j,i] =  "[";
                    warehouse[2 * j+1,i] =  "]";
                    continue;
                }
                if (tile == "@")
                {
                    warehouse[2 * j,i] = "@";
                    warehouse[2 * j + 1,i] = ".";
                    init = (2 * j,i);
                    continue;
                }
                warehouse[2 *j,i] = tile;
                warehouse[2 *j + 1,i] = tile;
                
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

