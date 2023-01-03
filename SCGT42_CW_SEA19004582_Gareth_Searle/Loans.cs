using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal class Loans //used to create Loans
    {
        public string Id { get; set; }
        public Products Product { get; set; }
        public Account Account { get; set; }
        public DateTime Timestamp { get; set; }

        public Loans(string id, Products product, Account account, DateTime timestamp)
        {
            Id = id;
            Product = product;
            Account = account;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            DateTime Overdue = DateTime.Today;
            if (Timestamp < Overdue)//works out how many days the loan has overrun
            {
                double daysoverdue = Overdue.Subtract(Timestamp).TotalDays;
                double extraexpenses = 0.25 * daysoverdue;
                return $"Booking ID: {Id}\nProduct details: {Product}\nAccount:{Account}\nLoan Started:{Timestamp}\n!!!Days Overdue{ daysoverdue} = +£{extraexpenses}!!!";
            }
            else
            {
                return $"Booking ID: {Id}\nProduct details: {Product}\nAccount:{Account}\nLoan Started:{Timestamp}";
            }
        }
    }
}
