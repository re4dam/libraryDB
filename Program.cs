using System;
namespace LibraryConsole;

/*
The entry point of program
Handles the accessing of each table management
*/

class Program
{
    static readonly string connectionString = "Server=localhost;Database=LibraryDB;Integrated Security=true;TrustServerCertificate=true;";
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Library Management System!");

        Database db = new Database(connectionString);
        BookMenu bookMenu = new BookMenu(db);
        MemberMenu memberMenu = new MemberMenu(db);
        StaffMenu staffMenu = new StaffMenu(db);
        LoanMenu loanMenu = new LoanMenu(db);

        bool running = true;
        while(running)
        {
            Console.Clear();
            Console.WriteLine("=== Main Menu ===");
            Console.WriteLine("1. Book Management");
            Console.WriteLine("2. Member Management");
            Console.WriteLine("3. Staff Management");
            Console.WriteLine("4. Loan Management");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");
            string input = Console.ReadLine();
            // Process input
            switch (input)
            {
                case "1":
                    bookMenu.Show(); // Delegates logic to BookMenu
                    break;
                case "2":
                    memberMenu.Show(); // Delegates logic to MemberMenu
                    break;
                case "3":
                    staffMenu.Show(); // Delegates logic to StaffMenu
                    break;
                case "4":
                    loanMenu.Show();
                    break;
                case "5":
                    Console.WriteLine("Exiting the system. Goodbye!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to try again...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}