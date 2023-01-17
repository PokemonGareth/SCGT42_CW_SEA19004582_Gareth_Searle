using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal class Library//creates a library to stores all the data and gather the Loans, account and products together.
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Loans> Loans { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Products> Products { get; set; }

        public Library(string name, string address)
        {
            Name = name;
            Address = address;
            Loans = new List<Loans>();
            Accounts = new List<Account>();
            Products = new List<Products>();
        }

        public Account? GetAccount(string id)//used when retrieing an account
        {
            foreach (var account in Accounts)
            {
                if (account.Id == id)
                {
                    return account;

                }
            }
            return null;

        }

        public Loans? GetLoan(string id)//used when retrieing a loan
        {
            foreach (var loan in Loans)
            {
                if (loan.Id.ToString() == id)
                {
                    return loan;

                }
            }
            return null;

        }
        public Products? GetProduct(string Id)//used when retrieing a product
        {
            foreach (var product in Products)
            {
                if (product.Id == Id)
                {
                    return product;

                }
            }
            return null;

        }
    }
}
