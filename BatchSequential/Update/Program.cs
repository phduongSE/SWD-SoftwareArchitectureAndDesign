using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Update
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
                    string accounts = args[1];
                    
                    XDocument inputDoc = XDocument.Load(inputFile);
                    XDocument accountsDoc = XDocument.Load(accounts);

                    var updateInputs = inputDoc.Root.Descendants("Transaction")
                        .Where(_ => _.Element("TYPE").Value == "U")
                        .ToList();

                    var deleteInputs = inputDoc.Root.Descendants("Transaction")
                        .Where(_ => _.Element("TYPE").Value == "D")
                        .ToList();

                    var accountList = accountsDoc.Root.Descendants("Account")
                        .ToList();

                    foreach (var item in updateInputs)
                    {
                        if (accountList.Where(_ => _.Element("UserId").Value == item.Element("UserId").Value).Count() > 0)
                        {
                            int accountBalance = int.Parse(accountList.Where(_ => _.Element("UserId").Value == item.Element("UserId").Value)
                                .FirstOrDefault()
                                .Element("Balance").Value);

                            int amountUpdate = int.Parse(item.Element("Amount").Value);

                            accountList.Where(_ => _.Element("UserId").Value == item.Element("UserId").Value)
                                .FirstOrDefault()
                                .Element("Balance").Value = (accountBalance + amountUpdate).ToString();
                        }
                    }

                    foreach (var item in deleteInputs)
                    {
                        if (accountList.Where(_ => _.Element("UserId").Value == item.Element("UserId").Value).Count() > 0)
                        {
                            int accountBalance = int.Parse(accountList.Where(_ => _.Element("UserId").Value == item.Element("UserId").Value)
                                .FirstOrDefault()
                                .Element("Balance").Value);

                            int amountDelete = int.Parse(item.Element("Amount").Value);

                            if ((accountBalance - amountDelete) < 0)
                            {
                                accountList.Where(_ => _.Element("UserId").Value == item.Element("UserId").Value)
                                    .FirstOrDefault()
                                    .Element("Balance").Value = "0";
                            }
                            else
                            {
                                accountList.Where(_ => _.Element("UserId").Value == item.Element("UserId").Value)
                                    .FirstOrDefault()
                                    .Element("Balance").Value = (accountBalance - amountDelete).ToString();
                            }
                        }
                    }

                    accountsDoc.Root.Descendants("Account").Remove();

                    foreach (var item in accountList)
                    {
                        accountsDoc.Root.Add(item);
                    }

                    accountsDoc.Save("Reports.xml");

                    Console.WriteLine("Transactions Updated");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid argument");
            }
        }
    }
}
