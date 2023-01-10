using System;

namespace TWW
{

    class program
    {
        static void Main()
        {
            string[] readAccounts = File.ReadAllLines(@"Accounts.txt");
            List<Account> Accountlist = new List<Account>();
            foreach (string readaccount in readAccounts)
            {
                string[] accountitems = readaccount.Split('\u002C');
                Account account = new Account(accountitems[0], accountitems[1], accountitems[2], accountitems[3]);
                Accountlist.Add(account);
            }

            string[] readProducts = File.ReadAllLines("Products.txt");
            List<Products> Productlist = new List<Products>();
            foreach (string readproduct in readProducts)
            {
                string[] productitems = readproduct.Split('\u002C');
                Products product = new Products(productitems[0], productitems[1], productitems[2]);
                Productlist.Add(product);
            }

            string[] readLoans = File.ReadAllLines("Loans.txt");
            foreach (string readLoan in readLoans)
            {
                string[] loanitems = readLoan.Split('\u002C');

                DateTime DT = DateTime.Parse(loanitems[3]);
                List<string> Product = new List<string>();
                List<string> User = new List<string>();


                foreach (var productID in Productlist)
                {
                    if (productID == loanitems[2])
                    {
                        Product.AddRange(productID);
                    }
                }

                foreach (var username in Accountlist)
                {
                    if (username == loanitems[3])
                    {
                        User.AddRange(username);
                    }
                }

                Loans loan = new Loans(loanitems[0], Product, User, DT);
            }
            Library library = new Library("The Word Wagon", "1 Taunton rd");
            CLI.MainMenu();//displays the main menu from CLI.cs
        }
    }
}