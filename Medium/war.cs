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
    // takes a card string and returns the value of the card
    static int cardValue(string card)
    {
        Dictionary<char, int> facecards=new Dictionary<char, int>();  // convert facecards to numerical digits
        facecards.Add('J', 11);
        facecards.Add('Q', 12);
        facecards.Add('K', 13);
        facecards.Add('A', 14);
        
        int intcard;
        
        if(facecards.ContainsKey(card[0]))
        {
            intcard=facecards[card[0]];
          
        }
        else
        {
           
            intcard=int.Parse(card.ToString().TrimEnd(new char[] {'H', 'D', 'C', 'S'}));
            
        }
         
        return intcard;            
    }
    
    static int playCards(int card1, int card2){
        if(card1>card2)
        {
            return 1;
        }
        if(card1<card2)
        {
            return 2;
        }
        else{
            return 0;
        }
    }
    static void Main(string[] args)
    {
        Queue<int> deck1=new Queue<int>();  // player 1 deck
        Queue<int> deck2=new Queue<int>();  // player 2 deck
        Queue<int> hand1=new Queue<int>();  // player 1 cards in play
        Queue<int> hand2=new Queue<int>();  // player 2 cards in play
        int card1;  // competing card for player1
        int card2;  // competing card for player2
        int roundnumber=0;  // number of rounds
       

        int n = int.Parse(Console.ReadLine()); // the number of cards for player 1
        for (int i = 0; i < n; i++)
        {
            string cardp1 = Console.ReadLine(); // the n cards of player 1
           // Console.Error.WriteLine("P1: {0}", cardp1);
            deck1.Enqueue(cardValue(cardp1));  // build player 1 deck

        }
        int m = int.Parse(Console.ReadLine()); // the number of cards for player 2
        for (int i = 0; i < m; i++)
        {
            string cardp2 = Console.ReadLine(); // the m cards of player 2
            //Console.Error.WriteLine("P2: {0}", cardp2);
            deck2.Enqueue(cardValue(cardp2));  // build player 2 deck
        }
        
        // when dealt, the cards are placed face down on the deck, so reverse the queues
       // deck1=new Queue<int>(deck1.Reverse());
        //deck2=new Queue<int>(deck2.Reverse());
        
        // play the game
        while(true){
            
            Console.Error.WriteLine("{0} {1}", deck1.Count, deck2.Count);
            card1=deck1.Dequeue();
            card2=deck2.Dequeue();
            int winner=playCards(card1, card2);
            Console.Error.WriteLine("P1:{0}  P2:{1}  =>  {2} : {3}", card1, card2, winner, roundnumber);
            
            hand1.Enqueue(card1);
            hand2.Enqueue(card2);
            
            // if equal then begin war
            while(winner==0)
            {
                Console.Error.WriteLine("WAR");
                for(int i=0; i<3; i++)
                { 
                    if(deck1.Count==0){Console.WriteLine("PAT"); return;}
                    hand1.Enqueue(deck1.Dequeue());
                    
                }
                
                for(int i=0; i<3; i++)
                { 
                    if(deck2.Count==0){Console.WriteLine("PAT"); return;}
                    hand2.Enqueue(deck2.Dequeue());

                }
                
                Console.Error.WriteLine("{0} {1}", deck1.Count, deck2.Count);
                if(deck1.Count==0 || deck2.Count==0){Console.WriteLine("PAT"); return;}
                card1=deck1.Dequeue();
                card2=deck2.Dequeue();
                winner=playCards(card1, card2);
                Console.Error.WriteLine("P1:{0}  P2:{1}  =>  {2}  WAR", card1, card2, winner);
                hand1.Enqueue(card1);
                hand2.Enqueue(card2);
                
            }
            // end war
            roundnumber++;
            
            if (winner==1)
            {
                while(hand1.Count>0){deck1.Enqueue(hand1.Dequeue());}
                while(hand2.Count>0){deck1.Enqueue(hand2.Dequeue());}
                if(deck2.Count==0){
                    Console.WriteLine("1 {0}", roundnumber);
                    return;
                }
                
            }
            else if (winner==2)
            {
                while(hand1.Count>0){deck2.Enqueue(hand1.Dequeue());}
                while(hand2.Count>0){deck2.Enqueue(hand2.Dequeue());}
                if(deck1.Count==0){
                    Console.WriteLine("2 {0}", roundnumber);
                    return;
                }
            }
            else
            {
                Console.Error.WriteLine("Something went wrong");
            }
        
        }
    }
}
