using System;
using System.Collections.Generic;
using System.IO;

namespace concur_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.Write("Enter Month End Closing Date (YYYY-MM-DD): ");
            string newDate = Console.ReadLine();
            List<string> newItems = new List<string>();

            StreamReader reader = File.OpenText("C:\\Users\\jhiney\\source\\repos\\concur-parser\\test.txt");
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] expenseLines = line.Split('\n');
                foreach (string item in expenseLines)
                {
                    string[] parts = item.Split('|');
                    //part that needs changing
                    parts[25] = newDate;

                    newItems.Add(string.Join('|', parts)); 
                }
            }
            File.WriteAllLines("C:\\Users\\jhiney\\source\\repos\\concur-parser\\output.txt", newItems.ToArray());
            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
