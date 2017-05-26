using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using MoreLinq;
using NUnit.Framework;

namespace DesignPatternsPractice
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }



    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount; //0
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            instanceCount++;
            Console.WriteLine("Initializing Database");
            // Read lines from a file to simulate Database access
            capitals = File.ReadAllLines("C:\\Кабинет\\VS\\DesignPatternsPractice\\DesignPatternsPractice\\DesignPatternsPractice\\capitals.txt")
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

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<String> names)
        {
            var result = 0;
            foreach (var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        //Dependant on a real database! Bad!
        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] {"Seoul", "Mexico City"};
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 17400000));
        }
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