using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Solution
{
    static void Main(string[] args)
    {
        HashSet<string> stringList = new HashSet<string>();
        int N = int.Parse(Console.ReadLine());

        for (int i = 0; i < N; i++)
        {
            string line = Console.ReadLine();
            for(int j=line.Length; j>0; j--)
            {
                if(!stringList.Contains(line.Substring(0,j)))
                {
                    stringList.Add(line.Substring(0,j));
                }
                else
                {
                    break;
                }
            }
            
        }
        
        Console.WriteLine(stringList.Count());

    }
}
