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
    class Point
    {
        public int X;
        public int Y;
        public Point(int x, int y)
        {
            this.X=x;
            this.Y=y;
        }
        
/*
        {
            int diffPointX = point1.X-point2.X;
            int diffPointY = point1.Y-point2.Y;
            
            return new Point(diffPointX, diffPointY);
        }*/
    }
    class Lander
    {
        public int X;
        public int Y;
        public double HSpeed;
        public double VSpeed;
        public int Fuel;
        public double Rotate;
        public int Power;
        public double AccelV; // vertical acceleration
        public double AccelH; // horizontal acceleration
        
        public Lander(int x, int y, double hSpeed, double vSpeed, int fuel, double rotate, int power)
        {
            this.X=x;
            this.Y=y;
            this.HSpeed=hSpeed; // the horizontal speed (in m/s), can be negative.
            this.VSpeed=vSpeed; // the vertical speed (in m/s), can be negative.
            this.Fuel=fuel; // the quantity of remaining fuel in liters.
            this.Rotate=rotate; // the rotation angle in degrees (-90 to 90).
            this.Power=power; // the thrust power (0 to 4).
            getAccel();
        }
        
        
        public void getAccel()
        {
            double gravity=-3.711;
            int power=this.Power;
            double rotate=this.Rotate;
            this.AccelV=gravity+power*Math.Cos(rotate*Math.PI/180);
            this.AccelH=power*-Math.Sin(rotate*Math.PI/180);
        }
        
        public double rotateLeft(){
            double rotate=this.Rotate+15;
            if(rotate>90) rotate=90;
            return rotate;
        }
        public double rotateRight(){
            double rotate=this.Rotate-15;
            if(rotate<-90) rotate=-90;
            return rotate;
        }
        public int addPower(){
            int power=this.Power+1;
            if(power>4) power=4;
            return power;
        }
        public int remPower(){
            int power=this.Power-1;
            if(power<0) power=0;
            return power;
        }
        
        
        public Point calculateXY(double rotate, int power, double HSpeedMax, double VSpeedMax){
            double newAccelH=(power*-Math.Sin(rotate*Math.PI/180));
            double newAccelV=-3.711+(power*Math.Cos(rotate*Math.PI/180));
            
            double X, Y;
            
            if(newAccelH==0){ X=this.X;}
            else{ X=this.X+( (Math.Pow(HSpeedMax,2)-Math.Pow(this.HSpeed,2)) / (2*newAccelH) );}
            if(newAccelV==0){ Y=this.Y;}
            else{ Y=this.Y+( (Math.Pow(VSpeedMax,2)-Math.Pow(this.VSpeed,2)) / (2*newAccelV) ); } 
            
            Console.Error.WriteLine("rotate:{0}\tpower:{1}", rotate, power);
            Console.Error.WriteLine("AccelH:{0}\tAccelV:{1}", Math.Round(newAccelH, 2), Math.Round(newAccelV, 2));
            Console.Error.WriteLine("X:{0}\tY:{1}\n\r", X, Y);

            
            return new Point ((int)X, (int)Y);
        }
        
        /*
        public void findBestRoute(double accelHIdeal, double accelVIdeal)
        {
            double bestRotate=0;
            double bestPower=0;
            double bestScore=10000;
            
            // all angles
            for (int rotate=-90; rotate<=90; rotate=rotate+15)
            {
                // all powers
                for(int power=0; power<=4; power++)
                {
                    // score current iteration of power & rotate
                    double accelH=this.AccelH+power*-Math.Sin(rotate*(Math.PI/180));
                    double accelV=this.AccelV+power*Math.Cos(rotate*(Math.PI/180));
                    double scoreH=Math.Abs(accelH-accelHIdeal); 
                    double scoreV=Math.Abs(accelV-accelVIdeal);
                    double score=scoreH+scoreV;
                    //if (score<0) continue;  // only select accelerations > than the ideal Acceleration
                    
                   // Console.Error.WriteLine("{0} {1} => {2} + {3} :: {4} + {5}", rotate, power, accelH, accelV, accelHIdeal, accelVIdeal);
                   //Console.Error.WriteLine("{0} {1} =>\t {3} - {2} = {4}  =>\t {5} ", rotate, power, Math.Round(accelH, 2), Math.Round(accelHIdeal, 2), Math.Round(scoreH, 2), Math.Round(score,2));
                    
                    // compare the score and take the closest match
                    if(score < bestScore)
                    {
                        Console.Error.WriteLine("{0} < {1}", score, bestScore);
                        Console.Error.WriteLine("BEST:  {0} {1} => {2}", rotate, power, Math.Round(score,2));
                        bestRotate=rotate;
                        bestPower=power;
                        bestScore=score;
                    }
                }
            }
            Console.WriteLine("{0} {1}", bestRotate, bestPower);
            
            
        }
        */
        
        public void goLeft(double AccelV)
        {
            double g=-3.711;
            int power=4;
            double rotate=Math.Acos((AccelV-g)/4);  // rotation to set for accelV=0;  
            rotate=Convert.ToInt32(rotate*180/Math.PI);  // convert to degrees 
            if(rotate>90) rotate=90;
            Console.WriteLine("{0} {1}", rotate, power);  
        }
        
        public void goRight(double AccelV)
        {
            double g=-3.711;
            int power=4;
            double rotate=Math.Acos((AccelV-g)/4);  // rotation to set for accelV=0;  
            rotate=Convert.ToInt32(rotate*180/Math.PI);  // convert to degrees
            if(rotate>90) rotate=90;
            Console.WriteLine("{0} {1}", -rotate, power); //negative of rotate for right
        }
    }

    public static double calculateAccel(double speedFinal, double speedInitial, double distance)
    {
        double accel=(Math.Pow(speedFinal,2)-Math.Pow(speedInitial,2))/(2*distance);
        return accel;
    }
    
  
    
    static void Main(string[] args)
    {

        string[] inputs;
        int surfaceN = int.Parse(Console.ReadLine()); // the number of points used to draw the surface of Mars.
        
        // find the landing site
        Point landingStart=new Point(0,0), landingEnd=new Point(0,0);
        int landX0=0, landY0=0;
        for (int i = 0; i < surfaceN; i++)
        {
            inputs = Console.ReadLine().Split(' ');
            int landX = int.Parse(inputs[0]); // X coordinate of a surface point. (0 to 6999)
            int landY = int.Parse(inputs[1]); // Y coordinate of a surface point. By linking all the points together in a sequential fashion, you form the surface of Mars.
            
            // if no change in Y then set as new landingzone
            if(landY0==landY)
            {
                landingStart=new Point(landX0,landY0);
                landingEnd=new Point(landX,landY);
            }    
            landX0=landX;
            landY0=landY;
        }
        
        Console.Error.WriteLine("Landing Area: {0} - {1}", landingStart.X, landingEnd.X);
        
        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int X = int.Parse(inputs[0]);
            int Y = int.Parse(inputs[1]);
            int hSpeed = int.Parse(inputs[2]); // the horizontal speed (in m/s), can be negative.
            int vSpeed = int.Parse(inputs[3]); // the vertical speed (in m/s), can be negative.
            int fuel = int.Parse(inputs[4]); // the quantity of remaining fuel in liters.
            double rotatation = int.Parse(inputs[5]); // the rotation angle in degrees (-90 to 90).
            int thrust = int.Parse(inputs[6]); // the thrust power (0 to 4).

             // create the lander
            Lander lander= new Lander(X, Y, hSpeed, vSpeed, fuel, rotatation, thrust);
            
            Point landing=new Point((landingEnd.X-landingStart.X)/2, landingStart.Y+2000);  // the ideal landing spot midway into the flat area
            
            
            double g=-3.711;
            int power=0;
            double rotate=0;
            double AccelV=0;  // allowable vertical accel
          
            if((lander.Y-landingStart.Y)>1000) AccelV=-0.5; 
        
            // move lander to the landing position
            if(lander.X < landingStart.X)
            {
                if(lander.HSpeed<50) lander.goRight(AccelV);
                else lander.goLeft(AccelV);
                
            }
             
            // move the lander to the starting position   
            else if(lander.X > landingEnd.X)
            {
                
                if(lander.HSpeed>-50) lander.goLeft(AccelV);
                else lander.goRight(AccelV);
            }
            else
            {
                if(lander.HSpeed<-1) lander.goRight(AccelV);
                else if(lander.HSpeed>1) lander.goLeft(AccelV);
                else{
                    rotate=0;
                    if(lander.VSpeed<=-40) power=4;
                    else power=3;
                    Console.WriteLine("{0} {1}", rotate, power);
                }
            }
           
        
        
        /*
            double bestRotate=0;
            double bestPower=0;
            double bestScore=100000;
            
            // all angles
            for (int rotate=-90; rotate<=90; rotate=rotate+15)
            {
                // all powers
                for(int power=0; power<=4; power++)
                {
                    //find the landing area for each combination
                    int HSpeedMax=40;
                    int VSpeedMax=-20;
                    Point estimatedlanding = lander.calculateXY(rotate, power, HSpeedMax, VSpeedMax);
                    double score=Math.Sqrt(Math.Pow(landing.X-estimatedlanding.X,2) + Math.Pow(landing.Y-estimatedlanding.Y, 2));
                    
                    // Console.Error.WriteLine("{0} {1} => {2}", estimatedlanding.X, estimatedlanding.Y, Math.Round(score,2)); 
                    
                
                    // compare the score and take the closest match
                    if(score < bestScore)
                    {
                        //Console.Error.WriteLine("{0} < {1}", score, bestScore);
                        // Console.Error.WriteLine("BEST:  {0} {1} => {2}", rotate, power, Math.Round(score,2));
                        bestRotate=rotate;
                        bestPower=power;
                        bestScore=score;
                    }
                }
            }
            Console.WriteLine("{0} {1}", bestRotate, bestPower);
            
            */
       /*
            // calculate ideal acceleration
            // a=[(v)^2-(v0)^2]/2d
            double HDistance, VDistance, HSpeedIdeal, VSpeedIdeal;
            
            // lander to the left of landing zone
            if(lander.X < landingStart.X)
            {
                HDistance=landingStart.X-lander.X;
                VDistance=lander.Y-landingStart.Y+500;
                VSpeedIdeal=-39;
                HSpeedIdeal=40;
                Console.Error.WriteLine("1");
                
            }
            // lander to the right of the landing zone
            else if(lander.X > landingEnd.X)
            {
                HDistance=landingEnd.X-lander.X;
                VDistance=landingStart.Y-lander.Y;
                VSpeedIdeal=0;
                HSpeedIdeal=-40;
            }
            // lander within the landing zone
            else
            {   
                HDistance=1;
                VDistance=1;
                HSpeedIdeal=0;
                VSpeedIdeal=0;
                
                if(lander.HSpeed<20){
                  
                    HDistance=1;
                    VDistance=lander.Y-landingStart.Y;
                    VSpeedIdeal=-39;
                    HSpeedIdeal=0;
                }
            }
            
            
            
            double accelHIdeal=calculateAccel(HSpeedIdeal, lander.HSpeed, HDistance);
            double accelVIdeal=calculateAccel(VSpeedIdeal, lander.VSpeed, VDistance);
            
            Console.Error.WriteLine("H:{0}  V:{1}", accelHIdeal, accelVIdeal);
            lander.findBestRoute(accelHIdeal, accelVIdeal);
        
            
               
            
            /*
            // hover the lander and move to the landing zone
            if(lander.X < landingStart.X)
            {
                if(lander.VSpeed < -30)
                {
                    rotate=0;
                    power=4;
                }
                else{
                    rotate=lander.Rotate-15;
                    if(rotate<-90) rotate=-90;
                    power=4;
                }
            
            }
            // lander to the right of the landing zone
            else if(lander.X > landingEnd.X)
            {
               
            }
            // lander within the landing zone
            else
            {   
                if(lander.Rotate==0 && Math.Abs(lander.HSpeed)<20)
                {
                    rotate=0;
                    if(lander.VSpeed>=40) power=4;
                    else power=lander.Power-1;
                }
                else if(lander.HSpeed>20)
                {
                    if(lander.VSpeed < -40)
                    {
                        rotate=0;
                        power=4;
                    }
                    else{
                        rotate=lander.Rotate+15;
                        if (rotate>90) rotate=90;
                        power=4;
                    }
                }
            }
           
           Console.WriteLine("{0} {1}", rotate, power);
           
           
           */
            
        }
    }
}
