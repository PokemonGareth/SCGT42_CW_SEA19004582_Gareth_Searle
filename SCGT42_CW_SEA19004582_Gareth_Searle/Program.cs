using System;

namespace TWW
{

    class program
    {
        static void Main()
        {
            Library library = new Library("The Word Wagon", "1 Taunton rd");

            string[] readAccounts = File.ReadAllLines(@"Accounts.txt");
            foreach (string line in readAccounts)
            {
                if (line != "")
                {
                    string[] accountitems = line.Split('\u002C');
                    Account account = new Account(accountitems[0], accountitems[1], accountitems[2], accountitems[3]);
                    library.Accounts.Add(account);
                }
            }

            string[] readProducts = File.ReadAllLines("Products.txt");
            foreach (string line in readProducts)
            {
                if (line != "")
                {
                    string[] productitems = line.Split('\u002C');
                    Products product = new Products(productitems[0], productitems[1], productitems[2], productitems[3]);
                    library.Products.Add(product);
                }
            }

            string[] readLoans = File.ReadAllLines("Loans.txt");
            foreach (string line in readLoans)
            {
                if (line != "")
                {
                    string[] loanitems = line.Split('\u002C');

                    DateTime DT = DateTime.Parse(loanitems[3]);

                    Products? productdetails = library.GetProduct(loanitems[1]);
                    Account? accountdetails = library.GetAccount(loanitems[2]);


                    Loans loan = new Loans(loanitems[0], productdetails, accountdetails, DT);
                    library.Loans.Add(loan);
                }
            }
            CLI.library = library;
            CLI.MainMenu();//displays the main menu from CLI.cs
        }
    }
}