using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 * ---
 * Hint: You can use the debug stream to print initialTX and initialTY, if Thor seems not follow your orders.
 **/
class Player
{
    class Character
    {
        private int x;
        private int y;
        
        public Character(int xPos, int yPos)
        {
            x=xPos;
            y=yPos;
        }    
        public int X
        {
            get{return x;}
            set{x=value;}
        }
        
        public int Y
        {   get{return y;} 
            set{y=value;}
        }
        public void updatePos(string direction)
        {
            if(direction.Contains("N")) y--;
            else if(direction.Contains("S")) y++;
            else if(direction.Contains("W")) x--;
            else if(direction.Contains("E")) x++;
            else return;
        }
    }
    class Target
    {
        private int x;
        private int y;
        
        public Target(int xPos, int yPos)
        {
            x=xPos;
            y=yPos;
        }    
        public int X
        {
            get{return x;}
            set{x=value;}
        }
        
        public int Y
        {   get{return y;} 
            set{y=value;}
        }
    }
    
    static string calculateX(int thorX, int lightX)
    {
        if(thorX>lightX) return "W";
        else if(thorX<lightX) return "E";
        else return "";
    }
    static string calculateY(int thorY, int lightY)
    {
        if(thorY>lightY) return "N";
        else if(thorY<lightY) return "S";
        else return "";
    }
    
    static void Main(string[] args)
    {
        // read the intial info
        string[] inputs = Console.ReadLine().Split(' ');
        int lightX = int.Parse(inputs[0]); // the X position of the light of power
        int lightY = int.Parse(inputs[1]); // the Y position of the light of power
        int initialTX = int.Parse(inputs[2]); // Thor's starting X position
        int initialTY = int.Parse(inputs[3]); // Thor's starting Y position
        
        // initialize thor and light objects
        Character thor=new Character(initialTX, initialTY);
        Target light=new Target(lightX, lightY);
        
       
        // game loop
        while (true)
        {
            int remainingTurns = int.Parse(Console.ReadLine()); // The remaining amount of turns Thor can move. Do not remove this line.
            string direction=calculateY(thor.Y, light.Y) + calculateX(thor.X, light.X);
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            
            thor.updatePos(direction); //update direction
            
            Console.Error.WriteLine(thor.X+" , "+thor.Y);
            // A single line providing the move to be made: N NE E SE S SW W or NW
            Console.WriteLine(direction);
        }
    }
}
