using System;

namespace Turchinovich.Nsudotnet.NumberGuesser
{
    class NumberGuesser
    {
        static void Main()
        {
            Console.WriteLine("Введите Ваше имя");
            string name = Console.ReadLine();
            Random random = new Random();
            int requiredNumber = random.Next(101);
            //Console.WriteLine(requiredNumber);
            Console.WriteLine("{0}, начнем игру! Угадай загаданное число от 0 до 100", name);
            string[] badWords = { ", а не Javascript'ор ли вы часом?", ", ты явно не сын маминой подруги", ", главное участие ведь, а не победа :)", ", вы и в кейсах поди участвуете?" };
            char[] allAttemps = new Char[1000];

            int WRONG_NUMBER = -1;
            int currentVariant = WRONG_NUMBER;
            int attempts = 0;
            DateTime start = DateTime.Now;

            while (requiredNumber != currentVariant)
            {
                if (attempts % 4 == 0 && attempts != 0)
                {
                    Console.WriteLine("{0}{1}", name, badWords[random.Next(badWords.Length)]);
                }
                string currentLine = Console.ReadLine();
                if (currentLine == "q")
                {
                    Console.WriteLine("Жалко, что не доиграли");
                    Environment.Exit(0);
                }
                if (Int32.TryParse(currentLine, out currentVariant))
                {
                    if (currentVariant > requiredNumber)
                    {
                        Console.WriteLine("Больше, чем загаданное");
                        allAttemps[attempts] = '>';
                    }

                    if (currentVariant < requiredNumber)
                    {
                        Console.WriteLine("Меньше, чем загаданное");
                        allAttemps[attempts] = '<';
                    }
                }
                else
                {
                    Console.WriteLine("Это не число");
                    allAttemps[attempts] = '-';
                    currentVariant = WRONG_NUMBER;
                }
                attempts++;
            }
            DateTime finish = DateTime.Now;
            TimeSpan totalTime = finish - start;
            Console.WriteLine("===========================");
            Console.WriteLine("Поздравляем!");
            Console.WriteLine("Совершено попыток: {0}.", attempts);
            Console.WriteLine("История угадывания:");
            for (int i = 0; i < attempts - 1; i++)
            {
                Console.WriteLine("Попытка {0}: {1}", i + 1, allAttemps[i]);
            }
            Console.WriteLine("Затрачено секунд на угадывание: {0}", totalTime.TotalSeconds);
            Console.WriteLine("===========================");

        }
    }
}
