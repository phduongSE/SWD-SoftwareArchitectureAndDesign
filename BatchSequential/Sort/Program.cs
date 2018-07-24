using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Sort
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

                    var a = inputDoc.Root.Descendants("Transaction").OrderBy(_ => _.Element("Amount").Value).ToList();

                    inputDoc.Root.Descendants("Transaction").Remove();

                    foreach (var item in a)
                    {
                        inputDoc.Root.Add(item);
                    }

                    inputDoc.Save("Sorted.xml");

                    Console.WriteLine("Transactions Sorted");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid argument");
            }
        }
    }
}
