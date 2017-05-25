using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsPractice
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Warm Tea sooths your nerves...");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Hot Coffee energizes you!");
        }
    }

    //Factory interface
    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"{amount} ml of Warm Tea are being prepared...");
            Console.WriteLine("");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"{amount} ml of Hot Coffee are being prepared...");
            Console.WriteLine("");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        //!!!
        // Original Class was bad, because it used enum to access drink types
        // That meant that variety of available drinks could only be changed from inside
        // Which is not good! Remember Open/Closed Principle!
        //!!!

        //public enum AvailableDrink { Coffee, Tea }

        //private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
        //    new Dictionary<AvailableDrink, IHotDrinkFactory>();

        //public HotDrinkMachine()
        //{
        //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //    {
        //        var factory = (IHotDrinkFactory) Activator.CreateInstance(
        //            Type.GetType("DesignPatternsPractice." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
        //        );
        //        factories.Add(drink, factory);
        //    }
        //}

        //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        //{
        //    return factories[drink].Prepare(amount);
        //}

        private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        ));
                }
            }
        }

        public IHotDrink MakeDrink()
        {
            Console.WriteLine("Available drinks:");
            for (var index = 0; index < factories.Count; index++)
            {
                var tuple = factories[index];
                Console.WriteLine($"{index}: {tuple.Item1}");
            }
            while (true)
            {
                string s;
                if ((s = Console.ReadLine()) != null
                    && int.TryParse(s, out int i)
                    && i >= 0
                    && i < factories.Count)
                {
                    Console.Write("Specify amount:");
                    if ((s = Console.ReadLine()) != null
                        && int.TryParse(s, out int amount)
                        && amount >= 0)
                    {
                        return factories[i].Item2.Prepare(amount);
                    }
                }

                Console.WriteLine("Incorrect input, try again");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink();
            drink.Consume();

            //var drink2 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 150);
            //drink2.Consume();

            Console.ReadLine();
        }
    }
}