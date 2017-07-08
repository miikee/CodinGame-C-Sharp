using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Don't let the machines win. You are humanity's last hope...
 **/
class Player
{
    
    class Node
    {
        private int xPos;
        private int yPos;
        public Node(int x, int y)
        {
            xPos=x;
            yPos=y;
        }
        public int XPos
        {
            get{return xPos;}
        }
        public int YPos
        {
            get{return yPos;}
        }
        
       
    }
    static void Main(string[] args)
    {
        List<Node> nodes = new List<Node>(); // holds list of existing nodes
        int width = int.Parse(Console.ReadLine()); // the number of cells on the X axis
        int height = int.Parse(Console.ReadLine()); // the number of cells on the Y axis
        for (int i = 0; i < height; i++)
        {
            string line = Console.ReadLine(); // width characters, each either 0 or .
            for (int j = 0; j < width; j++)
            {
                line.ToCharArray();
                if(line[j]=='0')
                {
                    nodes.Add(new Node(j,i));  // create new node and add to list of nodes
                }
                    //enter x,y into array
                    //search array for x+1, y+1 
                  
            }
        }
        foreach(Node n in nodes)
        {
                int xPos=n.XPos;
                int yPos=n.YPos;
                int rxPos=-1; // right node position
                int ryPos=-1;
                int bxPos=-1; // bottom node position
                int byPos=-1;
                
                foreach(Node m in nodes)
                {
                    
                    // check for right neighbor and bottom neighbor
                    for(int incX=1; incX<=width; incX++)
                    {
                        if(m.XPos==xPos+incX && m.YPos==yPos)
                        {
                            
                            rxPos=m.XPos;
                            ryPos=m.YPos;
                            break;
                        }
                    }
                    if(rxPos>0) break;
                }
                foreach(Node m in nodes)
                {
                    for(int incY=1; incY<=height; incY++)
                    {
                        if(m.XPos==xPos && m.YPos==yPos+incY)
                        {
                            bxPos=m.XPos;
                            byPos=m.YPos;
                            break;
                        }
                    }
                    if(byPos>0)break;
                }
             
               Console.WriteLine("{0} {1} {2} {3} {4} {5}", xPos, yPos, rxPos, ryPos, bxPos, byPos);
        }
        // Write an action using Console.WriteLine()
        // To debug: Console.Error.WriteLine("Debug messages...");


        // Three coordinates: a node, its right neighbor, its bottom neighbor
        
    }
}
