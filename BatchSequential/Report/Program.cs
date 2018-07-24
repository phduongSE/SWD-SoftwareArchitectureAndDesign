using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Report
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Count() > 0)
                {
                    string inputFile = args[0];
                    XDocument inputDoc = XDocument.Load(inputFile);

                    Console.WriteLine("Account Balances Report:");
                    Console.WriteLine("UserId | Balance");
                    Console.WriteLine("----------------");

                    foreach (var item in inputDoc.Root.Descendants("Account"))
                    {
                        Console.Write(item.Element("UserId").Value + "   | ");
                        Console.WriteLine(item.Element("Balance").Value);
                    }

                    Console.ReadKey();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid argument");
            }
        }
    }
}
