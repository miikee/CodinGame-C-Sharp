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
    static void Main(string[] args)
    {
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?";
       
        
        int L = int.Parse(Console.ReadLine()); // letter width
        int H = int.Parse(Console.ReadLine()); // letter height
        string T = Console.ReadLine();  // string to write
        
        // create list to hold character position in alphabet
        List<int> charPosList=new List<int>();
        foreach(char c in T.ToUpper().ToCharArray()){
            int charPos=alphabet.IndexOf(c);
            if (charPos==-1) charPos=26;
            charPosList.Add(charPos);
        }

        for (int i = 0; i < H; i++)
        {
            string ROW = Console.ReadLine(); // read row of ASCII 
            string outputRow=""; // initialize ouput row of ASCII
             
            foreach (int pos in charPosList){
               // Console.Error.WriteLine(pos);
                
                int startCol=L*pos; // position*letter widths
                int endCol=L; // start posititon+letter width
                
                outputRow+=ROW.Substring(startCol, endCol); // add to the row
            }
            Console.WriteLine(outputRow);
        }
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        //Console.WriteLine("answer");
    }
}
