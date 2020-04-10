using System;
using System.Threading;

namespace L1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Game current = new Game();

            #region SETUP
            current.Start(4);   // pass the size of the table to start. WARNING: size should be > 1
            #endregion

            #region GAME
            current.Draw();

            System.ConsoleKey chinput;

            while (!current.IsWon() && !current.IsLost())
            {
                chinput = Console.ReadKey().Key;

                switch (chinput)
                {
                    case ConsoleKey.RightArrow:
                        if (!current.Move(0))
                            continue;
                        break;
                    case ConsoleKey.DownArrow:
                        if (!current.Move(1))
                            continue;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (!current.Move(2))
                            continue;
                        break;
                    case ConsoleKey.UpArrow:
                        if (!current.Move(3))
                            continue;
                        break;
                    default:
                        continue;
                }

                current.NewElement();
                
                current.Draw();
                
            }

            current.Draw();

            #endregion

            #region RESULT

            current.counter.Change(Timeout.Infinite, Timeout.Infinite);
            Console.ReadKey();

            #endregion
        }
    }
}
