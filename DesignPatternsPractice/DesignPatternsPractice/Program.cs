﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternsPractice
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    public class PointFactory 
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

    public class Point
    {
        private double x, y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
            Console.ReadLine();
        }
    }
}