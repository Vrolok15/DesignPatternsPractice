using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsPractice
{

    //Random Drink Types just for fun :D
    public class DrinkTypes
    {
        public string[] TeaType = { "Green Tea", "Earl Grey", "Milk Oolong", "Raspberry Tea" };
        public string[] CoffeeType = { "Americano", "Cappuccino", "Latte", "Double Double" };
    }

    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Random rand = new Random();
            DrinkTypes drinks = new DrinkTypes();
            Console.WriteLine($"{drinks.TeaType[rand.Next(0, drinks.TeaType.Length - 1)]} sooths your nerves...");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Random rand = new Random();
            DrinkTypes drinks = new DrinkTypes();
            Console.WriteLine($"{drinks.CoffeeType[rand.Next(0, drinks.TeaType.Length - 1)]} energizes you!");
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
            Console.WriteLine($"{amount} ml of hot tea are being prepared...");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"{amount} ml of hot coffee are being prepared...");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        public enum AvailableDrink { Coffee, Tea }

        private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
            new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory) Activator.CreateInstance(
                    Type.GetType("DesignPatternsPractice." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
                );
                factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 250);
            drink.Consume();

            var drink2 = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 150);
            drink2.Consume();

            Console.ReadLine();
        }
    }
}