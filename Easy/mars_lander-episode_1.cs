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
    class Lander
    {
        private int x, y;  // x,y coordinate of lander
        private int hSpeed; // the horizontal speed (in m/s), can be negative.
        private int vSpeed; // the vertical speed (in m/s), can be negative.
        private int fuel; // the quantity of remaining fuel in liters. 
        private int angle; // the rotation angle in degrees (-90 to 90).
        private int power; // the thrust power (0 to 4).
        
        public void update(int X, int Y, int HSpeed, int VSpeed, int Fuel, int Angle, int Power){
            x=X;
            y=Y;
            hSpeed=HSpeed;
            vSpeed=VSpeed;
            fuel=Fuel;
            angle=Angle;
            power=Power;
            
            int outputRotate=0;
            int outputPower=adjustPower(power, vSpeed);
            Console.WriteLine("{0} {1}",outputRotate, outputPower);
        }
        
        private int adjustPower(int power, int vSpeed){
            Console.Error.WriteLine("{0},{1}",power, vSpeed);    
            if(vSpeed<=-40) return (power==4)? power : power+1;
            else if(vSpeed>-40) return (power==0)? power : power-1;
            else return power;
        
        }
        
        /*
        private int adjustPower(int currentPower){
            double g=-3.711; //gravity of the planet
            double idealA=-1.6; //ideal acceleration towards planet 
            double currentA=g+currentPower; // currrent accleration
            
            //if falling to quick - up power
            if (currentA < idealA) return power+1;
            //if falling to slow - down power
            else if(currentA > idealA) return power-1;
            // do nothing
            else return power;
        
        }*/
        
        
    }
    class Point
    {   
        private int x;
        private int y;
        public Point(int X, int Y)
        {
            x=X;
            y=Y;
        }
        public int X{
            get{return x;}
        }
        public int Y{
            get{return y;}
        }
    }
    static void Main(string[] args)
    {

        string[] inputs;
        List<Point> pointsList=new List<Point>();
        int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
        for (int i = 0; i < surfaceN; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
            int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            pointsList.Add(new Point(landX, landY));
        }
        foreach(Point p in pointsList){
            Console.Error.WriteLine("{0}, {1}", p.X, p.Y);
        }
        
        Lander lander=new Lander();
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);
            int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            int rotate = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            int power = int.Parse(inputs[6]); // the thrust power (0 to 4).
            lander.update(X, Y, hSpeed, vSpeed, fuel, rotate, power);
            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");


            // 2 integers: rotate power. rotate is the desired rotation angle (should be 0 for level 1), power is the desired thrust power (0 to 4).
            //Console.WriteLine("0 3");
        }
    }
}
