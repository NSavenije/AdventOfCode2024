#nullable disable

using System.Data;
using System.Text;

static class Day7 {
    
    public static void Solve1() 
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

            bool valid = CanReachTarget(vars, target);
            if (valid)
            {
                res += target;
            }

        }
        Console.WriteLine(res);
    }

    public static void Solve2()
    {
    
    }

    static bool CanReachTarget(List<int> vars, long target) 
    {   
        return CheckCombinations(vars, 0, vars[0], target);
    } 
    static bool CheckCombinations(List<int> vars, int index, long current, long target) 
    {   
        if (index == vars.Count - 1) 
        {   
            return current == target;
        } 
        // Trying addition 
        if (CheckCombinations(vars, index + 1, current + vars[index + 1], target)) 
        {   
            return true;
        } 
        // Trying multiplication 
        if (CheckCombinations(vars, index + 1, current * vars[index + 1], target)) 
        {   
            return true;
        } 
        return false;
    }   
}

