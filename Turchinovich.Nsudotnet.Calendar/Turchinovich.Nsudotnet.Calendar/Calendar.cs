using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Turchinovich.Nsudotnet.Calendar
{
    class Calendar
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите дату");
            Console.BackgroundColor = ConsoleColor.Blue;
            string date = Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Black;
            DateTime dateValue;

			string[] daysOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.DayNames;
			daysOfWeek = daysOfWeek.Skip(1).Concat(daysOfWeek.Take(1)).ToArray();

            if (DateTime.TryParse(date, out dateValue))
            {
				Console.WriteLine(string.Join(" \t ", daysOfWeek.Select(s => s.Substring(0, 2))));

                int currentOffset = 1 - dateValue.Day;
                int holidays = 0;
                while (dateValue.AddDays(currentOffset--).DayOfWeek != DayOfWeek.Monday)
                {
                    Console.Write(" \t ");
                }
                currentOffset = 1 - dateValue.Day;
                while (dateValue.AddDays(currentOffset).Month == dateValue.Month)  
                {
                    if (dateValue.AddDays(currentOffset).DayOfWeek == DayOfWeek.Saturday)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (currentOffset == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        holidays++;
                    }             
                    if (dateValue.AddDays(currentOffset).DayOfWeek == DayOfWeek.Sunday)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        if (currentOffset == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        Console.WriteLine(dateValue.AddDays(currentOffset).Day);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        holidays++;
                    }
                    else
                    {
                        if (currentOffset == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                        }
                        Console.Write("{0} \t ",dateValue.AddDays(currentOffset).Day);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    currentOffset++;
                }
                Console.WriteLine("\r\nЧисло рабочих дней: {0}", currentOffset  + dateValue.Day - 1 - holidays);
            }
            else
            {
                Console.WriteLine("Неверный формат даты");
            }
        }
    }
}
