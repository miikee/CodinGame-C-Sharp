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

    class Area
    {
        public int Width;
        public int Height;
        public Point point1;  // top-left corner
        public Point point2;  // bottom-right corner
    
        public Area(Point p1, Point p2)
        {
            this.point1=p1;
            this.point2=p2;
            this.Width=Math.Abs(p1.X-p2.X)+1;
            this.Height=Math.Abs(p2.Y-p2.Y)+1;
        }
        public Point MidPoint(){
            double midX=this.point1.X+((this.point2.X-this.point1.X)/2);
            double midY=this.point1.Y+((this.point2.Y-this.point1.Y)/2);
            Console.Error.WriteLine("midX, midY: ({0},{1})", midX, midY);
            
          
            return new Point((int)midX, (int)midY);
        }
        
        
        
    }  
    class Point
    {
        public int X;
        public int Y;
    
        public Point(int x, int y)
        {
            this.X=x;
            this.Y=y;
        }
    }
    
    
    
    static void Main(string[] args)
    {
        
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // width of the building.
        int H = int.Parse(inputs[1]); // height of the building.
        Area grid = new Area(new Point(0, 0), new Point(W-1, H-1));
        Area bombArea=grid;
        
        int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
        inputs = Console.ReadLine().Split(' ');
        int X0 = int.Parse(inputs[0]);
        int Y0 = int.Parse(inputs[1]);
        Point batman = new Point(X0, Y0);

        // game loop
        while (true)
        {
            string bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)
            int x1, y1, x2, y2;
            if(bombDir.Contains("U"))
            {
                y1=bombArea.point1.Y;
                y2=batman.Y;
            }
            else if(bombDir.Contains("D"))
            {
                y1=batman.Y+1;  // add 1 to correct for rounding
                y2=bombArea.point2.Y;
            }
            else
            {
                y1=batman.Y;
                y2=batman.Y;
                
            }
            
            if(bombDir.Contains("L"))
            {
                x1=bombArea.point1.X;
                x2=batman.X;
            }
            else if(bombDir.Contains("R"))
            {
                x1=batman.X+1;  // add 1 to correct for rounding
                x2=bombArea.point2.X;
            }
            else
            {
                x1=batman.X;
                x2=batman.X;
            }
            Console.Error.WriteLine("TopLeft:({0},{1}) : BottomRight:({2},{3})", x1, y1, x2, y2);
            bombArea=new Area(new Point(x1,y1), new Point(x2,y2));
            Point movePoint=bombArea.MidPoint();
            batman=new Point(movePoint.X, movePoint.Y);
            Console.WriteLine("{0} {1}", movePoint.X, movePoint.Y);
            
            
        }
    }
}
