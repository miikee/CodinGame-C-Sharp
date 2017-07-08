using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Node
{
    public int Id {get; private set;}
    public Dictionary<int, Node> ConnectedNodes {get; private set;}
    public int Rank {get; private set;}
    public bool Gateway {get; private set;}
    public bool Agent {get; private set;}
    
    public Node(int id)
    {
        this.Id = id;
        this.ConnectedNodes = new Dictionary<int, Node>();
        this.Rank = 0;
        this.Gateway = false;
        this.Agent = false;
    }
    
    public void AddConnectedNode(int NodeID, Node ConnectedNode)
    {
        this.ConnectedNodes.Add(NodeID, ConnectedNode);
    }
    
    public void RemoveConnectedNode(Node ConnectedNode)
    {
        this.ConnectedNodes.Remove(ConnectedNode.Id);
    }
    
    public void SetGateway()
    {
        this.Gateway = true;
    }
    
    public void SetRank()
    {
        if(this.Gateway)
        {   
            this.Rank = 5; 
        }
        else
        {
        
            this.Rank=this.ConnectedNodes.Where(x => x.Value.Gateway).Count();
           
            // Console.Error.WriteLine("ID: {0} Rank {1}", this.Id, this.Rank);
        }
    }
    
    public void SetAgent(bool Agent)
    {
        this.Agent = Agent;
    }
    
    public void CutGatewayLink()
    {
        Node LinkedGateway = this.ConnectedNodes.Where(x => x.Value.Gateway).Select(x => x.Value).First();
        this.RemoveConnectedNode(LinkedGateway);
        LinkedGateway.RemoveConnectedNode(this);
        Console.WriteLine(this.Id+" "+LinkedGateway.Id);
    }
        
}

class Player
{
   
    static void BuildNodesMap(int N1, int N2, ref Dictionary<int, Node> Nodes)
    {
        Node NodeN1;
        Node NodeN2;
        if(!Nodes.TryGetValue(N1, out NodeN1))
        {
            NodeN1 = new Node(N1);
            Nodes.Add(N1, NodeN1);
            
        }
        if(!Nodes.TryGetValue(N2, out NodeN2))
        {
            NodeN2 = new Node(N2);
            Nodes.Add(N2, NodeN2);
            
        }
        NodeN1.AddConnectedNode(N2, NodeN2);
        NodeN2.AddConnectedNode(N1, NodeN1);
    }
    
    static void BuildPaths(Node node, List<Node> PathHistory, ref List<List<Node>> Paths)
    {       
        if(node.Rank==0 || node.Agent)
        {
            PathHistory.Add(node);
            Paths.Add(PathHistory);
            return;
        }
        else if(PathHistory.Contains(node) || node.Gateway)
        {
            // looping back just delete the path or crossing over gateways
            return;
        }
        else
        {
            PathHistory.Add(node);
            foreach(var n in node.ConnectedNodes)
            {
                BuildPaths(n.Value, PathHistory.ToList(), ref Paths);
            }
        }
    }
    
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
        int L = int.Parse(inputs[1]); // the number of links
        int E = int.Parse(inputs[2]); // the number of exit gateways
        
        Dictionary<int, Node> Nodes = new Dictionary<int, Node>();
        for (int i = 0; i < L; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
            int N2 = int.Parse(inputs[1]);
            BuildNodesMap(N1, N2, ref Nodes);
            
           // Console.Error.WriteLine("{0} {1}", N1, N2);
        }
        // int[] Gateways = new int[E];
        for (int i = 0; i < E; i++)
        {
            int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
            Nodes[EI].SetGateway();
        }
        
        


        // game loop
        while (true)
        {
            foreach(var n in Nodes) n.Value.SetRank();
            
            int AgentID = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn        
            Node AgentNode = Nodes[AgentID];
            AgentNode.SetAgent(true);
            
            if(AgentNode.Rank==1)
            {
                AgentNode.CutGatewayLink();
            }
            else
            {
                
                /* find every rank 2 node
                * build paths from nodes with 1+ rank
                * end at rank 0 nodes
                * store these paths
                * cut path ending at agent, else shortest path
                */
                List<List<Node>> Paths = new List<List<Node>>();
                int TopRank=Nodes.Where(x => !x.Value.Gateway).Select(x => x.Value.Rank).Max();
                var HighRiskNodes = Nodes.Select(x => x.Value). Where(n => n.Rank==TopRank);
                
                
                foreach(Node HighRiskNode in HighRiskNodes)
                {
                    BuildPaths(HighRiskNode, new List<Node>(), ref Paths);
                }
                
                Node CutNode;
                var OrderedPaths = Paths.OrderBy(p => p.Count());
                
                var MostUrgentPath = OrderedPaths.First();
                
                var UrgentPaths = OrderedPaths.Where(p => p.Contains(AgentNode));

                if(UrgentPaths.Count()>0)
                {
                    MostUrgentPath = UrgentPaths.First();
                }
                CutNode = MostUrgentPath[0];

               
                Console.Error.WriteLine("Cut: "+CutNode.Id);
                CutNode.CutGatewayLink();
                AgentNode.SetAgent(false);
            }
                
            
        }

    }
}
