using System;
using System.IO;

class Zach
{
    static void Main()
    {
        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string path = Path.Combine(desktop, "txt.txt");

        if (!File.Exists(path))
        {
            Console.WriteLine("Файл txt.txt не найден на рабочем столе!");
            return;
        }

        string[] lines = File.ReadAllLines(path);

        for (int y = 0; y < lines.Length; y++)
            Console.WriteLine($"{y,2}| {lines[y]}");
        Console.WriteLine();

        Console.WriteLine("Введите координаты в формате x,y");
        Console.WriteLine("Пример: 3,0");
        Console.WriteLine();

        int replaced = 0;
        int skipped = 0;

        while (true)
        {
            Console.Write("Координата: ");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                break;

            string[] parts = input.Split(',');
            if (parts.Length != 2 ||
                !int.TryParse(parts[0].Trim(), out int x) ||
                !int.TryParse(parts[1].Trim(), out int y))
            {
                Console.WriteLine("  ! Неверный формат. Пример: 3,0");
                continue;
            }

            if (y < 0 || y >= lines.Length)
            {
                Console.WriteLine($"  ! Строка {y} вне диапазона (0..{lines.Length - 1})");
                continue;
            }
            if (x < 0 || x >= lines[y].Length)
            {
                Console.WriteLine($"  ! Столбец {x} вне диапазона (0..{lines[y].Length - 1})");
                continue;
            }

            char[] chars = lines[y].ToCharArray();

            if (chars[x] == '#')
            {
                chars[x] = '*';
                lines[y] = new string(chars);
                replaced++;
                Console.WriteLine($"  + ({x},{y}): '#' -> '*'");
            }
            else
            {
                skipped++;
                Console.WriteLine($"  - ({x},{y}): там '{chars[x]}', ничего не меняю.");
            }
        }

        File.WriteAllLines(path, lines);

        Console.WriteLine();
        Console.WriteLine($"Заменено: {replaced}, пропущено: {skipped}");
        Console.WriteLine();

        for (int y = 0; y < lines.Length; y++)
            Console.WriteLine($"{y,2}| {lines[y]}");
    }
}