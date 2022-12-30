using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal class Loans
    {
        public Guid Id { get; set; }
        public Products Product { get; set; }
        public Account Account { get; set; }
        public DateTime Timestamp { get; set; }

        public Loans(Products product, Account account, DateTime timestamp)
        {
            Id = Guid.NewGuid();
            Product = product;
            Account = account;
            Timestamp = timestamp;
        }

        public override string ToString()
        {
            DateTime Overdue = DateTime.Today;
            if (Timestamp < Overdue)
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
