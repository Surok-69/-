using System;
using System.Globalization;

namespace Laba1
{
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
}