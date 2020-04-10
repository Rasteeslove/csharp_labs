using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace L1
{
    class Game
    {
        int edge;
        short[][] table;
        uint gameTime;
        public System.Threading.Timer counter;

        public Game() { }

        public void NewElement()
        {
            uint size = 0;

            for (int i = 0; i < edge; i++)
                for (int j = 0; j < edge; j++)
                    if (table[i][j] == 0)
                        size++;

            Tuple<byte, byte>[] fields = new Tuple<byte, byte>[size];
            size = 0;

            for (byte i = 0; i < edge; i++)
                for (byte j = 0; j < edge; j++)
                    if (table[i][j] == 0)
                    {
                        fields[size] = new Tuple<byte, byte>(i, j);
                        size++;
                    }

            Random rand = new Random();
            int num = rand.Next((int)size);

            int chance = rand.Next(10);
            table[fields[num].Item1][fields[num].Item2] = (short)(chance == 0 ? 4 : 2);

        }

        void Swap(ref short a, ref short b)
        {
            a += b;
            b = (short)(a - b);
            a = (short)(a - b);
        }

        bool Transport(byte I, byte J, byte direction)
        {
            bool transportable = false;

            sbyte iDir = (sbyte)(-(direction % 2) * (direction - 2)), jDir = (sbyte)((direction % 2 - 1) * (direction - 1));

            while (I + iDir >= 0 && I + iDir < edge && J + jDir >= 0 && J + jDir < edge)
            {
                if (table[I][J] != 0)                           // prevents swapping zeros thus (this function) returning true        
                    if (table[I + iDir][J + jDir] == 0)
                    {
                        Swap(ref table[I][J], ref table[I + iDir][J + jDir]);
                        transportable = true;
                    }                                         // from HERE - conditions 4 standart mode
                    else if (table[I][J] == table[I + iDir][J + jDir] && table[I][J] % 2 == 0 && table[I + iDir][J + jDir] % 2 == 0)
                    {
                        table[I][J] = 0;
                        table[I + iDir][J + jDir] *= 2;
                        table[I + iDir][J + jDir]++;            // this line is 4 standart mode
                        transportable = true;
                    }
                    else
                        break;

                I = (byte)(I + iDir);
                J = (byte)(J + jDir);

            }

            return transportable;
        }

        public bool Move(byte direction)
        {
            bool movable = false;

            if (direction % 2 == 0)
                for (byte i = 0; i < edge; i++)
                    for (byte j = 0; j < edge; j++)
                        if (!movable)
                            movable = Transport(i, (byte)((edge - 2) * (1 - direction / 2) + (direction - 1) * j), direction);
                        else
                            Transport(i, (byte)((edge - 2) * (1 - direction / 2) + (direction - 1) * j), direction);
            else
                for (byte j = 0; j < edge; j++)
                    for (byte i = 0; i < edge; i++)
                        if (!movable)
                            movable = Transport((byte)((edge - 2) * (1 - direction / 2) + (direction - 2) * i), j, direction);
                        else
                            Transport((byte)((edge - 2) * (1 - direction / 2) + (direction - 2) * i), j, direction);

            // cycle 4 standardizing the numbers in the table 

            for (byte i = 0; i < edge; i++)
                for (byte j = 0; j < edge; j++)
                    if (table[i][j] % 2 == 1)
                        table[i][j]--;

            return movable;
        }

        static readonly ConsoleColor[] colors = {
                ConsoleColor.Black,
                ConsoleColor.Gray,
                ConsoleColor.White,
                ConsoleColor.Yellow,
                ConsoleColor.DarkYellow,
                ConsoleColor.Green,
                ConsoleColor.DarkCyan,
                ConsoleColor.Cyan,
                ConsoleColor.Blue,
                ConsoleColor.DarkRed,
                ConsoleColor.Red,
                ConsoleColor.Magenta,
        };

        short MyLogBaseTwoOf(short num)
        {
            short i = 0;
            for (; num > 1; i++)
                num /= 2;
            return i;
        }

        void DefineColor(short num)
        {
            Console.ForegroundColor = colors[MyLogBaseTwoOf(num)];
        }

        public void Draw()
        {
            for (byte i = 0; i < edge; i++)
                for (byte j = 0; j < edge; j++)
                {
                    DefineColor(table[i][j]);
                    Console.SetCursorPosition(1 + j * 4, i + 1);
                    Console.WriteLine("    ");
                    Console.SetCursorPosition(1 + j * 4, i + 1);
                    Console.WriteLine(table[i][j]);
                }
        }

        public void Start(byte s)
        {
            edge = s;

            table = new short[edge][];
            for (byte i = 0; i < edge; i++)
                table[i] = new short[edge];

            NewElement();
            NewElement();

            gameTime = 0;
            counter = new Timer(SayTimeConsole, 10, 1, 1000);
        }

        public bool IsWon()
        {
            for (byte i = 0; i < edge; i++)
                for (byte j = 0; j < edge; j++)
                    if (table[i][j] == 2048)
                        return true;
            return false;
        }

        public bool IsLost()
        {
            for (byte i = 0; i < edge - 1; i++)
                for (byte j = 0; j < edge - 1; j++)
                    if (table[i][j] == table[i][j + 1] || table[i][j] == 0 || table[i][j + 1] == 0 || table[i][j] == table[i + 1][j] || table[i + 1][j] == 0)
                        return false;

            if (table[edge - 1][edge - 1] == 0 || table[edge - 1][edge - 1] == table[edge - 2][edge - 1] || table[edge - 1][edge - 1] == table[edge - 1][edge - 2])
                return false;

            return true;
        }

        void SayTimeConsole(object args)
        {
            gameTime++;
            Console.Title = gameTime + " seconds have passed.";
        }

    }
}
