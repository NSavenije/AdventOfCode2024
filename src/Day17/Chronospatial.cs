#nullable disable

static class Day17
{
    // Register A: 729
    // Register B: 0
    // Register C: 0

    // Program: 0,1,5,4,3,0

    public static void Solve1()
    {
        string filePath = "src/Day17/17.in";
        string[] lines = File.ReadAllLines(filePath);
        int A = int.Parse(lines[0].Split(": ")[1]);
        int B = int.Parse(lines[1].Split(": ")[1]);
        int C = int.Parse(lines[2].Split(": ")[1]);
        int[] operands = [0,1,2,3,A,B,C];
        int[] program = lines[4].Split(": ")[1].Split(',').Select(int.Parse).ToArray();
        // Console.WriteLine("A: " + A + " B: " +  B + " C: " + C + " P: " + string.Join(',', program));
        List<int> output = [];
        for(int i = 0; i < program.Length - 1; i += 2)
        {
            int opcode = program[i];
            int literal = program[i + 1];
            int combo = operands[literal];
            
            switch(opcode)
            {
                case 0: // ADV / Division
                    int num = operands[4];
                    double denom = Math.Pow(2, combo);
                    operands[4] = (int)(num / denom);
                    break;
                case 1: // BXL bitwise XOR
                    int b = operands[5];
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
                    operands[5] = (int)(operands[4] / Math.Pow(2, combo));
                    break;
                case 7: // cdv
                    operands[6] = (int)(operands[4] / Math.Pow(2, combo));
                    break;
            }
        }
        Console.WriteLine(string.Join(',', output));
    }

    public static void Solve2()
    {
        
    }
}

