
/*

After catching your classroom students cheating before, you realize your students are getting craftier and hiding words in 2D grids of letters. The word may start anywhere in the grid, and consecutive letters can be either immediately below or immediately to the right of the previous letter.

Given a grid and a word, write a function that returns the location of the word in the grid as a list of coordinates. If there are multiple matches, return any one.

grid1 = [
    ['c', 'c', 't', 'i', 'b', 'x'],  
    ['c', 'c', 'a', 't', 'i', 't'],  
    ['a', 'c', 'n', 'n', 't', 't'],  
    ['t', 'n', 'i', 'i', 'p', 'p'],  
    ['a', 'o', 'o', 'o', 'a', 'a'],
    ['s', 'a', 'a', 'a', 'o', 'o'],
    ['k', 'a', 'i', 'o', 'k', 'i'],
]
word1 = "catnip"
word2 = "cccc"
word3 = "s"
word4 = "bit"
word5 = "aoi"
word6 = "ki"
word7 = "aaoo"
word8 = "ooo"

grid2 = [['a']]
word9 = "a"

find_word_location(grid1, word1) => [ (1, 1), (1, 2), (1, 3), (2, 3), (3, 3), (3, 4) ]
find_word_location(grid1, word2) =>
       [(0, 0), (1, 0), (1, 1), (2, 1)]
    OR [(0, 0), (0, 1), (1, 1), (2, 1)]
find_word_location(grid1, word3) => [(5, 0)]
find_word_location(grid1, word4) => [(0, 4), (1, 4), (2, 4)] OR [(0, 4), (1, 4), (1, 5)]
find_word_location(grid1, word5) => [(4, 5), (5, 5), (6, 5)]
find_word_location(grid1, word6) => [(6, 4), (6, 5)]
find_word_location(grid1, word7) => [(5, 2), (5, 3), (5, 4), (5, 5)]
find_word_location(grid1, word8) => [(4, 1), (4, 2), (4, 3)]
find_word_location(grid2, word9) => [(0, 0)]

Complexity analysis variables:

r = number of rows
c = number of columns
w = length of the word


O ( W*S )
O ( W*S )

*/

using System;
using System.Collections.Generic;
using System.Linq;

class Solution
{
    private static object currentCordiante;

    static void Main(String[] args)
    {

        char[][] grid1 = new[] {
            new []{'c', 'c', 't', 'i', 'b', 'x'},
            new []{'c', 'c', 'a', 't', 'i', 't'},
            new []{'a', 'c', 'n', 'n', 't', 't'},
            new []{'t', 'n', 'i', 'i', 'p', 'p'},
            new []{'a', 'o', 'o', 'o', 'a', 'a'},
            new []{'s', 'a', 'a', 'a', 'o', 'o'},
            new []{'k', 'a', 'i', 'o', 'k', 'i'},
        };

        string word0 = "cctib";
        string word1 = "catnip";
        string word2 = "cccc";
        string word3 = "s";
        string word4 = "bit";
        string word5 = "aoi";
        string word6 = "ki";
        string word7 = "aaoo";
        string word8 = "ooo";

        var coordList = FindCordinates(grid1, word0);
        foreach ( var coord in coordList)
        {
            Console.Write($"({coord[0]},{coord[1]})");
        }


        char[][] grid2 = new[] { new[] { 'a' } };
        string word9 = "a";

    }
    /*

    */
    static int[][] FindCordinates(char[][] grid, string word)
    {
        // For every possible start point of our grid
        for (int i = 0; i < grid.Length; i++)
        {
            for (int j = 0; j < grid[0].Length; j++)
            {
                // Check if position matches first word
                if ( grid[i][j] == word[0])
                {
                    var coordList = DeepSearch(grid, new int[] { i, j }, word);
                    if (coordList.Length > 0)
                        return coordList;
                }
            }
        }

        return new int[0][];
    }
    static int[][] DeepSearch(char[][] grid, int[] coord1, string word1)
    {
        // Stack
        var st = new Stack<(int[],string)>();
        st.Push((coord1, word1));

        int[] coord2;
        string word2;

        var coordList = new List<int[]>();

        while ( st.Any() )
        {
            var tuple = st.Pop();
            coord2 = tuple.Item1;
            word2 = tuple.Item2;

            coordList.Add(coord2);

            // Check if given word matches current position
            if (word2[0] == grid[coord2[0]][coord2[1]])
            {
                // Check if word hasn't ended
                if ( word2.Length > 1)
                {
                    // Check if we havent reached last column, and then if char to the right matches
                    if (coord2[1] < grid[0].Length-1 && grid[coord2[0]][coord2[1] + 1] == word2[1])
                    {
                        st.Push((new int[] { coord2[0], coord2[1] + 1 }, word2.Substring(1)));
                    }
                    // Check if we havent reached last row, and then if char to the bottom matches
                    if (coord2[0] < grid.Length-1 && grid[coord2[0] + 1][coord2[1]] == word2[1])
                    {
                        st.Push((new int[] { coord2[0]+1, coord2[1] }, word2.Substring(1)));
                    }
                }
                // reached end of word!
                else
                {
                    return coordList.ToArray();
                }
            }
        }

        return new int[0][];
    }

    static string? FindWord(string[] words, string s1)
    {
        // Create a map of input scrambled string s1
        var map = new Dictionary<char, int>();


        for (int i = 0; i < words.Length; i++)
        {
            // Here we have to initialize the map every time
            map = new Dictionary<char, int>();
            for (int j = 0; j < s1.Length; j++)
            {
                if (map.ContainsKey(s1[j]))
                    map[s1[j]]++;
                else
                    map.Add(s1[j], 1);
            }

            int matchedChar = 0;
            foreach (var c in words[i])
            {
                if (map.ContainsKey(c) && map[c] > 0)
                {
                    map[c]--;
                    matchedChar++;
                }
                else
                    break;
            }

            if (words[i].Length == matchedChar)
                return words[i];
        }

        return null;
    }
}

