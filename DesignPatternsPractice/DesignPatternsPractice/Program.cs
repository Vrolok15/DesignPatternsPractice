using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsPractice
{
    
    public class Point
    {
        private double x, y;

        //Can be made internal, if need to limit access outside the assembly
        //If needs to be private, need Inner Factory
        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
            Console.ReadLine();
        }
    }
}