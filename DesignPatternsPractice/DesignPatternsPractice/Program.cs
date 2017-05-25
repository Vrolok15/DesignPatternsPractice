using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsPractice
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    public class Point
    {
        private double x, y;

        // Need to know what "a" and "b" is, need to specify Coordinate System if not Cartesian
        public Point(double a, double b, 
            CoordinateSystem system =  CoordinateSystem.Cartesian)
        {
            
            switch (system)
            {
                case CoordinateSystem.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }

        class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }
    }
}