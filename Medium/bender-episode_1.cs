using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 *  @ = Start - Always travels SOUTH
 *  $ = Suicide Booth -> EndPoint
 *  # = Obstacle -> Changes Direction (S, E, N, W) in order
 *  X = Obstacle -> Changes Direction (S, E, N, W) in order
 *  S, E, N, W = Modifiers -> change direction immediately
 *  I = Toggles Invertor -> reverse obstacle directions (W, N, E, S)
 *  B = Toggles Breaker Mode -> Can crash through X obstacle
 *                           -> B remains, X destroyed
 *  T = Teleport -> teleport to other T (2 per Map)
 *
 *  Output = List of moves (SOUTH, EAST, NORTH, WEST) or LOOP (when bender can't reach $)
 **/
class Solution
{
    class MapObject 
    {
        public int X {get; private set;}
        public int Y {get; private set;}
        public char Symbol {get; private set;}
        
        public MapObject(char symbol, int x, int y)
        {
            this.Symbol=symbol;
            this.X=x;
            this.Y=y;
            
        }
        public void Remove()
        {
            this.Symbol=' ';
        }
        
        public bool isObstacle(bool BeerMode)
        {
            if(this.Symbol=='#') return true;
            else if(this.Symbol=='X')
                if(BeerMode)
                {
                    this.Remove();
                    return false;
                }
                else return true;
            else return false;
        }
    }
    
    class Bender 
    {
        public int X {get; private set;}
        public int Y {get; private set;}
        public string Direction {get; private set;}
        public bool Beer {get; private set;}
        public bool Inverted {get; private set;}
        public bool Dead {get; private set;}
        public List<string> History {get; private set;}


        
        public Bender(List<MapObject> map)
        {
            MapObject start=map.Where(x => x.Symbol=='@').First();
            this.X=start.X;
            this.Y=start.Y;
            this.Direction="SOUTH";
            this.Beer = false;
            this.Inverted = false;
            this.Dead = false; // change once '$' reached
            this.History = new List<string>();
            // this.AddHistory(this.Direction);
        }
        
        public void AddHistory()
        {
            Console.Error.WriteLine(this.Direction);
            this.History.Add(this.Direction);
        }
        
        public void CheckLoop()
        {
            if(this.History.Count() > 200)
            {
                Console.WriteLine("LOOP");
                this.Dead=true;
            }
            
        }
     
        
        public void CheckNextSpace(List<MapObject> map)
        {
            // list of all the spaces surrounding bender
            List<Tuple<string, MapObject>> Spaces = new List<Tuple<string, MapObject>>{
                Tuple.Create("SOUTH", map.Where(x => x.X==this.X && x.Y==this.Y+1).First()),
                Tuple.Create("EAST", map.Where(x => x.X==this.X+1 && x.Y==this.Y).First()),
                Tuple.Create("NORTH", map.Where(x => x.X==this.X && x.Y==this.Y-1).First()),
                Tuple.Create("WEST", map.Where(x => x.X==this.X-1 && x.Y==this.Y).First()) 
            };
            
            if(this.Inverted) Spaces.Reverse();
            
            MapObject NextSpace = Spaces.Where(x => x.Item1==this.Direction).Select(x => x.Item2).First();
            if(NextSpace.isObstacle(this.Beer))
            {
                // Console.Error.WriteLine("IS OBSTACLE");
                foreach(Tuple<string, MapObject> s in Spaces)
                {
                    // Console.Error.WriteLine("CHECK: "+s.Item1+" "+s.Item2.Symbol);
                    if(!s.Item2.isObstacle(this.Beer))
                    { 
                        
                        this.Direction=s.Item1;  // change direction
                        // Console.Error.WriteLine("DIRECTION: "+this.Direction);
                        break;
                    }  
                }
            }

        }
        
        public void Move()
        {
            // move to next space depending on direction
            switch(this.Direction)
            {
                case "SOUTH": this.Y++; break;
                case "EAST": this.X++; break;
                case "NORTH": this.Y--; break;
                case "WEST": this.X--; break;
            }
            this.AddHistory();
            Console.Error.WriteLine("({0}, {1})", this.X, this.Y);
        }
        
        public void ResolveSpace(List<MapObject> map)
        {
            char symbol=map.Where(x => x.X==this.X && x.Y==this.Y).Select(x => x.Symbol).First();   
            switch(symbol)
            {
                case '$' : 
                    this.Dead = true;
                    this.PrintHistory();
                    break;
                // case '#' : this.Obstacle(map); break;
                // case 'X' : this.Obstacle(map); break;
                case 'S' : this.Direction = "SOUTH"; break; 
                case 'E' : this.Direction = "EAST"; break;
                case 'N' : this.Direction = "NORTH"; break;
                case 'W' : this.Direction = "WEST"; break;
                case 'I' : this.Inverted = !this.Inverted; break;
                case 'B' : this.Beer = !this.Beer; break;
                case 'T' : this.Teleport(map); break;
                default: break;
            }
        }

        private void Teleport(List<MapObject> map)
        {
            MapObject teleportTo = map.Where(x => x.Symbol=='T' && (x.X != this.X || x.Y != this.Y)).First();
            this.X = teleportTo.X;
            this.Y = teleportTo.Y;
        }
        
       
        
        private void PrintHistory()
        {
            Console.Error.WriteLine();
            Console.Error.WriteLine();
            foreach(string m in this.History){
                Console.WriteLine(m);
            }
        }
    }
    
    
    static void Main(string[] args)
    {
        List<string> moves = new List<string>();
        List<MapObject> map = new List<MapObject>();
        string[] inputs = Console.ReadLine().Split(' ');
        int L = int.Parse(inputs[0]); // Lines
        int C = int.Parse(inputs[1]); // Columns
        for (int i = 0; i < L; i++)
        {
           
            char[] row = Console.ReadLine().ToCharArray();
            for(int j=0; j < row.Length; j++)
            {
                map.Add(new MapObject(row[j], j, i));
            }
        }
             
        // output map layout for visualization
        for(int i=0; i<L; i++)
        {
            string mapline="";
            foreach(char s in map.Where(x => x.Y==i).Select(x => x.Symbol))
            {
                mapline+=s;
            }
            Console.Error.WriteLine(mapline);
        }
        
        // begin program 
        Bender bender = new Bender(map);
        Console.Error.WriteLine("START: "+bender.X+", "+bender.Y);
        

        while(!bender.Dead)
        {
            bender.ResolveSpace(map);

            bender.CheckNextSpace(map);
           
            bender.Move();
            bender.CheckLoop();           
        }   

    }
}
