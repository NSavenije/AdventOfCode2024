#nullable disable

using System.Data;
using System.Text;

static class Day9 {
    
    public static void Solve1()
    {
        int[] blocks = ParseInput("src/Day9/9.in");
        Defragment(blocks);
        long res = CalcChecksum(blocks);
        Console.WriteLine(res);
    }

    public static void Solve2()
    {
        int[] blocks = ParseInput("src/Day9/9.in");
        DefragmentBlocks(blocks);
        long res = CalcChecksum(blocks, true);
        Console.WriteLine(res);
    }

    static int[] ParseInput(string filePath)
    {
        string disk = File.ReadAllLines(filePath)[0];
        List<int> blocks = [];
        int counter = 0;
        foreach(char c in disk)
        {
            int n = c - 48; //ASCII
            int val = counter % 2 == 0 ? counter / 2 : -1 * counter;
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

    static int[] DefragmentBlocks(int[] blocks)
    {
        Dictionary<int, (int index, int size)> blockDict = [];
        Dictionary<int, (int index, int size)> spaceDict = [];
        bool inBlock = true;
        int si = blocks.Length - 1;
        for(int i = blocks.Length - 2; i > 0; i--)
        {
            if (blocks[i] < blocks[i + 1] && inBlock)
            {
                blockDict.Add(blocks[i+1], ( si , si - i ));
                inBlock = false;
            }
            if (!inBlock && blocks[i] >= 0)
            {
                inBlock = true;
                si = i;
            }
        }

        inBlock = true;
        for(int i = 0; i < blocks.Length - 1; i++)
        {
            if (inBlock && blocks[i] < 0)
            {
                inBlock = false;
                si = i;
            }
            if(!inBlock && blocks[i] >= 0)
            {
                inBlock = true;
                spaceDict.Add(blocks[i - 1], ( si , i - si ));
            }
        }

        foreach(var block in blockDict.Keys)
        {
            // Find first available space
            var (index, size) = blockDict[block];
            (int index, int size) sp = (0,0);
            int spk = 0;
            bool spaceFound = false;
            foreach(var space in spaceDict.Keys)
            {
                sp = spaceDict[space];
                if (sp.index >= index)
                    break;
                if (sp.size >= size)
                {
                    spaceFound = true;
                    spk = space;
                    break;
                }
            }
            if(spaceFound)
            {
                // Move
                for(int i = sp.index; i < sp.index + size; i++)
                {
                    blocks[i] = block;
                }
                for(int i = index; i > index - size; i--)
                {
                    blocks[i] = -1;
                }

                // Update SpaceDict
                spaceDict[spk] = ( sp.index + size , sp.size - size);
            }
        } 
        return blocks;
    }

    static long CalcChecksum(int[] blocks, bool gaps = false)
    {
        long res = 0;
        List<int> ints = [];
        ints = gaps ? blocks.ToList() : blocks.Where(x => x >= 0).ToList();

        for(int i = 0; i < ints.Count; i++)
            if (blocks[i] >= 0)
                res += ints[i] * i;

        return res;
    }

    static string ToString(int[] blocks)
    {
        StringBuilder result = new(); 
        foreach (int value in blocks) 
        { 
            if (value >= 0) result.Append(value); 
            else result.Append('.'); 
        } 
        return result.ToString();
    }
}