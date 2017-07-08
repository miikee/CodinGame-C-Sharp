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
    static Queue<int> conway(Queue<int> input)
    {
        Queue<int> output=new Queue<int>();
        int count=0;
        
        int lead=input.Peek();
        
        while(input.Count>0)
        {
            int next=input.Dequeue();   
            if(next==lead)
            {
                count++;
            }
            else{
                output.Enqueue(count);
                output.Enqueue(lead);
                lead=next;
                count=1;
            }
        }
        
        // catch the last digits
        output.Enqueue(count);
        output.Enqueue(lead);
    
        return output;    
    }
    
    static void printQueue(Queue<int> queue)
    {
        string str=Convert.ToString(queue.Dequeue());
        foreach(int q in queue.ToArray())
        {
            str+=" "+q;
        }    
        Console.WriteLine(str);
    }
        
    static void Main(string[] args)
    {
        int R = int.Parse(Console.ReadLine());
        int L = int.Parse(Console.ReadLine());

        Queue<int> numbers = new Queue<int>();
        Queue<int> nextNumbers = new Queue<int>();
        
        numbers.Enqueue(R);
        for(int i=1; i<L; i++)
        {
            nextNumbers = conway(numbers);
            numbers = new Queue<int>(nextNumbers);
        }
        
        printQueue(numbers);
    }
}
