using System;
using System.Numerics;
using System.Collections.Generic;

namespace L7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Available formats for input are a/b or a b/c or a.bcd or a.bc(de)]");
            
            RNumber one, two;

            do
            {
                Console.WriteLine("Enter first: ");
            } while (!RNumber.TryParse(Console.ReadLine(), out one));

            do
            {
                Console.WriteLine("Enter second: ");
            } while (!RNumber.TryParse(Console.ReadLine(), out two));

            char operation;
            do
            {
                Console.WriteLine("Enter operation [+, -, *, /, ^]: ");
                operation = Console.ReadKey().KeyChar;
            } while (operation != '+' && operation != '-' && operation != '*' && operation != '/' && operation != '^');

            RNumber result = new RNumber(1, 1);

            switch(operation)
            {
                case '+':
                    result = one + two;
                    break;
                case '-':
                    result = one - two;
                    break;
                case '*':
                    result = one * two;
                    break;
                case '/':
                    if (two == 0)
                    {
                        Console.WriteLine("Can't divide by zero.");
                        return;
                    }
                    result = one / two;
                    break;
            }

            Console.WriteLine("Result in different formats: ");
            Console.WriteLine(result.ToString("s"));
            Console.WriteLine(result.ToString("m"));
            Console.WriteLine(result.ToString("d"));

        }
    }
}
