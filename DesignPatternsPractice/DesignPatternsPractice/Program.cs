using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using MoreLinq;

namespace DesignPatternsPractice
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }



    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;

        private SingletonDatabase()
        {
            Console.WriteLine("Initializing Database");
            // Read lines from a file to simulate Database access
            capitals = File.ReadAllLines("capitals.txt")
                // Group lines by 2
                .Batch(2)
                // Transform them to dictionary entries
                .ToDictionary(
                // Where 1st line goes to key value (trimmed - without spaces)
                list => list.ElementAt(0).Trim(),
                // And second line parses into int
                list => int.Parse(list.ElementAt(1))
                );
        }
        

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} has population: {db.GetPopulation(city)}");
            Console.ReadLine();
        }
    }
}