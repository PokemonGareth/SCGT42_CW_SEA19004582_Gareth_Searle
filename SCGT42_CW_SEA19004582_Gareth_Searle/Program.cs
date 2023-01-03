using System;


namespace TWW
{

    class program
    {
        static void Main()
        {
            string[] readAccounts = File.ReadAllLines("Accounts.txt");
            foreach (string readAccount in readAccounts)
            {
                string[] accountitems = readAccount.Split('\u002C');
                Account account = new Account(accountitems[0], accountitems[1], accountitems[2], accountitems[3]);
            }
            string[] readProducts = File.ReadAllLines("Products.txt");
            foreach (string readproduct in readProducts)
            {
                string[] productitems = readProducts.Split('\u002C');
                Products product = new Products(productitems[0], productitems[1], productitems[2]);
            }
            string[] readLoans = File.ReadAllLines("Loans.txt");
            foreach (string readLoan in readLoans)
            {
                string[] loanitems = readLoan.Split('\u002C');
                Loans loan = new Loans(loanitems[0], loanitems[1], loanitems[2], loanitems[3]);
            }

            CLI.MainMenu();//displays the main menu from CLI.cs
        }
    }
}