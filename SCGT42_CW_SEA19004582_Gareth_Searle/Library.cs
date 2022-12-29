using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal class Library
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

        public Account? GetAccount(string name)
        {
            foreach (var account in Accounts)
            {
                if (account.Name == name)
                {
                    return account;

                }
            }
            return null;

        }
        public Loans? GetLoan(string id)
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
        public Products? GetProduct(string name)
        {
            foreach (var product in Products)
            {
                if (product.Name == name)
                {
                    return product;

                }
            }
            return null;

        }
    }
}
