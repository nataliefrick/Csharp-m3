/*
    Guestbook entry app which has a simple menu to add or delete an entry that is stored in a datafile. 
*/
#nullable disable

using System;
using System.Collections.Generic;
using System.IO;

using System.Text.Json;

namespace entries
{
    public class Guestbook
    {
        private string datafile = @"gbEntries.json";

        private List<Entry> entries = new List<Entry>(); 

        public Guestbook() {
            //check first to see if datafile exists
            if(File.Exists(@"gbEntries.json")==true){
                //read data and place in variable
                string jsonString = File.ReadAllText(datafile);
                entries = JsonSerializer.Deserialize<List<Entry>>(jsonString);
            }
        }
        // Add a new entry
        public Entry addEntry(Entry entry) {
            entries.Add(entry);
            marshal();
            return entry;
        }
        // Delete an entry based on index
        public int deleteEntry(int index){
            entries.RemoveAt(index);
            marshal();
            return index;
        }

        // get all entries, return an array
        public List<Entry> getEntries() {
            return entries;
        }

        private void marshal() {
            // Serialize all objects and save to file
            var jsonString = JsonSerializer.Serialize(entries);
            File.WriteAllText(datafile, jsonString);
        }
    }

    public class Entry
    {
        private string name;
        public string Name
        {
            set { this.name = value; }
            get { return this.name; }
        } 
        private string comment; 
        public string Comment 
        {
            set { this.comment = value; }
            get { return this.comment; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Guestbook gbentries = new Guestbook();
            int i=0;

            while(true){
                Console.Clear();
                Console.CursorVisible = false;

                Console.WriteLine("GUESTBOOK\n\n");

                Console.WriteLine("1. Add a comment");
                Console.WriteLine("2. Delete a comment\n");
                Console.WriteLine("X. Quit\n");

                i=0;
                foreach(Entry entry in gbentries.getEntries()){
                    Console.WriteLine("[" + i++ + "] " + entry.Name + " - " + entry.Comment );
                }

                int input = (int) Console.ReadKey(true).Key;
                switch (input) {
                    case '1':
                        Console.CursorVisible = true; 
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Comment: ");
                        string comment = Console.ReadLine();
                        Entry obj = new Entry();
                        obj.Name = name;
                        obj.Comment = comment;
                        // if(!String.IsNullOrEmpty(name)) gbentries.addEntry(obj);
                        if(!String.IsNullOrEmpty(name)&&!String.IsNullOrEmpty(comment)) gbentries.addEntry(obj);
                        break;
                    case '2': 
                        Console.CursorVisible = true;
                        Console.Write("Which comment do you want to delete: ");
                        string index = Console.ReadLine();
                        gbentries.deleteEntry(Convert.ToInt32(index));
                        break;
                    case 88: 
                        Environment.Exit(0);
                        break;
                }
 
            }
        }
    }


}