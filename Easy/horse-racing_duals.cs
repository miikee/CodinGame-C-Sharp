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
        List<int> list = new List<int>();
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {
            int pi = int.Parse(Console.ReadLine());
            // Console.Error.WriteLine("N: {0}", N);
            list.Add(pi);
        }
       
        var sydneyList=list.OrderBy(x => x).ToList();
        
       int minDiff=int.MaxValue;
       
        for(int i=0; i<sydneyList.Count()-1; i++){
            int diff=sydneyList[i+1]-sydneyList[i];
            if(diff<minDiff){ minDiff=diff;}
        }

        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(minDiff);
    }
}
