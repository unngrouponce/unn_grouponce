using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingInputData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя папки:");
            string n = Console.ReadLine();
            Parser p = new Parser(n);
        }
    }
}
