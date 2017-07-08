using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


class Solution
{
    class Node
    {
        public int Id {get; private set;}
        public List<Node> AdjNodes {get; private set;}
        
        public Node(int id)
        {
            this.Id=id;
            this.AdjNodes = new List<Node>();
        }
        
        public void AddAdj(Node adj)
        {
            this.AdjNodes.Add(adj);
        }
        
        public void RemoveAdj(Node adj)
        {
            this.AdjNodes.Remove(adj);
        }
        
        public bool IsEnd()
        {
            return AdjNodes.Count() <=1;
        }
    }
    
    static void AddNode(Dictionary<int, Node> nodesList, int id)
    {
        Node node;
        if(!nodesList.TryGetValue(id, out node))
        {
            node=new Node(id);
            nodesList.Add(id, node);
        }
    }
    
    static void Main(string[] args)
    {
        Dictionary<int, Node> nodesList=new Dictionary<int, Node>();
        
        int n = int.Parse(Console.ReadLine()); // the number of adjacency relations
        for (int i = 0; i < n; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            int xi = int.Parse(inputs[0]); // the ID of a person which is adjacent to yi
            int yi = int.Parse(inputs[1]); // the ID of a person which is adjacent to xi
            
            AddNode(nodesList, xi);
            AddNode(nodesList, yi);
            nodesList[xi].AddAdj(nodesList[yi]);
            nodesList[yi].AddAdj(nodesList[xi]);

        }
        
        int count=0;
        while(nodesList.Count() > 1)
        {
            List<Node> endsList = nodesList.Where(x => x.Value.IsEnd()).Select(x => x.Value).ToList();
            count++;
            foreach(Node removalNode in endsList)
            {
                foreach(Node linkedNode in removalNode.AdjNodes)
                {
                    linkedNode.RemoveAdj(removalNode);  // remove existance of node from linked node's list of AdjNodes
                }
                nodesList.Remove(removalNode.Id);  // finally remove the node entirely
            }
        }
        
        Console.WriteLine(count);
    }
}
      

