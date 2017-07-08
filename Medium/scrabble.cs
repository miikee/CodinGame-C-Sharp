using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    static bool wordMatch(string w, List<char> letters)
    {
        char[] word=w.ToCharArray();
        foreach(char c in word)
        {
            
            if(letters.Contains(c))
            {
                letters.Remove(c);
            }
            else return false;
        }    
        return true;
    }
    
    static int scoreWord(string word)
    {
        List<Tuple<int, char[]>> scores = new List<Tuple<int, char[]>>()
        {
            Tuple.Create(1, new char[] {'e', 'a', 'i', 'o', 'n', 'r', 't', 'l', 's', 'u'}),
            Tuple.Create(2, new char[] {'d', 'g'}),
            Tuple.Create(3, new char[] {'b', 'c', 'm', 'p'}),
            Tuple.Create(4, new char[] {'f', 'h', 'v', 'w', 'y'}),
            Tuple.Create(5, new char[] {'k'}),
            Tuple.Create(8, new char[] {'j', 'x'}),
            Tuple.Create(10, new char[] {'q', 'z'}),
        };  // "which" score=4 4 1 3 4 = 16
        
        List<int> aggregateScore=new List<int>();
        foreach(char c in word.ToCharArray())
        {
            aggregateScore.Add(scores.Where(x => x.Item2.Contains(c)).Select(x => x.Item1).First());    
        }
        
        return aggregateScore.Sum();
    }
    
    static void Main(string[] args)
    {
        // build dictionary
        HashSet<string> dictionary=new HashSet<string>();
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {
            dictionary.Add(Console.ReadLine());
        }
        
                
        // build array of letters
        List<char> letters = new List<char>(Console.ReadLine().ToCharArray());

        // search and store dictionary for matches
        int highScore=0;
        string bestMatch="";
        foreach(string w in dictionary)
        {
            if(wordMatch(w, letters.ToList()))
            {
                //score the word match
                int score=scoreWord(w);
                if(score > highScore)
                {
                    highScore=score;
                    bestMatch=w;
                }
            }
        }
    

        Console.WriteLine(bestMatch);
    }
}
