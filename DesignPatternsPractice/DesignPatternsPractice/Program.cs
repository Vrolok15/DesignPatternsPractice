using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsPractice
{
    public class Person
    {
        public int Id { get; set; }
        public static int IdSetter = 0;
        public string Name { get; set; }

        public Person(string name)
        {
            Id = IdSetter;
            IdSetter++;
            Name = name;
        }

        public override string ToString()
        {
            return $"Person ID: {Id}, Name: {Name}";
        }
    }

    public class PersonFactory
    {
        public static Person NewPerson(string name)
        {
            return new Person(name);
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            var person = PersonFactory.NewPerson("Johnny");
            var person2 = PersonFactory.NewPerson("Joey");
            var person3 = PersonFactory.NewPerson("Dee Dee");
            var person4 = PersonFactory.NewPerson("Tommy");
            Console.WriteLine(person);
            Console.WriteLine(person2);
            Console.WriteLine(person3);
            Console.WriteLine(person4);
            Console.ReadLine();
        }
    }
}