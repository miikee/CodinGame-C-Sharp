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
    class Message
    {
        
    }
    
    static string chuckEncode(char encodingChar, int count){
        Console.Error.WriteLine("Encoding:{0} , count:{1}", encodingChar, count);
        string output="";
        if (encodingChar=='0') output="00 ";  // coding for 0
        else output="0 ";  // coding for 1
        
        //enter encode number encoding digits
        for(int i=0; i<count; i++){
            output+="0";
        }
    
        return output;
    }
    
    static void Main(string[] args)
    {
        string output="";
        string MESSAGE = Console.ReadLine();
        Console.Error.WriteLine(MESSAGE);
        byte[] msgBytes = Encoding.ASCII.GetBytes(MESSAGE);
        string msgByteString="";
        foreach(byte b in msgBytes){
            string byteString=Convert.ToString(b, 2);
            //correct for zeros at the beginning of byte strings
            while(byteString.Length<7){
                byteString="0"+byteString;
            }
            msgByteString+=byteString;
            Console.Error.WriteLine(msgByteString);
        }
        char[] charByte=msgByteString.ToCharArray();  //00101011
        
        char encodeChar=charByte[0]; // currently encoding 1 or 0
        int count=0;  // number of 0 or 1 to encode
        
        for(int i=0; i<charByte.Length; i++){
            Console.Error.WriteLine("i:{0}, charByte:{1}", i, charByte[i]); 
                // wrap up final
         
            if(charByte[i]==encodeChar){
                count++;  // same character increase count

            }
            else{
                output+=chuckEncode(encodeChar, count)+" ";// encode the current string with a space and start again
                encodeChar=charByte[i]; //switch to 0 or 1
                count=1;  //reset count
            }
                
            // if final pass, then encode the last bytes
            if(i==charByte.Length-1){
                output+=chuckEncode(encodeChar, count);// encode the current string and start again
            }
        }
     

        Console.WriteLine(output);
    }
}
