#nullable disable

static class Day21
{
    public static void Solve1() => Solve(2);     

    public static void Solve2() => Solve(25);

    private static void Solve(int robots)
    {
        string filePath = "src/Day21/21.in";
        string[] codes = File.ReadAllLines(filePath);
        long total = 0;
        foreach (string code in codes)
        {
            long keyPresses = GetKeyPresses(code, robots, numpad);
            int num = int.Parse(new string(code.Where(char.IsDigit).ToArray()));
            total += keyPresses * num;
        }
        Console.WriteLine(total);
    }

    private static long GetKeyPresses(string code, long robots, Dictionary<char, (int r, int c)> input) 
    { 
        if (memo.TryGetValue((code,robots), out long val)) 
            return val; 
        
        char current = 'A';
        long length = 0; 
        for (int i = 0; i < code.Length; i++) 
        {
            List<string> moves = GetCommand(input, current, code[i]); 
            if (robots == 0) 
                length += moves[0].Length; 
            else 
                length += moves.Min(move => GetKeyPresses(move, robots - 1, keypad)); 
            current = code[i];
        } 
        memo[(code,robots)] = length; 
        return length; 
    }

    private static List<string> GetCommand(Dictionary<char, (int r, int c)> input, char start, char end)
    {
        Queue<(int r, int c, string path)> queue = [];

        queue.Enqueue((input[start].r, input[start].c, string.Empty));
        Dictionary<(int,int), long> distances = [];

        if (start == end) return ["A"];

        List<string> allPaths = [];
        while (queue.Count != 0)
        {
            var cur = queue.Dequeue();
            if ((cur.r, cur.c) == input[end]) 
                allPaths.Add(cur.path + "A");

            if (distances.TryGetValue((cur.r,cur.c), out var d) && d < cur.path.Length) 
                continue;

            foreach (var direction in dirs)
            {
                var (r, c) = (cur.r + direction.Value.r, cur.c + direction.Value.c);

                if (!input.ContainsValue((r,c)) || (r,c) == input['#'])
                    continue;
                
                var newPath = cur.path + direction.Key;
                if (!distances.ContainsKey((r,c)) || distances[(r,c)] >= newPath.Length)
                {
                    queue.Enqueue((r, c, newPath));
                    distances[(r,c)] = newPath.Length;
                }
            }
        }

        return allPaths.OrderBy(p => p.Length).ToList();
    }

    private static readonly Dictionary<(string code,long robots), long> memo = [];

    private static readonly Dictionary<char, (int r,int c)> numpad = new()
    {
        {'7', (0,0)}, {'8', (0,1)}, {'9', (0,2)},
        {'4', (1,0)}, {'5', (1,1)}, {'6', (1,2)},
        {'1', (2,0)}, {'2', (2,1)}, {'3', (2,2)},
        {'#', (3,0)}, {'0', (3,1)}, {'A', (3,2)}
    };

    private static readonly Dictionary<char, (int r,int c)> dirs = new()
    {
        {'^', (-1,0)}, {'<', (0,-1)}, {'>', (0,1)}, {'v', (1,0)}
    };

    private static readonly Dictionary<char, (int r,int c)> keypad = new()
    {
        {'#', (0,0)}, {'^', (0,1)}, {'A', (0,2)},
        {'<', (1,0)}, {'v', (1,1)}, {'>', (1,2)}
    };
}