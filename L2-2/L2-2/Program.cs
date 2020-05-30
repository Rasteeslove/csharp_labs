using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace L2_2
{

    class Triangle
    {
        
        public struct Point : IEquatable<Point>
        {
            public readonly double X, Y;

            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }

            public bool Equals(Point other)
            {
                return Same(this.X, other.X) && Same(this.Y, other.Y);
            }
        }

        public readonly Point A, B, C;
        public readonly bool IsValid;

        #region Constructors

        public Triangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;

            if (ValidityCheck(this))
            {
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
        }

        #endregion

        #region Static methods

        private static bool Same(double a, double b)
        {
            return Math.Abs(a - b) < 0.0000001;
        }

        public static double Distance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public static double AngleByTheSecondOfThree(Point a, Point b, Point c)
        {
            return Math.Acos((Math.Pow(Distance(a, b), 2) + Math.Pow(Distance(b, c), 2) - Math.Pow(Distance(a, c), 2)) /
                (2 * Distance(a, b) * Distance(b, c)));
        }

        private static bool ValidityCheck(Triangle ABC)
        {
            if (ABC.A.Equals(ABC.B) || ABC.A.Equals(ABC.C) || ABC.B.Equals(ABC.C))
            {
                return false;
            }

            if (Same(ABC.AB + ABC.BC, ABC.AC))
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Static properties

        public static double DegRadCoefficient { get { return 180.0 / Math.PI; } }

        #endregion

        #region Instance properties

        public double AB { get { return Distance(A, B); } }
        public double BC { get { return Distance(B, C); } }
        public double AC { get { return Distance(A, C); } }
        public double Perimeter { get { return AB + BC + AC; } }
        public double Square 
        { 
            get 
            {
                double p = Perimeter / 2;
                return Math.Sqrt(p * (p - AB) * (p - AC) * (p - BC));
            }
        }

        public double AngleABC { get { return AngleByTheSecondOfThree(A, B, C); } }
        public double AngleBAC { get { return AngleByTheSecondOfThree(B, A, C); } }
        public double AngleACB { get { return AngleByTheSecondOfThree(A, C, B); } }

        public double IncircleRadius { get { return 2 * Square / (AB + BC + AC); } }
        public double ExcircleRadius { get { return AB * BC * AC / (4 * Square); } }

        #endregion



    };

    class Program
    {
        static void Main(string[] args)
        {       
            List<Triangle.Point> points = new List<Triangle.Point>();
            Triangle ABC = null;

            #region Defining ABC

            while (ABC == null)
            { 
                Console.WriteLine("Set triangle's points:");
                
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine("\tSet triangle's point #{0}:", i + 1);

                    Console.Write("\t\tSet X: ");
                    if (!double.TryParse(Console.ReadLine(), out double x)) 
                    {
                        Console.WriteLine("Invalid input.");
                        break;
                    }

                    Console.Write("\t\tSet Y: ");
                    if (!double.TryParse(Console.ReadLine(), out double y)) 
                    {
                        Console.WriteLine("Invalid input.");
                        break;
                    }

                    points.Add(new Triangle.Point(x, y));
                }

                ABC = new Triangle(points[0], points[1], points[2]);
            }

            #endregion

            #region Validity check

            if (!ABC.IsValid)
            {
                Console.WriteLine("These three points don't form a triangle.");
                return;
            }

            #endregion

            #region Calculating stuff

            Console.WriteLine("Triangle info: ");

            Console.WriteLine("Sides: AB = {0:0.00}, BC = {1:0.00}, AC = {2:0.00}", ABC.AB, ABC.BC, ABC.AC);
            Console.WriteLine("Perimeter = {0:0.00}", ABC.Perimeter);

            Console.WriteLine("Angles: ABC = {0:0.00} deg, BAC = {1:0.00} deg, ACB = {2:0.00} deg", 
                Triangle.DegRadCoefficient * ABC.AngleABC,
                Triangle.DegRadCoefficient * ABC.AngleBAC, 
                Triangle.DegRadCoefficient * ABC.AngleACB);

            Console.WriteLine("Square = {0:0.00}", ABC.Square);

            Console.WriteLine("Incircle radius = {0:0.00}, excircle radius = {1:0.00}", ABC.IncircleRadius, ABC.ExcircleRadius);

            #endregion

        }
    }
}
