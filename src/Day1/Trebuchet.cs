#nullable disable

static class Day1 {
    public static void Solve1() =>
        ParseInput();
    public static void Solve2() =>
        ParseInput();

    static void ParseInput()
    {
        string filePath = "src/Day1/1.in";
        using StreamReader sr = new(filePath);
        string line;
        int res = 0;
        while ((line = sr.ReadLine()) != null)
        {
            res += line.Length; 
        }
        Console.WriteLine(res);
    }
}