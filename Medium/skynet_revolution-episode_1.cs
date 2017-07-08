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
class Player
{
    class Link
    {
        public int Node1 {get; set;}
        public int Node2 {get; set;}
        public int Rank {get; set;}
        
        public Link(int N1, int N2)
        {
            this.Node1=N1;
            this.Node2=N2;
            this.Rank=0;
        }
        
        public void RankLink(List<Node> nodeList)
        {
            foreach(Node node in nodeList.OrderByDescending(x=>x.Rank).ToList())
            {  
                if((this.Node1==node.Id || this.Node2==node.Id) && node.Rank>0)
                {
                    this.Rank=node.Rank;
                    Console.Error.WriteLine("RankLink: {0}", node.Id);
                    
                }
            }
        }
    }
    class Node
    {
        public int Id {get; set;}
        public int Rank {get; set;}
        
        public Node(int id)
        {
            this.Id=id;
            this.Rank=0;
        }
        public void SetGateway()
        {
            this.Rank=1;
        }
    }

        

    
  static void Main(string[] args)
    {
        string[] inputs;
        
        // READING #Nodes, #links, #gateways
        inputs = Console.ReadLine().Split(' ');
        int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        int L = int.Parse(inputs[1]); // the number of links
        int E = int.Parse(inputs[2]); // the number of exit gateways
        
        List<Node> nodeList=new List<Node>();
        for(int i=0; i<N; i++)
        {
            nodeList.Add(new Node(i));
        }
        
        // READING LINKS
        List<Link> linkList=new List<Link>();
        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
            linkList.Add(new Link(N1, N2));
        }
        
        //READING GATEWAYS
        for (int i = 0; i < E; i++)
        {
            int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            List<Node> gatewayList=nodeList.Where(x=>x.Id==EI).ToList();
            foreach(Node node in gatewayList)
            {
                node.SetGateway();
            }
        }
            
        
        // map out the board
        // set ranks for all the nodes and links
        // while any nodes have unset ranks, loop this
        int currentRank=1;
       
        while(nodeList.Where(x => x.Rank==0).Count()>0)
        {
            // int to increment rank
            // select all nodes with the current set rank
            foreach(Node n in nodeList.Where(x => x.Rank==currentRank))
            {
                            
            //Console.WriteLine(linkList.Where(x => (x.Node1==n.Id || x.Node2==n.Id) && x.Rank==0).Count());

                // select all links that attach to a node with the current rank, and make sure rank hasn't already been set
                foreach(Link l in linkList.Where(x => (x.Node1==n.Id || x.Node2==n.Id) && x.Rank==0))
                {
                   // Console.WriteLine(nodeList.Where(x => x.Rank==currentRank).Count());
                    // foreach link set its rank equal to the node with the current rank
                    l.Rank=currentRank;
                    // find the second node that each link attaches to
                    int secondNode = l.Node1==n.Id ? l.Node2 : l.Node1;
                    // set the next set of node's rank, if rank not already set
                    foreach(Node n2 in nodeList.Where(x => x.Id==secondNode && x.Rank==0))
                    {
                        n2.Rank=currentRank+1;
                    }

                }
            }
            // increment rank for next iteration
            currentRank++;
           // Console.Error.WriteLine("CurrentRank: {0}", currentRank);
        }
        
        // CHECK the board is properly ranked
        foreach(Node n in nodeList)
        {
            Console.Error.WriteLine("Check Board:");
            Console.Error.WriteLine("Node: {0} Rank: {1}", n.Id, n.Rank);
        }

        // game loop
        while (true)
        {
            // The index of the node on which the Skynet agent is positioned this turn
            int agentNode = int.Parse(Console.ReadLine()); 
            
            // find every node that touches the agent node and cut the one with the lowest rank
            int lowestRank=Int32.MaxValue;
            int node1=0;
            int node2=0;
            foreach(Link l in linkList.Where(x => x.Node1==agentNode || x.Node2==agentNode))
            {
                if(l.Rank<lowestRank) 
                {
                    lowestRank=l.Rank;
                    node1=l.Node1;
                    node2=l.Node2;
                }
            }
            
            Console.WriteLine("{0} {1}", node1, node2);
        }
    }
}
        
