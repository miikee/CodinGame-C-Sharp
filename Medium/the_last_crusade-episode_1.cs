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
  
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // number of columns.
        int H = int.Parse(inputs[1]); // number of rows.
        
        int[,] roomPos = new int[H,W];
        for (int i = 0; i < H; i++)
        {
            string LINE = Console.ReadLine(); // represents a line in the grid and contains W integers. Each integer represents one room of a given type.
            string[] types = LINE.Split(' ');
            for(int w=0; w<W; w++)
            {
                roomPos[i,w]=int.Parse(types[w]);
            }
            
            Console.Error.WriteLine(LINE);
        }
        int EX = int.Parse(Console.ReadLine()); // the coordinate along the X axis of the exit (not useful for this first mission, but must be read).

        int[] DownRooms={1, 3, 7, 8, 9, 12, 13};
        

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int XI = int.Parse(inputs[0]);
            int YI = int.Parse(inputs[1]);
            string POS = inputs[2];  // TOP LEFT RIGHT
            
            int RoomType=roomPos[YI, XI];
            
              
            // Always Down:  1, 3, 7, 8, 9, 12, 13
            
            // Top->Left 4
            // Top->Right, 5
            // Top->Left: 10
            // Top->Right: 11
            
            // Left->Down: 4
            // Right->Down: 5
            // Continue through:  6,2
            
            // Down = y+1
            // Left = x-1
            // Right = x+1
            
            if(DownRooms.Contains(RoomType))
            {
                YI++; //Down
            }
            else if(POS=="TOP")
            {
                if(RoomType==5 || RoomType==11) XI++; // Right
                else XI--;  // Left
            }
            else if(POS=="LEFT")
            {
                if(RoomType==6 || RoomType==2) XI++; //Continue Right
                else YI++; // Down 
            }
            else
            {
                if(RoomType==6 || RoomType==2) XI--; // Continue LEFT
                else YI++; // Down
            }
            Console.Error.WriteLine(RoomType);
            Console.WriteLine("{0} {1}", XI, YI);

            // One line containing the X Y coordinates of the room in which you believe Indy will be on the next turn.
            // Console.WriteLine("0 0");
        }
    }
}
