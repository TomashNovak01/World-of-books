using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace World_of_books_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string number = "12345678914";
            string error1 = "213";
            string error2 = "asdsdw";
            string error3 = "serfgvcdser";
            string error4 = "serfg12dser";


            //if(error3.Length != 11 && !int.TryParse(error3, out int x))
            //    Console.WriteLine("!!!!!!!!!!!!!!");

            if(error3.Length != 11)
                Console.WriteLine("!!!!!!!!!!!!!!");
            if(!int.TryParse(error3, out int x))
                Console.WriteLine("@@@@@@@@@@@@@@@");
            if(error3.Length != 11 || !int.TryParse(error3, out int x2))
                Console.WriteLine("RRRRRRRRRRRRRRR");
            if(error3.Length != 11 && !int.TryParse(error3, out int x3))
                Console.WriteLine("GGGGGGGGGGGGGGGGGG");

            Console.ReadKey();
        }
    }
}
