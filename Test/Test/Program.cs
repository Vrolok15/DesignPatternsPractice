using System;
using System.Collections.Generic;

namespace Test
{
    class Program
    {

        public static int HowManyRepeatingNumbersInArray(int[] array)
        {
            HashSet<int> differentNumbersSet = new HashSet<int>();
            HashSet<int> repeatingSet = new HashSet<int>();

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (differentNumbersSet.Contains(array[i]))
                {
                    repeatingSet.Add(array[i]);
                }
                else
                {
                    differentNumbersSet.Add(array[i]);
                }
                    
            }

            return repeatingSet.Count;
        }

        static void Main(string[] args)
        {
            int[] array = { 1, 1, 1, 2, 2, 3, 3, 4, 5, 6, 6, 7, 7, 8, 9, 9 };
            Console.WriteLine(HowManyRepeatingNumbersInArray(array) + " total Repeating numbers");
            Console.ReadLine();
        }
    }
}
