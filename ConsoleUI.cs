using System;

namespace Lab3
{
    public class ConsoleUI
    {
        public void ShowMenu()
        {
            Console.WriteLine("\n=== МЕНЮ ===");
            Console.WriteLine("1 - Добавить лабораторную");
            Console.WriteLine("2 - Добавить курсовую");
            Console.WriteLine("3 - Добавить дипломную");
            Console.WriteLine("4 - Добавить контрольную");
            Console.WriteLine("5 - Загрузить из файла");
            Console.WriteLine("6 - Загрузить из строки");
            Console.WriteLine("7 - Показать всё и выйти");
        }

        public string Ask(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        public void Show(string text) => Console.WriteLine(text);

        public void ShowWork(Work work) => Console.WriteLine(work.Describe());
    }
}

