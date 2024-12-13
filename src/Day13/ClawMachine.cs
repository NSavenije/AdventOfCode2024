#nullable disable

using System.Security.Principal;
using System.Text.RegularExpressions;

static class Day13
{
    public static void Solve1() => Solve(true);

    public static void Solve2() => Solve(false);

    static void Solve(bool part1)
    {
        string filePath = "src/Day13/13.in";
        List<Machine> machines = ParseInput(filePath);
        int res = 0;
        foreach(Machine machine in machines)
        {
            Console.WriteLine(machine);
            int cost = MinEnergyCost(machine);
            res = cost >= 0 ? res + cost : res;
        }
        Console.WriteLine(res);
    }

    static int MinEnergyCost(Machine m) 
    { 
        var dp = new Dictionary<(int, int), int> {[(0, 0)] = 0 }; 
        var queue = new Queue<(int, int, int, int)>(); 
        queue.Enqueue((0, 0, 0, 0)); 
        int px = m.Prize.X;
        int py = m.Prize.Y;
        while (queue.Count > 0) 
        { 
            var (cx, cy, pa, pb) = queue.Dequeue(); 
            int currentEnergy = dp[(cx, cy)]; 
            // Move using button A 
            int nx = cx + m.ButtonA.X; 
            int ny = cy + m.ButtonA.Y; 
            if (pa <= 100 && nx <= px && ny <= py && (!dp.ContainsKey((nx, ny)) || currentEnergy + 3 < dp[(nx, ny)])) 
            { 
                dp[(nx, ny)] = currentEnergy + 3;
                queue.Enqueue((nx, ny, pa + 1, pb));
            } 
            // Move using button B 
            nx = cx + m.ButtonB.X; 
            ny = cy + m.ButtonB.Y; 
            if (pb <= 100 && nx <= px && ny <= py && (!dp.ContainsKey((nx, ny)) || currentEnergy + 1 < dp[(nx, ny)])) 
            { 
                dp[(nx, ny)] = currentEnergy + 1; 
                queue.Enqueue((nx, ny, pa, pb + 1)); 
            } 
        } 
        return dp.ContainsKey((px, py)) ? dp[(px, py)] : -1; 
    }

    private static ulong GCD(ulong a, ulong b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    static List<Machine> ParseInput(string filePath) 
    { 
        var machines = new List<Machine>(); 
        var lines = File.ReadAllLines(filePath); 
        var input = string.Join("\n", lines);
        var machineStrings = input.Split(["\n\n"], StringSplitOptions.None); 
        var pattern = @"Button A: X\+(?<ax>\d+), Y\+(?<ay>\d+)\s+Button B: X\+(?<bx>\d+), Y\+(?<by>\d+)\s+Prize: X=(?<px>\d+), Y=(?<py>\d+)"; 
        foreach (var machineString in machineStrings) 
        { 
            var match = Regex.Match(machineString, pattern, RegexOptions.Multiline); 
            if (match.Success) 
            { 
                var machine = new Machine { 
                    ButtonA = (int.Parse(match.Groups["ax"].Value), int.Parse(match.Groups["ay"].Value)), 
                    ButtonB = (int.Parse(match.Groups["bx"].Value), int.Parse(match.Groups["by"].Value)), 
                    Prize = (int.Parse(match.Groups["px"].Value), int.Parse(match.Groups["py"].Value)) }; 
                machines.Add(machine); 
            } 
        } 
        return machines;
    }

    public class Machine
    {
        public (int X, int Y) ButtonA { get; set; }
        public (int X, int Y) ButtonB { get; set; }
        public (int X, int Y) Prize { get; set; }

        public override string ToString()
        {
            return $"Button A: X+{ButtonA.X}, Y+{ButtonA.Y}, Button B: X+{ButtonB.X}, Y+{ButtonB.Y}, Prize: X={Prize.X}, Y={Prize.Y}";
        }
    }

}