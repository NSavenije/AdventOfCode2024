#nullable disable

static class Day11
{
    public static void Solve1() => Solve(true);

    public static void Solve2() => Solve(false);

    static void Solve(bool partOne)
    {
        List<long> stones = File.ReadAllLines("src/Day11/11.in")[0].Split(' ').Select(long.Parse).ToList();
        for(int i = 0; i < 25; i++)
        {
            List<long> newStones = [];
            for(int s = 0; s < stones.Count; s++)
            {
                long stone = stones[s];
                int numLen = stone.ToString().Length;
                if (stone == 0)
                    newStones.Add(1);

                else if (numLen % 2 == 0)
                {
                    newStones.Add((long)(stone / Math.Pow(10, numLen / 2)));
                    newStones.Add((long)(stone % Math.Pow(10, numLen / 2)));
                }
                else
                    newStones.Add(stone * 2024);
            }

            stones = newStones;
            Console.WriteLine("it: " + i + " stones: " + stones.Count);
            // Console.WriteLine(string.Join(' ', stones));
        }
        Console.WriteLine(stones.Count);
    }
}