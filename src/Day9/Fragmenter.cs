#nullable disable

using System.Data;
using System.Text;

static class Day9 {
    
    public static void Solve1()
    {
        string filePath = "src/Day9/9.in";
        string disk = File.ReadAllLines(filePath)[0];
        int[] blocks = ParseInput(disk);
        Console.WriteLine(ToString(blocks));

        int[] defragmented = Defragment(blocks);
        Console.WriteLine(ToString(defragmented));

        long res = CalcChecksum(blocks);
        Console.WriteLine(res);
    }

    public static void Solve2()
    {

    }

    static int[] ParseInput(string disk)
    {
        List<int> blocks = [];
        int counter = 0;
        foreach(char c in disk)
        {
            int n = c - 48; //ASCII
            // Generate a list 
            int val = counter % 2 == 0 ? counter / 2 : -1;
            for(int i = 0; i < n; i++)
            {
                blocks.Add(val);
            }
            counter++;
        }  
        return blocks.ToArray();
    }

    static int[] Defragment(int[] blocks)
    {
        int a = 0;
        for(int z = blocks.Length - 1; z > a; z--)
        {
            while(blocks[a] >= 0)
            {
                a++;
            }
            if(blocks[z] >= 0)
            {
                blocks[a] = blocks[z];
                blocks[z] = -1;
                a++;
            }
        }
        return blocks;
    }

    static long CalcChecksum(int[] blocks)
    {
        long res = 0;
        List<int> ints = blocks.Where(x => x >= 0).ToList();
        for(int i = 0; i < ints.Count; i++)
        {
            res += ints[i] * i;
        }
        return res;
    }

    static string ToString(int[] blocks)
    {
        StringBuilder result = new(); 
        foreach (int value in blocks) 
        { 
            if (value >= 0) 
            { 
                result.Append(value); 
            } 
            else
            {
                result.Append('.'); 
            } 
        } 
        return result.ToString();
    }
}