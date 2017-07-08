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
    static int findMinPos(int[] values)
    {
        // return the position of the minimum in an array
        int min = int.MaxValue;
        int minPos=0;
        for(int pos=0; pos<values.Length; pos++)
        {
            if (values[pos]<min)
            {   
                min=values[pos];
                minPos=pos;
            }
        }
        return minPos;
    }
    
    static int findMaxPos(int[] values)
    {
        // return the position of the maximum in an array
        int max = int.MinValue;
        int maxPos=0;
        for(int pos=0; pos<values.Length; pos++)
        {
            if(values[pos]>max){
                max=values[pos];
                maxPos=pos;
            }
        }
        return maxPos;
    }
    
    static void Main(string[] args)
    {     
        int n = int.Parse(Console.ReadLine());
        int[] values = new int[n]; 
        List<int> pValues=new List<int>(); 
        
        string[] inputs = Console.ReadLine().Split(' ');
        for (int i = 0; i < n; i++)
        {
            values[i] = int.Parse(inputs[i]);
        }
        int arrayStart=0;
         // find the position of the first minimum 
        int minPos=findMinPos(values);
        while(arrayStart<=values.Length-1)
        {
            // find the maximum value that happened before the minimum value 
            int take=minPos+1;
            int[] subset=values.Skip(arrayStart).Take(take).ToArray();
            int maxSubsetPos=findMaxPos(subset);
        
            // subtract maximum value from the minimum value (always at the end of the subset array)
            // save this value to a list to be searched through for largest loss
            pValues.Add(subset[subset.Length-1] - subset[maxSubsetPos]);
          
            // set the start of the next array to where this subest finished+1
            arrayStart+=take;
            
            // find the next minPos for the next loop          
            minPos=findMinPos(values.Skip(arrayStart).ToArray());
        }

        // final p is the minimum of all the stored values
        int p=pValues.Min();
        Console.WriteLine(p);
        
    }
}
