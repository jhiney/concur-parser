using System;
using System.IO;

namespace concur_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            int test = 0;
            Console.WriteLine("Hello World!");


            StreamReader reader = File.OpenText("C:\\Users\\jhiney\\source\\repos\\concur-parser\\test.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] expenseLines = line.Split('\n');
                foreach (string item in expenseLines)
                {
                    string[] parts = item.Split('|');

                    //this is the part right before the Report Name
                    Console.WriteLine(parts[5]+ " "+parts[25]);
                       
                    
                    test++;
                }
            }
            Console.WriteLine(test.ToString());
            Console.Read();
        }
    }
}
