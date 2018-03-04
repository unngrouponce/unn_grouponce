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
            Parser parser = new Parser(args[0]);
            Console.WriteLine(args[0]);
            ObjWriter writer = new ObjWriter(args[0]+"\\result.obj");
        }
    }
}
