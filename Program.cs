﻿using System;
using System.Collections.Generic;
using System.IO;

namespace concur_parser
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            //Starting variables
            string filePath = Directory.GetParent(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString()).ToString();
            string previousPath = null;
            bool exploringFiles = true;
            filePath = MainMenu(filePath);

            while (exploringFiles)
            {
                //if the filePath is not 0 and the end of the line hasn't been found
                if (filePath.Length != 0 && !filePath.Equals("found"))
                {
                    //set the previous path before getting a new one
                    previousPath = filePath;
                    filePath = MainMenu(filePath);
                }
                else
                {
                    //close the main menu
                    exploringFiles = false;
                }
            }

            //Ask the user for a date to overwrite the text file
            Console.Write("Enter Month End Closing Date (YYYY-MM-DD): ");
            string newDate = Console.ReadLine();

            Console.Write("Name of output file? No need for .txt :");
            string outputFileName = Console.ReadLine();

            List<string> newItems = new List<string>();

            List<string> cashItems = new List<string>();

            //We open the previous path because technically the filePath is now "found"
            //TODO: Need to think of a more elegant solution for this
            StreamReader reader = File.OpenText(previousPath);
            string line;

            //While there are still lines to be read
            while ((line = reader.ReadLine()) != null)
            {
                //split the lines
                string[] expenseLines = line.Split('\n');
                foreach (string item in expenseLines)
                {
                    //split on Concur's custom delimiter
                    string[] parts = item.Split('|');
                    //part that needs changing
                    parts[25] = newDate;
                    //add back the delimiter
                    newItems.Add(string.Join('|', parts));

                    //////////////////////////////////////
                    //This is the section grabbing all the cash expenses

                    cashItems.Add(string.Join(',',parts[4],parts[124],parts[125]));
                    


                }
            }

            //write all the lines to the output file
            File.WriteAllLines(Path.Combine(Directory.GetParent(previousPath).ToString(),outputFileName + ".txt"), cashItems.ToArray());

            //You are done - read a character
            Console.WriteLine("Done");
        }

        public static string MainMenu(string filePath)
        {
            string backup = Directory.GetParent(filePath).ToString();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Currently looking in: " + filePath);
            Console.ResetColor();
            //if the found filepath is NOT a text file, keep looking through folders
            if (!filePath.EndsWith(".txt"))
            {
                //grabs all the sub-directories from the one passed
                string[] filePaths = Directory.GetDirectories(filePath);

                //if the filepath is greater than zero means that there are still more sub-directories
                if (filePaths.Length > 0)
                {
                    //write out the subdirectories as a menu
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write((i + 1).ToString() + "> ");
                        Console.ResetColor();
                        Console.WriteLine(filePaths[i]);
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Back(0)> " + backup);
                    Console.ResetColor();
                    //let the user pick a menu
                    int select = (Convert.ToInt32(Console.ReadLine()));

                    foreach (string path in filePaths)
                    {
                        if (select == 0) { Console.Clear(); return backup; }
                        //Subtract select by 1 because the user choice starts at 1 but the array is 0-indexed.
                        if ((filePaths[select - 1]).Equals(path))
                        {
                            //grab what the user picks and starts the menuing over again
                            Console.Clear();
                            return path;
                        }
                    }
                }
                //this trigers if there are no more sub directories - so instead it grabs all the text files instead
                else
                {
                    //get all the text files in the chosen directory
                    filePaths = Directory.GetFiles(filePath, "*.txt");
                    for (int i = 0; i < filePaths.Length; i++)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write((i + 1).ToString() + "> ");
                        Console.ResetColor();
                        Console.WriteLine(filePaths[i]);
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Back(0)> " + backup);
                    Console.ResetColor();

                    int select = (Convert.ToInt32(Console.ReadLine()));

                    foreach (string path in filePaths)
                    {
                        if (select == 0) { Console.Clear(); return backup; }
                        if ((filePaths[select - 1]).Equals(path))
                        {
                            //returns the chosen text file
                            Console.Clear();
                            return path;
                        }
                    }
                }
            }
            //if the filepath is a text file - returns "found"
            else return "found";
            //default return - shouldn't ever be triggered.
            return null;
        }
    }
}