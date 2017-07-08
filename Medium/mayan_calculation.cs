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
    public static string DigitToString(int digit, Dictionary<int, string> decipher)
    {
        return decipher[digit];
    }
    
    public static int StringToDigit(string codedString, Dictionary<int, string> decipher)
    {
        
        return decipher.Where(x => x.Value==codedString).Select(x => x.Key).First();    
    }
    
    public static double Decode(string codedString, Dictionary<int, string> decipher, int blockLength, int blockHeight)
    {
        int power = 0;
        double result = 0;
        while(codedString.Length>0)
        {
            int start=codedString.Length-(blockLength*blockHeight);
            string block=codedString.Substring(start, blockLength*blockHeight);
            codedString=codedString.Remove(start, blockLength*blockHeight);
            int digit=StringToDigit(block, decipher);
            result+=(digit*Math.Pow(20,power));
            power++;
            // Console.Error.WriteLine("Block: "+block);
        }
        
        return result;
    }
    
    public static string Encode(double number, Dictionary<int, string> decipher)
    {
        string encoded="";
        if(number==0) return DigitToString(0, decipher);

        while(number>0)
        {
            int digit=Convert.ToInt32(number%20);
            number=(number-digit);
            //Console.Error.WriteLine("NewNumber: "+number+"  Digit: "+digit);
            number=number/20;
            encoded=DigitToString(digit, decipher)+encoded;
        }
        return encoded;
    }
    
    static void Main(string[] args)
    {
        Dictionary<int, string> decipher = new Dictionary<int, string>();
        
        string[] inputs = Console.ReadLine().Split(' ');
        int L = int.Parse(inputs[0]);
        int H = int.Parse(inputs[1]);
        
        // representation of the 20 mayan numerals (0-19)
        for (int i = 0; i < H; i++)
        {
            string numeral = Console.ReadLine();
            for(int j=0; j<20; j++)
            {   
                string value;
                if(!decipher.TryGetValue(j, out value))
                {
                    decipher.Add(j, "");
                }
                decipher[j]+=numeral.Substring(j*L, L);
                
            }
        }
        
        string num1String="";
        string num2String="";
        int S1 = int.Parse(Console.ReadLine());
        for (int i = 0; i < S1; i++)
        {
            
            string num1Line = Console.ReadLine();
            num1String+=num1Line;
        }
        
        int S2 = int.Parse(Console.ReadLine());
        for (int i = 0; i < S2; i++)
        {
            string num2Line = Console.ReadLine();
            num2String+=num2Line;
        }
        
        string operation = Console.ReadLine();

        double num1 = Decode(num1String, decipher, L, H);
        double num2 = Decode(num2String, decipher, L, H);
        Console.Error.WriteLine("Number1: "+num1);
        Console.Error.WriteLine("Number2: "+num2);

        double answer=0;
        switch(operation)
        {
            case "+": answer=num1+num2; break;
            case "-": answer=num1-num2; break;
            case "*": answer=num1*num2; break;
            case "/": answer=num1/num2; break;
        }
        
        Console.Error.WriteLine("Answer: "+answer);
        
        string result=Encode(answer, decipher);
        //refoprtmat the answer
        int k = 0;
        while(result.Length>0)
        {
            
            Console.WriteLine(result.Substring(0, L));
            result=result.Remove(0, L);
           
        }
        
        //Console.WriteLine(result);
    }
}
