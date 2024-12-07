#nullable disable

using System.Data;
using System.Text;

static class Day7 {
    
    public static void Solve1() => ParseInput(false);

    public static void Solve2() => ParseInput(true);

    static void ParseInput(bool allowConcat)
    {
        string filePath = "src/Day7/7.in";
        using StreamReader sr = new(filePath);
        string line;
        long res = 0;
        while((line = sr.ReadLine()) != null)
        {
            var split = line.Split(": ");
            long target = long.Parse(split[0]);
            List<int> vars = split[1].Split(' ').Select(int.Parse).ToList();

            if (CanReachTarget(vars, target, allowConcat))
            {
                res += target;
            }
        }
        Console.WriteLine(res);
    }

    static bool CanReachTarget(List<int> vars, long target, bool allowConcat) =>
        CheckCombinations(vars, 0, vars[0], target, allowConcat);

    static bool CheckCombinations(List<int> vars, int index, long current, long target, bool allowConcat) 
    {   
        if (index == vars.Count - 1) 
        {   
            return current == target;
        } 
        // Trying addition 
        if (CheckCombinations(vars, index + 1, current + vars[index + 1], target, allowConcat)) 
        {   
            return true;
        } 
        // Trying multiplication 
        if (CheckCombinations(vars, index + 1, current * vars[index + 1], target, allowConcat)) 
        {   
            return true;
        } 
        // Trying concatenation
        if (allowConcat && CheckCombinations(vars, index + 1, Concat(current, vars[index + 1]), target, allowConcat)) 
        {   
            return true;
        }
        return false;
    }   

    static long Concat(long current, int var) =>
        long.Parse(current.ToString() + var.ToString());
}

