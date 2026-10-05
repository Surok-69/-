using System;

namespace Laba1
{
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
}