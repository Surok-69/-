using System;

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
            string[] inputs = new string[]
            {
                "Файл \"report.docx\" 2024.03.15 204800",
                "Файл \"my photo.jpg\" 2023.12.01 1048576",
                "Файл \"notes.txt\" 2025.01.09 0",
                "Файл \"a b c d.txt\" 2022.07.30 42"
            };

            for (int i = 0; i < inputs.Length; i++)
            {
                Console.WriteLine("Вход: " + inputs[i]);
                FileObject file = FileParser.Parse(inputs[i]);
                Console.WriteLine("Результат: " + file);
                Console.WriteLine();
            }
        }
    }
}