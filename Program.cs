using System;
using System.Collections.Generic;
using System.IO;


namespace concur_parser
{
    class Program
    {
        
        [STAThread]
        static void Main(string[] args)
        {
            string filePath = @"K:\ACCTING\GENERAL\Expense reports\Concur Extracts";
            string previous = null;
            bool showMenu = true;         
            filePath = MainMenu(filePath);
            
            while (showMenu)
            {  
                if (filePath.Length != 0 && !filePath.Equals("found"))
                {
                    previous = filePath;     
                    filePath = MainMenu(filePath);
                }
                else
                {
                    showMenu = false;
                }
            }

            Console.Write("Enter Month End Closing Date (YYYY-MM-DD): ");
            string newDate = Console.ReadLine();
            
            List<string> newItems = new List<string>();
            
            StreamReader reader = File.OpenText(previous);
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

        public static string MainMenu(string filePath) {

            if (!filePath.EndsWith(".txt"))
            {
                string[] filePaths = Directory.GetDirectories(filePath);

                if (filePaths.Length > 0)
                {
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        Console.WriteLine((i + 1).ToString() + ") " + filePaths[i]);
                    }

                    int select = (Convert.ToInt32(Console.ReadLine()));

                    foreach (string path in filePaths)
                    {
                        if ((filePaths[select - 1]).Equals(path))
                        {

                            return path;
                        }
                    }
                }
                else
                {
                    filePaths = Directory.GetFiles(filePath, "*.txt");
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        Console.WriteLine((i + 1).ToString() + ") " + filePaths[i]);
                    }

                    int select = (Convert.ToInt32(Console.ReadLine()));

                    foreach (string path in filePaths)
                    {
                        if ((filePaths[select - 1]).Equals(path))
                        {

                            return path;
                        }
                    }
                }
            }
            else return "found";
            return null;
        }
    }
}
