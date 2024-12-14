#nullable disable

using System.Text.RegularExpressions;

static class Day13
{
    public static void Solve1() => Solve(true);

    public static void Solve2() => Solve(false);

    static void Solve(bool part1)
    {
        string filePath = "src/Day13/13.in";
        List<Machine> machines = ParseInput(filePath, part1);
        long tokens = 0;
        foreach(Machine m in machines)
        {
            // Cramer's rule?
            long det = m.ButtonA.X * m.ButtonB.Y - m.ButtonA.Y * m.ButtonB.X;
            if (((m.Prize.X * m.ButtonB.Y - m.Prize.Y * m.ButtonB.X) % det == 0) 
                && (m.Prize.Y * m.ButtonA.X - m.Prize.X * m.ButtonA.Y) % det == 0)
            {
                long a = (m.Prize.X * m.ButtonB.Y - m.Prize.Y * m.ButtonB.X) / det;
                long b = (m.Prize.Y * m.ButtonA.X - m.Prize.X * m.ButtonA.Y) / det;
                tokens += 3 * a + b;
            }
        }
        Console.WriteLine(tokens);
    }

    static List<Machine> ParseInput(string filePath, bool part1) 
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
                    ButtonA =           (int.Parse(match.Groups["ax"].Value), int.Parse(match.Groups["ay"].Value)), 
                    ButtonB =           (int.Parse(match.Groups["bx"].Value), int.Parse(match.Groups["by"].Value)), 
                    Prize = part1   ?   (long.Parse(match.Groups["px"].Value), long.Parse(match.Groups["py"].Value)) 
                                    :   (long.Parse(match.Groups["px"].Value) + 10000000000000, long.Parse(match.Groups["py"].Value) + 10000000000000) }; 
                machines.Add(machine); 
            } 
        } 
        return machines;
    }

    public class Machine
    {
        public (int X, int Y) ButtonA { get; set; }
        public (int X, int Y) ButtonB { get; set; }
        public (long X, long Y) Prize { get; set; }

        public override string ToString()
        {
            return $"Button A: X+{ButtonA.X}, Y+{ButtonA.Y}, Button B: X+{ButtonB.X}, Y+{ButtonB.Y}, Prize: X={Prize.X}, Y={Prize.Y}";
        }
    }

}