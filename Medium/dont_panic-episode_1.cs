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
    public class Point
    {   
        public int Pos;
        public int Level;
        public Point(int level, int pos)
        {
            this.Pos=pos;
            this.Level=level;
        }
       
    }
    public class Clone
    {
        public int Pos;
        public int Level {get; private set;}
        public string Direction;
        public Clone(int level, int pos, string direction)
        {
            this.Pos=pos;
            this.Level=level;
            this.Direction=direction;
        }
        public void Block(List<Clone> blockList)
        {  // blocks the leadclone
            blockList.Add(this);
            Direction="NONE";
            Console.WriteLine("BLOCK");
           
        }
    }

    
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        
        // arena/game
        int nbFloors = int.Parse(inputs[0]); // number of floors
        int width = int.Parse(inputs[1]); // width of the area
        int nbRounds = int.Parse(inputs[2]); // maximum number of rounds
        
        // exit
        int exitFloor = int.Parse(inputs[3]); // floor on which the exit is found
        int exitPos = int.Parse(inputs[4]); // position of the exit on its floor
        Point exit=new Point(exitFloor, exitPos);
        
        // game setup
        int nbTotalClones = int.Parse(inputs[5]); // number of generated clones
        int nbAdditionalElevators = int.Parse(inputs[6]); // ignore (always zero)
        
        // elevators setup
        List<Point> elevatorsList = new List<Point>();
        int nbElevators = int.Parse(inputs[7]); // number of elevators
        for (int i = 0; i < nbElevators; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int elevatorFloor = int.Parse(inputs[0]); // floor on which this elevator is found
            int elevatorPos = int.Parse(inputs[1]); // position of the elevator on its floor
            elevatorsList.Add(new Point(elevatorFloor, elevatorPos));
        }

        // initialize clones list
        List<Clone> blockList = new List<Clone>();

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int cloneFloor = int.Parse(inputs[0]); // floor of the leading clone
            int clonePos = int.Parse(inputs[1]); // position of the leading clone on its floor
            string direction = inputs[2]; // direction of the leading clone: LEFT or RIGHT
            Clone leadClone=new Clone(cloneFloor, clonePos, direction);
            Console.Error.WriteLine("Floor: {0}   Pos: {1}   Direction: {2}", inputs[0], inputs[1], inputs[2] );
            
            
            // pick target as elevator or exit based on floor clone is on
            Point target=exit;
            if(leadClone.Level != target.Level)
            {
                foreach(Point elevator in elevatorsList.Where(x => x.Level==leadClone.Level))
                {
                    target=elevator;
                }
            }
            
            
            // If clone going in wrong direction, then "BLOCK"
            if(leadClone.Pos > target.Pos && leadClone.Direction=="RIGHT")
            {
                // "BLOCK", unless blocker ahead of leadClone on same level then "WAIT" 
                foreach(Clone blocker in blockList.Where(x => x.Level==leadClone.Level))
                {
                    if(leadClone.Pos < blocker.Pos)
                    {
                        Console.WriteLine("WAIT");
                        continue;
                    }
                    
                }
                leadClone.Block(blockList);
                
            }
            else if(leadClone.Pos < target.Pos && leadClone.Direction=="LEFT")
            {
                // "BLOCK", unless blocker ahead of leadClone on same level then "WAIT" 
                foreach(Clone blocker in blockList.Where(x => x.Level==leadClone.Level))
                {
                    if(leadClone.Pos > blocker.Pos)
                    {
                        Console.WriteLine("WAIT");
                        continue;
                    }
                    
                }
                leadClone.Block(blockList);
            }
            else
            {
                Console.WriteLine("WAIT");
            }
        }
    }
}
