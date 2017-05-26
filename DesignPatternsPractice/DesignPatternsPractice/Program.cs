using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsPractice
{
    public class Person
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
        }

        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }


        public interface IInter
        {
            void Stop();
            string Name { get; set; }
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(paramName: nameof(streetName));
            HouseNumber = houseNumber;
        }

        //keep parameter name the same
        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new [] {"John", "Smith"}, 
                new Address("Lundy's Lane", 1234));
            var jane = new Person(john);

            jane.Names = new[] {"Jane", "Black"};
            jane.Address.HouseNumber = 4321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
            Console.ReadLine();
        }
    }
}