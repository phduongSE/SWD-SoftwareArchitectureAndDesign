using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassThroughMain
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Add 3 4
                if (args.Count() == 2)
                {
                    int val1 = int.Parse(args[0]);
                    int val2 = int.Parse(args[1]);

                    Console.WriteLine(val1 + val2);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Arguments");
            }
            
        }
    }
}
