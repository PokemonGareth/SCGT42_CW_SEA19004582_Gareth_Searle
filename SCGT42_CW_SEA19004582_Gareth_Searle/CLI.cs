using Sharprompt;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    ;
                    break;
                case "Manage Products":
                    ;
                    break;
                case "Manage Loans":
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

                string manage = Prompt.Select("Select your option", new[] { "New Loan", "View Loans", "Delete Loan" });

                switch (manage)
                {
                    case "New Loan":

                        Console.Clear();
                        string Product = Prompt.Input<string>("Enter loaning account:");
                        Products? currentProduct = library.GetProduct(Product);

                        if (currentProduct != null)
                        {

                            Account Name = Prompt.Input<Account>("Enter Loanee's Account Name:");
                            int d = Prompt.Input<int>("Enter day:");
                            int M = Prompt.Input<int>("Enter month:");
                            int y = Prompt.Input<int>("Enter year:");
                            int h = Prompt.Input<int>("Enter hour:");
                            int m = Prompt.Input<int>("Enter mins:");
                            Loans Loan = new Loans(currentProduct, Name , new DateTime(y, M, d, h, m, 0));
                            Console.WriteLine($"Booking added: \n {Loan} ");
                        }
                        else
                        {
                            Console.WriteLine("Product not found.");
                        }

                        Console.ReadLine();
                        Console.WriteLine("Prease any key to return to main menu.");
                        MainMenu();
                        break;

                    case "View Loans":
                        Console.Clear();
                        foreach (var item in library.Loans)
                        {
                            Console.WriteLine(item);
                            Console.WriteLine("--------------------");
                        }
                        Console.ReadLine();
                        Console.WriteLine("Prease any key to return to main menu.");
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

                        Console.ReadLine();
                        Console.WriteLine("Prease any key to return to main menu.");
                        MainMenu();
                        break;

                    default:
                        break;
                }
            }
        }
    }
