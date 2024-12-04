#nullable disable

using System;
using System.Text.RegularExpressions;

static class Day4 {
    public static void Solve1() {
        string filePath = "src/Day4/4.in";
        string[] rows = File.ReadAllLines(filePath);
        string[] cols = Transpose(rows);
        string[] dias = GetDiagonals(rows);

        string[] patterns = ["XMAS","SAMX"];
        int res = 0;
        
        foreach (string row in rows)
        {
            var matches = FindOverlappingMatches(row, patterns);
            res += matches.Count;
        }
        foreach (string col in cols)
        {
            var matches = FindOverlappingMatches(col, patterns);
            res += matches.Count;
        }
        foreach (string dia in dias)
        {
            var matches = FindOverlappingMatches(dia, patterns);
            res += matches.Count;
        }
        Console.WriteLine(res);
    }

    public static void Solve2()
    {
        string filePath = "src/Day4/4.in";
        string[] rows = File.ReadAllLines(filePath);
        int res = 0;
        // No reason to check the border
        for(int r = 1; r < rows.Length - 1; r++)
        {
            string row = rows[r];
            for(int c = 1; c < row.Length - 1; c++)
            {
                char col = row[c];
                if (col == 'A')
                {
                    char tl = rows[r - 1][c - 1];
                    char tr = rows[r - 1][c + 1];
                    char bl = rows[r + 1][c - 1];
                    char br = rows[r + 1][c + 1];
                    //M.M M.S S.S S.M
                    //.A. .A. .A. .A.
                    //S.S M.S M.M S.M
                    {
                        if ((tl == 'M' && tr == 'M' && bl == 'S' && br == 'S') 
                        ||  (tl == 'M' && tr == 'S' && bl == 'M' && br == 'S') 
                        ||  (tl == 'S' && tr == 'S' && bl == 'M' && br == 'M') 
                        ||  (tl == 'S' && tr == 'M' && bl == 'S' && br == 'M'))
                            res++;
                    }
                }
            }
        }
        Console.WriteLine(res);
    }

    static string[] Transpose(string[] array)
    {
        int length = array[0].Length;
        char[][] transposedArray = new char[length][];

        for (int i = 0; i < length; i++)
        {
            transposedArray[i] = new char[array.Length];
            for (int j = 0; j < array.Length; j++)
            {
                transposedArray[i][j] = array[j][i];
            }
        }

        return transposedArray.Select(chars => new string(chars)).ToArray();
    } 
    
    static List<string> FindOverlappingMatches(string input, string[] patterns) 
    { 
        List<string> matches = []; 
        foreach (var pattern in patterns) 
        { 
            for (int i = 0; i <= input.Length - pattern.Length; i++) 
            { 
                if (input.Substring(i, pattern.Length) == pattern) 
                { 
                    matches.Add(pattern); 
                } 
            } 
        } 
        return matches; 
    }

    static string[] GetDiagonals(string[] array) 
    { 
        List<string> diagonals = []; 
        int numRows = array.Length; 
        int numCols = array[0].Length; 
        // Major diagonals 
        for (int k = 0; k < numRows + numCols - 1; k++) 
        { 
            List<char> majorDiagonal = []; 
            for (int j = 0; j <= k; j++) 
            { 
                int i = k - j; if (i < numRows && j < numCols) 
                { 
                    majorDiagonal.Add(array[i][j]);
                } 
            } 
            if (majorDiagonal.Count > 0) 
            { 
                diagonals.Add(new string([.. majorDiagonal])); 
            } 
        } 
        // Minor diagonals 
        for (int k = 1 - numRows; k < numCols; k++) 
        { 
            List<char> minorDiagonal = []; 
            for (int j = 0; j < numRows; j++) 
            { 
                int i = j + k;
                if (i >= 0 && i < numCols) 
                { 
                    minorDiagonal.Add(array[j][i]);
                } 
            } 
            if (minorDiagonal.Count > 0) 
            { 
                diagonals.Add(new string([.. minorDiagonal]));
            } 
        } 
        return [.. diagonals];
    }
}