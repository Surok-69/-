using System;

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
}