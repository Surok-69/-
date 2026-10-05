using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laba1
{
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
}