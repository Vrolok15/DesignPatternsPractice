using System;

namespace DesignPatternsPractice
{
    //Can only be one! But making it static - not a great idea...
    public class CEO
    {
        //Make these static!
        private static string name;
        private static int age;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Age
        {
            get => age;
            set => age = value;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ceo = new CEO();
            ceo.Name = "August C.";
            ceo.Age = 33;

            var ceo2 = new CEO();
            //Still refers to the same fields
            Console.WriteLine(ceo2);
            Console.ReadLine();
        }
    }
}