using System;
using System.Collections.Generic;

namespace FileObjectApp
{
    public class SimpleDate
    {
        public int Year;
        public int Month;
        public int Day;

        public SimpleDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public static SimpleDate Parse(string text)
        {
            string[] parts = text.Split('.');
            int y = int.Parse(parts[0]);
            int m = int.Parse(parts[1]);
            int d = int.Parse(parts[2]);
            return new SimpleDate(y, m, d);
        }

        public override string ToString()
        {
            return Year + "." + Month.ToString("D2") + "." + Day.ToString("D2");
        }
    }

    public class FileObject
    {
        public string Name;
        public SimpleDate CreationDate;
        public int SizeBytes;

        public FileObject(string name, SimpleDate creationDate, int sizeBytes)
        {
            Name = name;
            CreationDate = creationDate;
            SizeBytes = sizeBytes;
        }

        public override string ToString()
        {
            return "Файл { название='" + Name +
                   "', дата создания=" + CreationDate +
                   ", размер=" + SizeBytes + " байт }";
        }
    }

    public static class FileParser
    {
        public static FileObject Parse(string input)
        {
            int firstQuote = input.IndexOf('"');
            int secondQuote = input.IndexOf('"', firstQuote + 1);

            if (firstQuote < 0 || secondQuote < 0)
                throw new Exception("В строке нет имени файла в кавычках: " + input);

            string name = input.Substring(firstQuote + 1, secondQuote - firstQuote - 1);

            string withoutName = input.Remove(firstQuote, secondQuote - firstQuote + 1);

            string[] parts = withoutName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 3)
                throw new Exception("В строке не хватает данных: " + input);

            string type = parts[0];
            if (type != "Файл")
                throw new Exception("Неизвестный тип объекта: " + type);

            SimpleDate date = SimpleDate.Parse(parts[1]);
            int size = int.Parse(parts[2]);

            return new FileObject(name, date, size);
        }
    }

    public class Program
    {
        public static void Main()
        {
            List<FileObject> files = new List<FileObject>();

            files.Add(FileParser.Parse("Файл \"report.docx\" 2024.03.15 204800"));
            files.Add(FileParser.Parse("Файл \"my photo.jpg\" 2023.12.01 1048576"));
            files.Add(FileParser.Parse("Файл \"notes.txt\" 2025.01.09 0"));
            files.Add(FileParser.Parse("Файл \"a b c d.txt\" 2022.07.30 42"));

            while (true)
            {
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Показать список файлов");
                Console.WriteLine("2. Добавить файл");
                Console.WriteLine("3. Удалить файл");
                Console.WriteLine("4. Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "1")
                {
                    ShowFiles(files);
                }
                else if (choice == "2")
                {
                    AddFile(files);
                }
                else if (choice == "3")
                {
                    DeleteFile(files);
                }
                else if (choice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Такого пункта нет, попробуйте ещё раз.");
                    Console.WriteLine();
                }
            }
        }

        static void ShowFiles(List<FileObject> files)
        {
            if (files.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                Console.WriteLine();
                return;
            }

            for (int i = 0; i < files.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + files[i]);
            }
            Console.WriteLine();
        }

        static void AddFile(List<FileObject> files)
        {
            Console.WriteLine("Введите строку в формате:");
            Console.WriteLine("Файл \"имя\" гггг.мм.дд размер");
            Console.Write("> ");

            string input = Console.ReadLine();

            try
            {
                FileObject file = FileParser.Parse(input);
                files.Add(file);
                Console.WriteLine("Файл добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            Console.WriteLine();
        }

        static void DeleteFile(List<FileObject> files)
        {
            if (files.Count == 0)
            {
                Console.WriteLine("Удалять нечего — список пуст.");
                Console.WriteLine();
                return;
            }

            ShowFiles(files);

            Console.Write("Введите номер файла для удаления: ");
            string input = Console.ReadLine();

            int number;
            if (!int.TryParse(input, out number))
            {
                Console.WriteLine("Это не число.");
                Console.WriteLine();
                return;
            }

            if (number < 1 || number > files.Count)
            {
                Console.WriteLine("Файла с таким номером нет.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Удалён: " + files[number - 1]);
            files.RemoveAt(number - 1);
            Console.WriteLine();
        }
    }
}