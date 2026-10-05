using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Laba1
{
    abstract class Item
    {
        public string Name { get; }
        public DateTime Date { get; }

        protected Item(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }

        protected abstract string Kind { get; }
        protected abstract string Details { get; }

        public override string ToString() => $"[{Kind}] \"{Name}\" {Date:dd.MM.yyyy} {Details}";
    }

    class FileItem : Item
    {
        public int Size { get; }

        public FileItem(string name, DateTime date, int size) : base(name, date)
        {
            Size = size;
        }

        protected override string Kind => "Файл";
        protected override string Details => $"размер: {Size}";
    }

    class FolderItem : Item
    {
        public int ItemsCount { get; }

        public FolderItem(string name, DateTime date, int itemsCount) : base(name, date)
        {
            ItemsCount = itemsCount;
        }

        protected override string Kind => "Папка";
        protected override string Details => $"элементов внутри: {ItemsCount}";
    }

    class ShortcutItem : Item
    {
        public string Target { get; }

        public ShortcutItem(string name, DateTime date, string target) : base(name, date)
        {
            Target = target;
        }

        protected override string Kind => "Ярлык";
        protected override string Details => $"ведёт на: {Target}";
    }

    static class ItemFactory
    {
        public static Item CreateFromString(string line)
        {
            string[] parts = line.Split('"');
            if (parts.Length < 3)
                throw new FormatException("неверный формат строки: " + line);

            string type = parts[0].Trim().ToUpper();
            string name = parts[1];
            string[] rest = parts[2].Trim().Split(new char[] { ' ' }, 2);
            if (rest.Length < 2)
                throw new FormatException("неверный формат строки: " + line);

            DateTime date = DateTime.ParseExact(rest[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            string value = rest[1].Trim();

            switch (type)
            {
                case "D": return new FolderItem(name, date, int.Parse(value));
                case "L": return new ShortcutItem(name, date, value);
                case "F":
                case "": return new FileItem(name, date, int.Parse(value));
                default: throw new FormatException("неизвестный тип: " + type);
            }
        }
    }

    class ItemStorage
    {
        public const string FileName = "input.txt";
        private List<Item> items = new List<Item>();

        public void Add(Item item) => items.Add(item);

        public int RemoveByName(string name) => items.RemoveAll(i => i.Name == name);

        public void Show()
        {
            if (items.Count == 0)
                Console.WriteLine("Список пуст.");

            for (int i = 0; i < items.Count; i++)
                Console.WriteLine((i + 1) + ". " + items[i]);
        }

        public void LoadFromFile()
        {
            if (!File.Exists(FileName))
            {
                Console.WriteLine("Файл '" + FileName + "' не найден.");
                return;
            }

            foreach (string line in File.ReadAllLines(FileName, Encoding.UTF8))
            {
                if (line.Trim() == "") continue;

                try
                {
                    items.Add(ItemFactory.CreateFromString(line));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Пропущена строка (" + ex.Message + ")");
                }
            }
        }
    }

    class Menu
    {
        private ItemStorage storage;

        public Menu(ItemStorage storage)
        {
            this.storage = storage;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== МЕНЮ =====");
                Console.WriteLine("1. Добавить объект");
                Console.WriteLine("2. Показать все объекты");
                Console.WriteLine("3. Удалить объект по имени");
                Console.WriteLine("4. Загрузить из " + ItemStorage.FileName);
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");

                switch (Console.ReadLine()?.Trim())
                {
                    case "1": AddItem(); break;
                    case "2": storage.Show(); break;
                    case "3": RemoveItem(); break;
                    case "4": storage.LoadFromFile(); break;
                    case "0": return;
                    default: Console.WriteLine("Неверный пункт меню."); break;
                }
            }
        }

        private void AddItem()
        {
            Console.WriteLine("Формат: [F|D|L] \"имя\" дд.мм.гггг значение");
            Console.WriteLine("F - файл (размер), D - папка (кол-во элементов), L - ярлык (путь)");
            Console.Write("Введите строку: ");

            try
            {
                Item item = ItemFactory.CreateFromString(Console.ReadLine());
                storage.Add(item);
                Console.WriteLine("Добавлено: " + item);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }

        private void RemoveItem()
        {
            Console.Write("Введите имя: ");
            int removed = storage.RemoveByName(Console.ReadLine());
            Console.WriteLine(removed > 0 ? "Удалено объектов: " + removed : "Объект не найден.");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            ItemStorage storage = new ItemStorage();
            if (args.Length > 0 && args[0] == "-f")
                storage.LoadFromFile();

            new Menu(storage).Run();
        }
    }
}