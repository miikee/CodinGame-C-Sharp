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
    static void countNodes(int node, List<Tuple<int, int>> links, int count, ref int highestCount)
    {
        var linkedLinks = links.Where(x => x.Item1==node).ToList();
        
        if(linkedLinks.Count()==0)
        {
            //reached end, return count
            if(count>highestCount)
            {
                highestCount=count;
            }
        }    
        else
        {
            var linkedNodes = linkedLinks.Select(x => x.Item2).ToList();
            count++;
            foreach(int n in linkedNodes)
            {
                countNodes(n, links, count, ref highestCount);
            }
        }
    }
    static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine()); // the number of relationships of influence
        List<Tuple<int ,int>> links = new List<Tuple<int, int>>();

        for (int i = 0; i < n; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int x = int.Parse(inputs[0]); // a relationship of influence between two people (x influences y)
            int y = int.Parse(inputs[1]);
            links.Add(Tuple.Create(x, y));
        }
        
        int count=1;
        int highestCount=0;
        
        foreach( var node in links.Select(x => x.Item1))
        {
            countNodes(node, links, count, ref highestCount);  
        }

        Console.WriteLine(highestCount);
    }
}
