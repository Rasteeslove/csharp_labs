using System;
using System.Runtime.InteropServices;

namespace L4_2
{ 
    class UnmanagedMath
    {
        [DllImport("MathLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Add(int a, int b);

        [DllImport("MathLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Subtract(int a, int b);

        [DllImport("MathLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Multiply(int a, int b);

        [DllImport("MathLib.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern float Divide(float a, float b);

        [DllImport("MathLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int Power(int a, int b);

        [DllImport("MathLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int Factorial(int a);

        [DllImport("MathLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int GCD(int a, int b);

        [DllImport("MathLib.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int LCM(int a, int b);      
    }

    class Program
    {
        static void Main(string[] args)
        {
            int a, b;

            Console.WriteLine("Enter 2 numbers so that the app performs some operations on them:");
            Console.WriteLine("[power might be wrong with big values]");

            #region Input

            do
            {
                Console.WriteLine("Enter a: ");
            } while (!Int32.TryParse(Console.ReadLine(), out a));

            do
            {
                Console.WriteLine("Enter b: ");
            } while (!Int32.TryParse(Console.ReadLine(), out b));

            #endregion

            #region Output

            Console.WriteLine("Calculations:");
            Console.WriteLine("{0} + {1} = {2};", a, b, UnmanagedMath.Add(a, b));
            Console.WriteLine("{0} - {1} = {2};", a, b, UnmanagedMath.Subtract(a, b));
            Console.WriteLine("{0} * {1} = {2};", a, b, UnmanagedMath.Multiply(a, b));
            if (b != 0)
            { 
                Console.WriteLine("{0} / {1} = {2};", a, b, UnmanagedMath.Divide(a, b));
            }
            else
            {
                Console.WriteLine("b = 0, so can't divide by it;");
            }
            if (a != 0)
            { 
                Console.WriteLine("{0} / {1} = {2};", b, a, UnmanagedMath.Divide(b, a));
            }
            else
            {
                Console.WriteLine("a = 0, so can't divide by it;");
            }

            if (b >= 0)
            { 
                Console.WriteLine("{0} ^ {1} = {2};", a, b, UnmanagedMath.Power(a, b));

                if (b <= 12)
                { 
                    Console.WriteLine("{0}! = {1};", b, UnmanagedMath.Factorial(b));
                }
                else
                {
                    Console.WriteLine("b is too big to calculate fatorial;");
                }
            }
            else
            {
                Console.WriteLine("b < 0, so the app can't perform a ^ b and b!;");
            }

            if (a >= 0)
            { 
                Console.WriteLine("{0} ^ {1} = {2};", b, a, UnmanagedMath.Power(b, a));
                if (a <= 12)
                { 
                    Console.WriteLine("{0}! = {1};", a, UnmanagedMath.Factorial(a));
                }
                else
                {
                    Console.WriteLine("a is too big to calculate fatorial;");
                }
            }
            else
            {
                Console.WriteLine("a < 0, so the app can't perform b ^ a and a!;");
            }

            if (a == 0 && b == 0)
            {
                Console.WriteLine("For two zeros gcd and lcm are undefined;");
            }
            else 
            { 
                Console.WriteLine("gcd({0}, {1}) = {2};", a, b, UnmanagedMath.GCD(a, b));
                Console.WriteLine("lcm({0}, {1}) = {2};", a, b, UnmanagedMath.LCM(a, b));
            }

            #endregion

        }
    }
}