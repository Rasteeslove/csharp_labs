using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace L2_3
{
    class Program
    {
        static List<string> GetWeekDaysByCulture(CultureInfo culture)
        {
            return new List<string>
            {

                culture.DateTimeFormat.GetDayName(DayOfWeek.Monday),
                culture.DateTimeFormat.GetDayName(DayOfWeek.Tuesday),
                culture.DateTimeFormat.GetDayName(DayOfWeek.Wednesday),
                culture.DateTimeFormat.GetDayName(DayOfWeek.Thursday),
                culture.DateTimeFormat.GetDayName(DayOfWeek.Friday),
                culture.DateTimeFormat.GetDayName(DayOfWeek.Saturday),
                culture.DateTimeFormat.GetDayName(DayOfWeek.Sunday)
            };
        }

        static List<string> GetMonthNamesByCulture(CultureInfo culture)
        {
            return new List<string>
            {
                culture.DateTimeFormat.GetMonthName(1),
                culture.DateTimeFormat.GetMonthName(2),
                culture.DateTimeFormat.GetMonthName(3),
                culture.DateTimeFormat.GetMonthName(4),
                culture.DateTimeFormat.GetMonthName(5),
                culture.DateTimeFormat.GetMonthName(6),
                culture.DateTimeFormat.GetMonthName(7),
                culture.DateTimeFormat.GetMonthName(8),
                culture.DateTimeFormat.GetMonthName(9),
                culture.DateTimeFormat.GetMonthName(10),
                culture.DateTimeFormat.GetMonthName(11),
                culture.DateTimeFormat.GetMonthName(12),
            };
        }

        static void Main(string[] args)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

            Console.WriteLine("Select language:");
            for (int i = 0; i < cultures.Length; i++)
            {
                Console.WriteLine(" {0}\t\t{1}", i + 1, cultures[i].EnglishName);
            }

            Console.WriteLine("Enter the number of language: ");

            int choice;
            do
            {
                string input = Console.ReadLine();
                if (!Int32.TryParse(input, out choice))
                {
                    continue;
                }
            } while (choice - 1 < 0 || choice - 1 > cultures.Length);

            CultureInfo culture = cultures[choice - 1];

            List<string> weekDays = GetWeekDaysByCulture(culture);
            List<string> months = GetMonthNamesByCulture(culture);

            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Week days names in {0}:", culture.EnglishName);
            for (int i = 0; i < weekDays.Count; i++)
            {
                Console.WriteLine(weekDays[i]);
            }

            Console.WriteLine("Months names in {0}:", culture.EnglishName);
            for (int i = 0; i < months.Count; i++)
            {
                Console.WriteLine(months[i]);
            }
        }
    }
}
