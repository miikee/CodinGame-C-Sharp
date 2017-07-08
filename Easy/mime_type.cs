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
    static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
        int Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.
        
        Dictionary<string, string> MIMES = new Dictionary<string, string>();
        for (int i = 0; i < N; i++)
        {
            string[] inputs = Console.ReadLine().Split(' ');
            string EXT = inputs[0].ToLower(); // file extension lowercase
            string MT = inputs[1]; // MIME type.
            //Console.Error.WriteLine("{0} , {1}", EXT, MT);
            MIMES.Add(EXT, MT);
        }
        
        
        List<string> extensions = new List<string>();
        for (int i = 0; i < Q; i++)
        {
            string FNAME = Console.ReadLine(); // One file name per line.
            if(FNAME.IndexOf('.')==-1) FNAME+=".noextensionsdeteced"; // if FNAME has no extsion, add one to make program work
            string[] fname=FNAME.Split('.');
            string ext=fname[fname.Length-1].ToLower();  // ext following last '.' lowercased
            Console.Error.WriteLine(FNAME);
            Console.Error.WriteLine("EXT:{0}",ext);
            extensions.Add(ext);
        }
        Console.Error.WriteLine("end List------");
        
        foreach(string ext in extensions){
            string output;
            if(!MIMES.TryGetValue(ext, out output)) output="UNKNOWN";
            Console.WriteLine(output);
        }
        
        // For each of the Q filenames, display on a line the corresponding MIME type. If there is no corresponding type, then display UNKNOWN.
       // Console.WriteLine("UNKNOWN");
    }
}
