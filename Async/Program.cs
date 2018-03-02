using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            Console.WriteLine(test.GetHtmlAsync());
            Console.WriteLine(test.GetFirstCharactersCountAsync(100));
            Console.ReadKey();
        }
    }
}
