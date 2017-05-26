using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DesignPatternsPractice
{

    public static class ExtensionMethods
    {

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                //does same as stream.Seek(0, SeekOrigin.Begin);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
            }
                
        }
    }

    public class Person
    {
        public string[] Names;
        public Address Address;

        //Need to have empty constructors to use XmlSerializer
        public Person()
        {
            
        }

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(paramName: nameof(names));
            Address = address ?? throw new ArgumentNullException(paramName: nameof(address));
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

        //Need to have empty constructors to use XmlSerializer
        public Address()
        {
            
        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(paramName: nameof(streetName));
            HouseNumber = houseNumber;
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

            var jane = john.DeepCopyXml();
            jane.Names = new[] {"Jane", "Black"};
            jane.Address.HouseNumber = 4321;

            Console.WriteLine(john);
            Console.WriteLine(jane);
            Console.ReadLine();
        }
    }
}