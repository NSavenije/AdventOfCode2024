#nullable disable

static class Day17
{
    public static void Solve1()
    {
        string filePath = "src/Day17/17.in";
        string[] lines = File.ReadAllLines(filePath);
        long A = int.Parse(lines[0].Split(": ")[1]);
        long B = int.Parse(lines[1].Split(": ")[1]);
        long C = int.Parse(lines[2].Split(": ")[1]);
        long[] operands = [0,1,2,3,A,B,C];
        long[] program = lines[4].Split(": ")[1].Split(',').Select(long.Parse).ToArray();
        List<long> output = RunProgram(program, operands);
        Console.WriteLine(string.Join(',', output));
    }

    public static void Solve2()
    {
        string filePath = "C:/Users/nouds/Repos/AdventOfCode2024/src/Day17/17.in";
        string[] lines = File.ReadAllLines(filePath);
        long[] program = lines[4].Split(": ")[1].Split(',').Select(long.Parse).ToArray();
        int pointer = program.Length - 1;
        long result = DFS(program, pointer, 0);
        Console.WriteLine(result);
        
    }

    static long DFS(long[] program, int cursor, long current)
    {
        for (int candidate = 0; candidate < 8; candidate++)
        {
            List<long> p1 = RunProgram(program, [0, 1, 2, 3, current * 8 + candidate, 0, 0]);
            List<long> p2 = program.Skip(cursor).ToList();
            if (p1.SequenceEqual(p2))
            {
                if (cursor == 0)
                    return current * 8 + candidate;

                var result = DFS(program, cursor - 1, current * 8 + candidate);
                if (result > -1)
                    return result;
            }
        }
        return -1;
    }

    static List<long> RunProgram(long[] program, long[] operands)
    {
        List<long> output = [];
        for(long i = 0; i < program.Length - 1; i += 2)
        {
            long opcode = program[i];
            long literal = program[i + 1];
            long combo = operands[literal];
            
            switch(opcode)
            {
                case 0: // ADV / Division
                    long num = operands[4];
                    double denom = Math.Pow(2, combo);
                    operands[4] = (long)(num / denom);
                    break;
                case 1: // BXL bitwise XOR
                    long b = operands[5];
                    operands[5] = b ^ literal;
                    break;  
                case 2: // bst modulo
                    operands[5] = combo % 8;
                    break;
                case 3: // jnz jump
                    if (operands[4] != 0)
                        i = literal - 2;
                    break;
                case 4: // bxc bitwise xor combo
                    operands[5] = operands[5] ^ operands[6];
                    break;
                case 5: // out
                    output.Add(combo % 8);
                    break;
                case 6: // bdv division
                    operands[5] = (long)(operands[4] / Math.Pow(2, combo));
                    break;
                case 7: // cdv
                    operands[6] = (long)(operands[4] / Math.Pow(2, combo));
                    break;
            }
        }
        return output;   
    }
}

