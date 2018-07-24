using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Validate
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

                    inputDoc.Root.Descendants("Transaction")
                        .Where(_ => int.Parse(_.Element("Amount").Value) < 0)
                        .Remove();

                    inputDoc.Save("Validated.xml");

                    Console.WriteLine("Transactions Validated");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid argument");
            }
        }
    }
}
