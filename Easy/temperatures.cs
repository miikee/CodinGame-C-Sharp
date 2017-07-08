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
        int n = int.Parse(Console.ReadLine()); // the number of temperatures to analyse
        string[] strTemps = Console.ReadLine().Split(' '); // the n temperatures expressed as integers ranging from -273 to 5526
        
        
        int[] temps=new int[n];
        for(int j=0; j<n; j++){            
           temps[j]=Convert.ToInt32(strTemps[j]); 
        }
        
        int lowestTemp=int.MaxValue;;
        Console.Error.WriteLine(temps);
        if (n>0){
            for(int i=0; i<n; i++){
                if(Math.Abs(temps[i])<Math.Abs(lowestTemp)){
                    lowestTemp=temps[i];   
                }
                else if(Math.Abs(temps[i])==Math.Abs(lowestTemp)){
                    lowestTemp=(temps[i]>lowestTemp) ? temps[i] : lowestTemp;
                }
                else continue;
            }
        }
        else{lowestTemp=0;}
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");

        Console.WriteLine(lowestTemp);
    }
}
