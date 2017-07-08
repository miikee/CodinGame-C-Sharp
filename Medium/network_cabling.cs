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
        int N = int.Parse(Console.ReadLine());
        
        List<double> xPos = new List<double>();
        List<double> yPos = new List<double>();
        
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            xPos.Add(int.Parse(inputs[0]));
            yPos.Add(int.Parse(inputs[1]));
        }
        
        yPos=yPos.OrderBy(x => x).ToList();        

        double WireXLength=(xPos.Max()-xPos.Min());
        double median=0;
        
        // even
        if(N%2==0)
        {
            median=(yPos[N/2] + yPos[(N/2)-1])/2;
            
        }
        else
        {
            median=yPos[(N/2)];
        }    
        
        double Total=yPos.Select(a => Math.Abs(median-a)).Sum();
        Total+=WireXLength;

        Console.WriteLine(Total);      
    }
}
