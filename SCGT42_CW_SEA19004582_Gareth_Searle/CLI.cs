﻿using Microsoft.VisualBasic;
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
        public static Library? library { get; set; }//gets the contents of Library and stores it into a local variable
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"Welcome to The Word Wagon!");
            Console.WriteLine("--------------------------------");

            string manage = Prompt.Select("Select your option", new[] { "Manage Accounts", "Manage Products", "Manage Loans" , "View Overdue Loans", "Exit"});//allows the user to choose an option

            switch (manage)//used to work out what the user wants to do
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
                case "View Overdue Loans":
                    Console.Clear();
                    overdueLoans();
                    break;
                case "Exit":
                    break;
                default:
                    break;
            }
        }



        private static void manageLoans()
        {
            Console.WriteLine($"Loans Manager");
            Console.WriteLine("---------------");

            string manage = Prompt.Select("Select your option", new[] { "New Loan", "View Loans", "Edit Loans", "Delete Loan" });//allows the user to chose what they want to do to the loans database

            switch (manage)
            {
                case "New Loan":

                    Console.Clear();//clears the console
                    string Product = Prompt.Input<string>("Enter Product ID");
                    Products? currentProduct = library.GetProduct(Product);//gets details of the product to be loaned

                    if (currentProduct != null && currentProduct.Status == "true")//checks whether the user input was not empty and the product is available
                    {
                        string Id = Prompt.Input<string>("Enter Id");
                        string AccountIDstr = Prompt.Input<string>("Enter Loanee's Account ID");//gathers the data to create a new loan
                        Account? AccountID = library.GetAccount(AccountIDstr);
                        Console.WriteLine("Now Please enter the return date");
                        int d = Prompt.Input<int>("Enter day");
                        int M = Prompt.Input<int>("Enter month");
                        int y = Prompt.Input<int>("Enter year");
                        int h = Prompt.Input<int>("Enter hour");
                        int m = Prompt.Input<int>("Enter mins");
                        Loans Loan = new Loans(Id, currentProduct, AccountID, new DateTime(y, M, d, h, m, 00));//creates a new loan

                        currentProduct.Status = "false";//changes the product status

                        var loanFile = new StreamWriter("Loans.txt", append: true);
                        loanFile.WriteLineAsync($"{Id},{currentProduct.Id},{AccountID.Id},{new DateTime(y, M, d, h, m, 0)}");//appends the loan to a loans text file
                        loanFile.Close();

                        Console.WriteLine($"Loan added: \n {Loan} ");
                    }
                    else if (currentProduct != null && currentProduct.Status != "true")
                    {
                        Console.WriteLine("Product already Loaned");//displays that the product is already bieng loaned
                    }
                    else
                    {
                        Console.WriteLine("Product not found.");//displays wether the input was blank
                    }

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "View Loans":
                    Console.Clear();
                    if (library.Loans.Count != 0)
                    {
                        foreach (Loans loa in library.Loans)//loops through the Loans displaying each one
                        {
                            Console.WriteLine(loa);
                            Console.WriteLine("--------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No loans Found");
                    }
                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "Edit Loans":
                    Console.Clear();
                    string loannumber = Prompt.Input<string>("Enter Loan number");
                    Loans? edit = library.GetLoan(loannumber);//gathers data for the chosen loan

                    if (edit != null)
                    {
                        string editString = edit.ToString(); //converts data type to string
                        var oldId = edit.Id;
                        var oldProduct = edit.Product;//stores all other data from the loan bieng edited
                        var oldAccount = edit.Account;

                        var tempFile = Path.GetTempFileName();
                        var linesToKeep = File.ReadLines("Loans.txt").Where(i => i != editString);//removes loan from txt file

                        File.WriteAllLines(tempFile, linesToKeep);

                        File.Delete("Loans.txt");
                        File.Move(tempFile, "Loans.txt");

                        int newYear = Prompt.Input<int>("Please enter new Due Year");//gathers data to update the loan
                        int newMonth = Prompt.Input<int>("Please enter new Due Month");
                        int newDay = Prompt.Input<int>("Please enter new Due Day");
                        int newHour = Prompt.Input<int>("Please enter new Due Hour");
                        int newMinute = Prompt.Input<int>("Please enter new Due Minute");
                        Loans editedLoan = new Loans(oldId, oldProduct, oldAccount, new DateTime(newYear, newMonth, newDay, newHour, newMinute, 0));//modifies the time and date of the loan

                        var loanFile = new StreamWriter("Loans.txt", append: true);
                        loanFile.WriteLineAsync($"{oldProduct},{oldAccount},{new DateTime(newYear, newMonth, newDay, newHour, newMinute, 0)}");//appends the loan to a loans text file
                        loanFile.Close();

                        Console.WriteLine("Details have been updated.");
                    }
                    else
                    {
                        Console.WriteLine("Loan reference not found.");//displays whether the loan ID could be found
                    }
                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "Delete Loan":
                    Console.Clear();
                    string loanid = Prompt.Input<string>("Enter loan ID");
                    Loans? current = library.GetLoan(loanid);//locates loan

                    if (current != null)
                    {
                        Console.WriteLine(current.ToString());
                        string? currentString = current.ToString();
                        Console.WriteLine("------------------------------------------------------------------------");
                        var sure = Prompt.Confirm("Are you sure that you would like to delete this Loan?");//confirms the deletion
                        if (sure == true)
                        {

                            library.Loans.Remove(current);//removes loan from library

                            var file = new StreamWriter("Loans.txt", append: false);

                            foreach (Loans Loa in library.Loans)
                            {

                                file.WriteLine($"{Loa.Id},{Loa.Product},{Loa.Account},{Loa.Timestamp}");//adds all account details to loan.txt

                            }

                            file.Close();

                            Console.WriteLine("Loan removed.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Loan not found.");
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

            string manage = Prompt.Select("Select your option", new[] { "Create Account", "View Accounts", "Delete Accounts" });//allows the user to chose what they want to do to the Accounts database

            switch (manage)
            {
                case "Create Account":

                    Console.Clear();//clears console

                    string Id = Prompt.Input<string>("Enter Id");
                    string name = Prompt.Input<string>("Enter Account Name");//gathers data to create a new account
                    string email = Prompt.Input<string>("Enter Account Email");
                    bool Loop = true;
                    if (email.Contains('@') && email.Contains('.')){
                        Loop = false;
                    }
                    while (Loop == true){
                        email = Prompt.Input<string>("Must be email:");
                        if (email.Contains('@') && email.Contains('.')){
                            Loop = false;
                        }
                    }
                    string address = Prompt.Input<string>("Enter Account Address");
                    Account account = new Account(Id, name, email, address);//creates new account

                    var accountFile = new StreamWriter("Accounts.txt", append: true);
                    accountFile.WriteLine($"{Id},{name},{email},{address}");//adds account details to Account.txt
                    accountFile.Close();

                    Console.WriteLine($"Account Creation Complete:\n{account} ");

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "View Accounts":

                    Console.Clear();//clears console

                    if (library.Accounts.Count() != 0)
                    {

                        foreach (Account acc in library.Accounts)
                        {
                            Console.WriteLine(acc);
                            Console.WriteLine("--------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Accounts found");
                    }

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "Delete Accounts":
                    Console.Clear();
                    string accountName = Prompt.Input<string>("Enter Account Name");
                    Account? current = library.GetAccount(accountName);//locates account

                    if (current != null)
                    {
                        Console.WriteLine(current.ToString());
                        string? currentString = current.ToString();
                        Console.WriteLine("------------------------------------------------------------------------");
                        var sure = Prompt.Confirm("Are you sure that you would like to delete this Account?");//confirms the deletion
                        if (sure == true)
                        {

                            library.Accounts.Remove(current);//removes account from library

                            var file = new StreamWriter("Accounts.txt", append: false);  

                            foreach (Account acc in library.Accounts)
                            {

                                file.WriteLine($"{acc.Id},{acc.Name},{acc.Email},{acc.Address}");//adds account details to Account.txt
                          
                            }
                            
                            file.Close();
                          
                            Console.WriteLine("Account removed.");
                        }
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

            string manage = Prompt.Select("Select your option", new[] { "Create New Product", "View Products", "Delete Products" });//lets the user chose an option

            switch (manage)
            {
                case "Create New Product":

                    Console.Clear();

                    string Id = Prompt.Input<string>("Enter Id");
                    string name = Prompt.Input<string>("Enter Product Name");//gathers data to add to the product
                    string type = Prompt.Input<string>("Enter Product Type");
                    Products Product = new Products(Id, name, "true", type);

                    var productFile = new StreamWriter("Products.txt", append: true);
                    productFile.WriteLineAsync($"{Id},{name},'true',{type}");//adds account details to Products.txt
                    productFile.Close();

                    Console.WriteLine($"Product Registered: \n {Product} ");

                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "View Products":
                    Console.Clear();
                    if (library.Products.Count() != 0)
                    {
                        foreach (Products prod in library.Products)//loops through displaying each product
                        {
                            Console.WriteLine(prod);
                            Console.WriteLine("--------------------");
                        }
                    }
                    Console.WriteLine("Prease any key to return to main menu.");
                    Console.ReadLine();
                    MainMenu();
                    break;

                case "Delete Products":
                    Console.Clear();
                    string Productid = Prompt.Input<string>("Enter product ID");
                    Products? current = library.GetProduct(Productid);//locates loan

                    if (current != null)
                    {
                        Console.WriteLine(current.ToString());
                        string? currentString = current.ToString();
                        Console.WriteLine("------------------------------------------------------------------------");
                        var sure = Prompt.Confirm("Are you sure that you would like to delete this Product?");//confirms the deletion
                        if (sure == true)
                        {

                            library.Products.Remove(current);//removes loan from library

                            var file = new StreamWriter("Products.txt", append: false);

                            foreach (Products pro in library.Products)
                            {

                                file.WriteLine($"{pro.Id},{pro.Name},{pro.Status},{pro.Type}");//adds all account details to loan.txt

                            }

                            file.Close();

                            Console.WriteLine("Product removed.");
                        }
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

        private static void overdueLoans()
        {
            Console.Clear();
            int count = 0;
            foreach (var item in library.Loans)//loops through the Loans
            {
                if (item.Timestamp < DateTime.Now)//cheks whether Loan is overdue
                Console.WriteLine(item);
                count++;
                Console.WriteLine("--------------------");
            }

            if (count == 0)//checks whether there were any over due and if not then displays No Overdue Loans
            {
                Console.WriteLine("There are no Overdue Loans :)\n");
                Console.WriteLine("-------------------------------------------\n");
            }
            Console.WriteLine("Prease any key to return to main menu.");
            Console.ReadLine();
            MainMenu();
        }
    }
}
