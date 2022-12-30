using Microsoft.VisualBasic;
using Sharprompt;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TWW
{
    internal static class CLI
    {
        public static Library? library { get; set; }
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to The Word Wagon!");
            Console.WriteLine("--------------------------------");

            string manage = Prompt.Select("Select your option", new[] { "Manage Accounts", "Manage Products", "Manage Loans" });

            switch (manage)
            {
                case "Manage Accounts":
                    Console.Clear();
                    manageAccounts();
                    break;
                case "Manage Products":
                    Console.Clear();
                    manageProducts();
                    break;
                case "Manage Loans":
                    Console.Clear();
                    manageLoans();
                    break;
                default:
                    break;
            }
        }

        private static void manageLoans()
        {
            Console.WriteLine($"Loans Manager");
            Console.WriteLine("---------------");

            string manage = Prompt.Select("Select your option", new[] { "New Loan", "View Loans", "Edit Loans", "Delete Loan" });

            switch (manage)
            {
                case "New Loan":

                    Console.Clear();
                    string Product = Prompt.Input<string>("Enter Product:");
                    Products? currentProduct = library.GetProduct(Product);

                    if (currentProduct != null)
                    {

                        Account Name = Prompt.Input<Account>("Enter Loanee's Account Name:");
                        Console.WriteLine("Now Please enter the return date:");
                        int d = Prompt.Input<int>("Enter day:");
                        int M = Prompt.Input<int>("Enter month:");
                        int y = Prompt.Input<int>("Enter year:");
                        int h = Prompt.Input<int>("Enter hour:");
                        int m = Prompt.Input<int>("Enter mins:");
                        Loans Loan = new Loans(currentProduct, Name, new DateTime(y, M, d, h, m, 0));
                        Console.WriteLine($"Loan added: \n {Loan} ");
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");
                    }

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "View Loans":
                    Console.Clear();
                    foreach (var item in library.Loans)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine("--------------------");
                    }
                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "Edit Loans":
                    Console.Clear();
                    string loannumber = Prompt.Input<string>("Enter Loan number:");
                    Loans? edit = library.GetLoan(loannumber);

                    if (edit != null)
                    {
                        int newYear = Prompt.Input<int>("Please enter new Due Year");
                        int newMonth = Prompt.Input<int>("Please enter new Due Month");
                        int newDay = Prompt.Input<int>("Please enter new Due Day");
                        int newHour = Prompt.Input<int>("Please enter new Due Hour");
                        int newMinute = Prompt.Input<int>("Please enter new Due Minute");
                        edit.Timestamp = new DateTime(newYear, newMonth, newDay, newHour, newMinute, 0);

                        Console.WriteLine("Details have been updated.");
                    }
                    else
                    {
                        Console.WriteLine("Loan reference not found.");
                    }
                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "Delete Loan":
                    Console.Clear();
                    string loanID = Prompt.Input<string>("Enter Loan number:");
                    Loans? current = library.GetLoan(loanID);

                    if (current != null)
                    {
                        library.Loans.Remove(current);
                        Console.WriteLine("Loan terminated.");
                    }
                    else
                    {
                        Console.WriteLine("Loan reference not found.");
                    }

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                default:
                    break;
            }
        }
        private static void manageAccounts()
        {
            Console.WriteLine($"Account Manager");
            Console.WriteLine("---------------");

            string manage = Prompt.Select("Select your option", new[] { "Create Account", "View Accounts", "Delete Accounts" });

            switch (manage)
            {
                case "Create Account":

                    Console.Clear();

                    string name = Prompt.Input<string>("Enter Account Name:");
                    string email = Prompt.Input<string>("Enter Account Email:");
                    string address = Prompt.Input<string>("Enter Account Address:");
                    Account account = new Account(name, email, address);
                    Console.WriteLine($"Account Creation Complete: \n {account} ");

                    Console.ReadLine();
                    Console.WriteLine("Prease any key to return to main menu.");
                    MainMenu();
                    break;

                case "View Accounts":
                    Console.Clear();
                    foreach (var item in library.Accounts)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine("--------------------");
                    }
                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "Delete Account":
                    Console.Clear();
                    string accountName = Prompt.Input<string>("Enter Account Name:");
                    Account? current = library.GetAccount(accountName);

                    if (current != null)
                    {
                        library.Accounts.Remove(current);
                        Console.WriteLine("Account removed.");
                    }
                    else
                    {
                        Console.WriteLine("Account not found.");
                    }

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                default:
                    break;
            }
        }
        private static void manageProducts()
        {
            Console.WriteLine($"Products Manager");
            Console.WriteLine("---------------");

            string manage = Prompt.Select("Select your option", new[] { "Create New Product", "View Produts", "Delete Products" });

            switch (manage)
            {
                case "Create New Product":

                    Console.Clear();

                    string name = Prompt.Input<string>("Enter Product Name:");
                    Products Product = new Products(name);
                    Console.WriteLine($"Product Registered: \n {Product} ");

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "View Product":
                    Console.Clear();
                    foreach (var item in library.Products)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine("--------------------");
                    }
                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "Delete Product":
                    Console.Clear();
                    string ProductName = Prompt.Input<string>("Enter Product Name:");
                    Products? current = library.GetProduct(ProductName);

                    if (current != null)
                    {
                        library.Products.Remove(current);
                        Console.WriteLine("Product removed.");
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");
                    }

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                default:
                    break;
            }
        }
    }
}
