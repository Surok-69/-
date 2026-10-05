using System;

namespace Laba1
{
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
}