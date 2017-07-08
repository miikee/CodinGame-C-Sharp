using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

class Solution
{
    static void Main(string[] args)
    {
        Dictionary<int, int> Line = new Dictionary<int, int>();
        Dictionary<int, int> Ride = new Dictionary<int, int>();
        
        Dictionary<int, Tuple<int, double>> Memos = new Dictionary<int, Tuple<int, double>>();
        //HashSet<int> CashBox = new HashSet<int>();
        
        string[] inputs = Console.ReadLine().Split(' ');
        int Seats = int.Parse(inputs[0]);
        int Rounds = int.Parse(inputs[1]);
        int Groups = int.Parse(inputs[2]);
      
        Console.Error.WriteLine("{0} {1} {2}", Seats, Rounds, Groups);
        Console.Error.WriteLine();
      
        for (int i = 0; i < Groups; i++)
        {
            Line.Add(i,int.Parse(Console.ReadLine()));
        }
        
  
        int StartGroup=0;
        double Sum=0;
        double RideSum=0;
        bool RideFull = false;
        while(Rounds > 0)
        {
            // try looking up info
            if(Memos.ContainsKey(StartGroup))
            {
                
                // Make one full cycle of the line & remember its income
                double LineCycleIncome = Memos[StartGroup].Item2;
                int NextGroup = Memos[StartGroup].Item1;
                int RoundsPerLineCycle = 1;
                while(NextGroup != StartGroup)
                {
                   LineCycleIncome += Memos[NextGroup].Item2;
                   NextGroup = Memos[NextGroup].Item1;
                   RoundsPerLineCycle++;
                }
                
                // Each full cycle through the line results in the same income amount
                int NumberOfLineCycles=Rounds/RoundsPerLineCycle;
                Rounds=Rounds%RoundsPerLineCycle;
                Sum+=LineCycleIncome*(NumberOfLineCycles);
                
                // find any remainder of the line if line doesn't make a full cycle
                while(Rounds > 0)
                {
                    Tuple<int, double> Result=Memos[StartGroup];  
                    Sum += Result.Item2;
                    StartGroup = Result.Item1;
                    
                    Rounds--;
                }

            }
            // manually add riders
            else
            {
                int FrontGroup=StartGroup;  // first group loaded -> IDs the memo
                while(!RideFull)
                {
                    if(Line[FrontGroup] + RideSum <= Seats)
                    {
                        RideSum+=Line[FrontGroup];
                        // loop back through at end of line
                        if(FrontGroup==Line.Count()-1)
                        {
                            // check if entire line loaded onto single ride
                            if(StartGroup==0) RideFull=true;
                            FrontGroup=0;
                        }
                        else{
                            FrontGroup++;
                        }
                    }
                    else
                    {
                        RideFull = true;
                    }
                }
                
                // create a memo containing StartGroup, Tuple(FrontGroup, RideSum)
                // FrontGroup = next group at the front of the line to get on
                Tuple<int, double> Results = Tuple.Create(FrontGroup, (double)RideSum);
                Memos.Add(StartGroup, Results);
                
                Console.Error.WriteLine("ADD MEMO: {0} => {1}, {2} ", StartGroup, FrontGroup, RideSum);
                StartGroup=FrontGroup; // last group not added becomes the new front group

            }

            Sum+=RideSum;
            RideSum = 0;
            RideFull = false;
            Rounds--;
        }

        Console.WriteLine(Sum);
    }
}
