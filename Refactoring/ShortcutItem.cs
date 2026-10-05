using System;

namespace Laba1
{
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
}