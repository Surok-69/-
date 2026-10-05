using System;
using System.Text;

namespace Laba1
{
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