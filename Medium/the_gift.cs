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
    /*
    static int divideRemainder(List<int> remainingPayers, int price)
    {
        
    }
    */
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        int price = int.Parse(Console.ReadLine());
        
        List<int> budget = new List<int>();
        for (int i = 0; i < N; i++)
        {
            budget.Add(int.Parse(Console.ReadLine()));
        }
        Console.Error.WriteLine("Price: {0} \t N: {1}\r\nBudgets:", price, N );
        foreach(int b in budget)
        {
            Console.Error.WriteLine("\t"+b);
        }
        
        
        int sum = budget.Sum();
        if(sum-price < 0)
        {   
            Console.WriteLine("IMPOSSIBLE");
            return;
        }
         
        List<int> answer = new List<int>();
        int count=budget.Count();
        while(count > 0)
        {
            // remove minimum
            int payment=budget.Min();
            budget.Remove(payment);
            int currentTotal=(payment*count)+answer.Sum();
            
            Console.Error.WriteLine("Count: "+count);
            if(currentTotal > price)
            {
                // can pay -> now divide up the remainder
                int remainder = currentTotal-price;
                int fractionLeft = remainder%count;
                payment=payment-(remainder/count);
                
                Console.Error.WriteLine("payment: {0}  remainder: {1}  fractionLeft: {2}  count: {3}", payment, remainder, fractionLeft, count);
                
                while(count > 0)
                {
                    if(fractionLeft > 0)
                    {
                        answer.Add(payment-1);
                        fractionLeft--;
                    }
                    else
                    {
                        answer.Add(payment);
                    }
                    count--;
                }
                
            }
            else
            {
                // can't pay yet, add current minimum payer to answer budget
                answer.Add(payment);
                count--;
            }
        }
        
        answer.OrderBy(x => x);
        foreach(int a in answer)
        {
            Console.WriteLine(a);
        }
    }
}
