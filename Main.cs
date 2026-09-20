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
            List<string> tokens = SplitIntoTokens(input);

            string type = tokens[0];
            if (type != "Файл")
                throw new Exception("Неизвестный тип объекта: " + type);

            string name = RemoveQuotes(tokens[1]);
            SimpleDate date = SimpleDate.Parse(tokens[2]);
            int size = int.Parse(tokens[3]);

            return new FileObject(name, date, size);
        }

        static List<string> SplitIntoTokens(string input)
        {
            List<string> tokens = new List<string>();
            string current = "";
            bool insideQuotes = false;

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                    current = current + c;
                }
                else if (c == ' ' && insideQuotes == false)
                {
                    if (current != "")
                    {
                        tokens.Add(current);
                        current = "";
                    }
                }
                else
                {
                    current = current + c;
                }
            }

            if (current != "")
                tokens.Add(current);

            return tokens;
        }

        static string RemoveQuotes(string text)
        {
            if (text.Length >= 2 && text[0] == '"' && text[text.Length - 1] == '"')
                return text.Substring(1, text.Length - 2);
            return text;
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