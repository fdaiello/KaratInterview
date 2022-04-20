
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

        var wordList = new List<string>() { "catnip", "cccc", "s", "bit", "aoi", "ki", "aaoo", "ooo" };

        foreach ( var word in wordList)
        {
            Console.WriteLine(word);
            var coordList = FindCordinates(grid1, word);
            foreach (var coord in coordList)
            {
                Console.Write($"({coord[0]},{coord[1]})");
            }
            Console.WriteLine();

        }

        // Second Grid
        Console.WriteLine("Second Grid");

        char[][] grid2 = new[] { new[] { 'a' } };
        string word9 = "a";
        var coordList2 = FindCordinates(grid2, word9);
        foreach (var coord in coordList2)
        {
            Console.WriteLine(word9);
            Console.Write($"({coord[0]},{coord[1]})");
            Console.WriteLine();
        }


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
                    var tmpCoordList = new List<int[]>();
                    var finalCoordList = new List<int[]>();
                    DeepSearch(grid, new int[] { i, j }, word, ref tmpCoordList, ref finalCoordList);
                    if (finalCoordList.Count > 0)
                        return finalCoordList.ToArray();
                }
            }
        }

        return new int[0][];
    }
    static void DeepSearch(char[][] grid, int[] coord, string word, ref List<int[]> tempCoordList, ref List<int[]> finalCoordList)
    {
        // Add coord to list
        tempCoordList.Add(coord);

        // Check if word hasn't ended
        if ( word.Length > 1)
        {
            // Check if we havent reached last column, and then if char to the right matches
            if (coord[1] < grid[0].Length-1 && grid[coord[0]][coord[1] + 1] == word[1])
            {
                DeepSearch( grid, new int[] { coord[0], coord[1] + 1 }, word.Substring(1), ref tempCoordList, ref finalCoordList);
            }
            // Check if we havent reached last row, and then if char to the bottom matches
            if (coord[0] < grid.Length-1 && grid[coord[0] + 1][coord[1]] == word[1])
            {
                DeepSearch(grid, new int[] { coord[0]+1, coord[1] }, word.Substring(1), ref tempCoordList, ref finalCoordList);
            }
        }
        // Word has ended, return full list
        else
        {
            // clone tmpCoordList to finalCoordList
            finalCoordList = new List<int[]>(tempCoordList);
        }

        // Remove (last) coord from list
        tempCoordList.RemoveAt(tempCoordList.Count-1);

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

