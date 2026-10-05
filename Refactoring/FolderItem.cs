using System;

namespace Laba1
{
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
}