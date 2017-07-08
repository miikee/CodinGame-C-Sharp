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
class Solution
{
    class User{
        public double lon {private set; get;}
        public double lat {private set; get;}
        
        public User(string lon, string lat){
            this.lon=Convert.ToDouble(lon)*Math.PI/180;
            this.lat=Convert.ToDouble(lat)*Math.PI/180;
        }
        
        public Defib findClosestDefib(List<Defib> dList){
            Dictionary<Defib, double> distList=new Dictionary<Defib, double>();
        
            Defib closestDefib;
            double distance;
            foreach(Defib d in dList){
                distance=distBetween(this.lon, this.lat, d.lon, d.lat);
                distList.Add(d, distance);
            }
            
            closestDefib=distList.Aggregate((x, y)=>(x.Value < y.Value) ? x : y).Key;
            return closestDefib;
        }
      
    }
    
    class Defib{
        double id;
        public string name {get; private set;}
        string address;
        string phone;
        public double lon{get; private set;}
        public double lat{get; private set;}
        
        // set defib properties
        public Defib(string defibString){
        
            string[] defib=defibString.Replace(',', '.').Split(';');
           
            this.id=Convert.ToDouble(defib[0]);
            this.name=defib[1];
            this.address=defib[2];
            this.phone=(String.IsNullOrWhiteSpace(defib[3])) ? Convert.ToString(null) : Convert.ToString(defib[3]);
            this.lon=Convert.ToDouble(defib[4])*Math.PI/180;
            this.lat=Convert.ToDouble(defib[5])*Math.PI/180;
            
       
        }
            
        
    }
    
    //calculate distance between user and defib
    public static double distBetween(double lonA, double latA, double lonB, double latB){
        double xPos = (lonB - lonA) * Math.Cos((latA+latB)/2);
        double yPos = latB - latA;
        double distance = Math.Sqrt(Math.Pow(xPos, 2)+Math.Pow(yPos, 2))*6371;
        
        return distance;
    }
    
    static void Main(string[] args)
    {
        
        string LON = Console.ReadLine().Replace(',','.');
        string LAT = Console.ReadLine().Replace(',', '.');
        User user=new User(LON, LAT);
        int N = int.Parse(Console.ReadLine());
        
        List<Defib> defibsList=new List<Defib>();  //list of Defib objects
        for (int i = 0; i < N; i++)
        {
            // add all defibs to list
            defibsList.Add(new Defib(Console.ReadLine()));    
        }

        // find the closest defib object
        Defib closestDefib=user.findClosestDefib(defibsList);
        
        Console.WriteLine(closestDefib.name);
    }
}
