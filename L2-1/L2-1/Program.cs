using System;

namespace L2_1
{
    class Program
    {

        static void SingleCardShuffle(ref string str)
        {

            System.Random rand = new Random();

            int slice = 1 + rand.Next(str.Length);
            int entry = rand.Next(str.Length - slice);

            string partOne = str.Substring(0, entry);
            string partX = str.Substring(entry, slice);
            string partTwo = str.Substring(entry + slice, str.Length - entry - slice);

            int newEntry = rand.Next(str.Length - slice + 1);

            if (newEntry < entry)
            {
                str = partOne.Substring(0, newEntry) + partX + partOne[newEntry..] + partTwo;
            }
            else
            {
                str = partOne + partTwo.Substring(0, newEntry - partOne.Length) +
                    partX + partTwo.Substring(newEntry - partOne.Length, partOne.Length + partTwo.Length - newEntry);
            }
        }

        // kind of shuffle you would expect before the card game and clearly not the best one
        static string CardShuffle(string str)
        {
            // i dunno how many shuffles to do but probably the half of the number of elements will do
            for (uint i = 0; i < str.Length / 2; i++)
            {
                SingleCardShuffle(ref str);
            }

            return str;
        }

        static void RandomSwap(ref System.Text.StringBuilder strB)
        {
            System.Random rand = new Random();
            int i = rand.Next(strB.Length);
            int j = rand.Next(strB.Length);
            char tmp = strB[i];
            strB[i] = strB[j];
            strB[j] = tmp;
        }

        static string ExperimentalShuffle(string str)   // lasts a lot of time i guess
        {
            System.Text.StringBuilder strB = new System.Text.StringBuilder(str);

            for (int i = 0; i < str.Length; i++)
            {
                RandomSwap(ref strB);
            }

            return strB.ToString();
        }

        static string CoolShuffle(string str)   // probably the best option
        {
            System.Text.StringBuilder origStr = new System.Text.StringBuilder(str);
            System.Text.StringBuilder newStr = new System.Text.StringBuilder(str.Length);
            System.Random rand = new Random();

            for (int i = 0; i < str.Length; i++)
            {
                int index = rand.Next(origStr.Length);
                newStr.Append(origStr[index]);
                origStr.Remove(index, 1);
            }

            return newStr.ToString();
        }

        static void FirstTask() // эффективное перемешивание символов строки
        {
            int numberOfSamples = 10;

            System.Console.Write("Enter your line: ");
            string str = Console.ReadLine();

            System.Console.Write("\nCard shuffle:\n\n");

            for (int i = 0; i < numberOfSamples; i++)
            {
                System.Console.Write(CardShuffle(str) + "\n");
            }

            System.Console.Write("\nExperimental shuffle:\n\n");

            for (int i = 0; i < numberOfSamples; i++)
            {
                System.Console.Write(ExperimentalShuffle(str) + "\n");
            }

            System.Console.Write("\n\"Cool\" shuffle:\n\n");

            for (int i = 0; i < numberOfSamples; i++)
            {
                System.Console.Write(CoolShuffle(str) + "\n");
            }
        }

        static void Main(string[] args)
        {
            FirstTask();
        }
    }
}
