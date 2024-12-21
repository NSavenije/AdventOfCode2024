#nullable disable

static class Day21
{
    public static void Solve1()
    {
        string filePath = "C:/Users/nouds/Repos/AdventOfCode2024/src/Day21/21b.in";
        string[] codes = File.ReadAllLines(filePath);
        // Console.WriteLine(codes[0] + ": " + TranslateNumpadCode(codes[0]));
        long total = 0;
        foreach (string code in codes)
        {
            string res = TranslateNumpadCode(code);
            // Console.WriteLine(code + ": " + res);
            res = TranslateKeypadCode(res);
            // Console.WriteLine(code + ": " + res);
            res = TranslateKeypadCode(res);
            int num = int.Parse(new string(code.Where(char.IsDigit).ToArray()));
            Console.WriteLine(code + ": " + res);
            total += res.Length * num;
        }
        Console.WriteLine(total);
    }

    public static void Solve2()
    {
        string filePath = "src/Day21/21.in";
        string[] lines = File.ReadAllLines(filePath);
    }

    private static string TranslateNumpadCode(string code)
    {
        string res = "";
        code = "A" + code;
        for(int i = 0; i < code.Length - 1; i++)
            res += TranslateNumpad(code[i], code[i + 1]);
        return res;
    }

    private static string TranslateNumpad(char init, char goal)
    {
        // +---+---+---+
        // | 7 | 8 | 9 |
        // +---+---+---+
        // | 4 | 5 | 6 |
        // +---+---+---+
        // | 1 | 2 | 3 |
        // +---+---+---+
        //     | 0 | A |
        //     +---+---+
        
        var (r1,c1) = GetNumpadCoord(init);
        var (r2,c2) = GetNumpadCoord(goal);
        
        string res = "";
        res += c2 > c1 ? new string('>', c2 - c1) : "";
        res += r1 > r2 ? new string('^', r1 - r2) : "";
        res += c1 > c2 ? new string('<', c1 - c2) : "";
        res += r2 > r1 ? new string('v', r2 - r1) : "";
        res += "A";
        return res;
    }

    private static char GetNumpadChar((int r,int c) coord) => coord switch
    {
        (0,0) => '7', 
        (0,1) => '8',
        (0,2) => '9',
        (1,0) => '4',
        (1,1) => '5',
        (1,2) => '6',
        (2,0) => '1',
        (2,1) => '2',
        (2,2) => '3',
        (3,1) => '0',
        (3,2) => 'A',
        _ => throw new IndexOutOfRangeException()
    };

    private static (int,int) GetNumpadCoord(char c)
    {
        var row = c switch
        {
            '7' or '8' or '9' => 0,
            '4' or '5' or '6' => 1,
            '1' or '2' or '3' => 2,
            _ => 3,
        };
        var col = c switch
        {
            '7' or '4' or '1' => 0,
            '8' or '5' or '2' or '0' => 1,
            _ => 2
        };
        return (row,col);
    }

    private static string TranslateKeypadCode(string code)
    {
        string res = "";
        code = "A" + code;
        for(int i = 0; i < code.Length - 1; i++)
            res += TranslateKeypad(code[i], code[i + 1]);
        return res;
    }

    private static string TranslateKeypad(char init, char goal)
    {
        //     +---+---+
        //     | ^ | A |
        // +---+---+---+
        // | < | v | > |
        // +---+---+---+
        
        var (r1,c1) = GetKeypadChar(init);
        var (r2,c2) = GetKeypadChar(goal);
        // Right & down first
        string res = "";
        res += c2 > c1 ? new string('>', c2 - c1) : "";
        res += r2 > r1 ? new string('v', r2 - r1) : "";
        res += r1 > r2 ? new string('^', r1 - r2) : "";
        res += c1 > c2 ? new string('<', c1 - c2) : "";
        
        res += "A";
        return res;
    }

    private static (int,int) GetKeypadChar(char c)
    {
        var row = c switch
        {
            '^' or 'A' => 0,
            _ => 1
        };
        var col = c switch
        {
            '<' => 0,
            '^' or 'v' => 1,
            _ => 2
        };
        return (row,col);
    }
}