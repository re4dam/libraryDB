using System;
namespace LibraryConsole;

class LoanMenu
{
    private Loan LoanService;

    public LoanMenu(Database database)
    {
        LoanService = new Loan(database);
    }

    public void Show()
    {
        bool running = true;
        while(running)
        {
            Console.Clear(); // ill consider this for a moment
            Console.WriteLine("Member Menu");
            Console.WriteLine("1. Create Member");
            Console.WriteLine("2. Read Member");
            Console.WriteLine("3. Update Member");
            Console.WriteLine("4. Delete Member");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");

            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Console.WriteLine("You selected: Create Member");
                    Create();
                    break;
                case "2":
                    Console.WriteLine("You selected: Read Member");
                    Read();
                    break;
                case "3":
                    Console.WriteLine("You selected: Update Member");
                    Update();
                    break;
                case "4":
                    Console.WriteLine("You selected: Delete Member");
                    Delete();
                    break;
                case "5":
                    Console.WriteLine("Returning to Main Menu...");
                    running = false;
                    break;
                default:
                    Console.WriteLine($"Invalid option: {input}");
                    break;
            }
        }
    }

    public void Create()
    {
        Console.Write("Enter BookID: ");
        int bookId;
        // This loops until the user types a valid integer
        while (!int.TryParse(Console.ReadLine(), out bookId))
        {
            Console.Write("Invalid input. Please enter a valid numeric BookID: ");
        }

        Console.Write("Enter MemberID: ");
        int memberId;
        while (!int.TryParse(Console.ReadLine(), out memberId))
        {
            Console.Write("Invalid input. Please enter a valid numeric MemberID: ");
        }

        Console.Write("Enter StaffID: ");
        int staffId;
        while (!int.TryParse(Console.ReadLine(), out staffId))
        {
            Console.Write("Invalid input. Please enter a valid numeric StaffID: ");
        }

        Console.Write("Enter Borrow Date (DD-MM-YYYY): ");
        DateTime parsedBorrowDate;
        // TryParseExact ensures the user strictly follows your requested date format
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedBorrowDate))
        {
            Console.Write("Invalid format. Please enter date as DD-MM-YYYY: ");
        }

        Console.Write("Enter borrow duration (in days): ");
        int duration;
        while (!int.TryParse(Console.ReadLine(), out duration))
        {
            Console.Write("Invalid input. Please enter a number of days: ");
        }

        // Calculate the due date based on the user's inputted borrow date, not DateTime.Now
        DateTime dueDate = parsedBorrowDate.AddDays(duration);

        // Now you can pass bookId, memberId, staffId, parsedBorrowDate, and dueDate to your Data layer!
        Console.WriteLine($"\nSuccess! Book {bookId} borrowed until {dueDate.ToString("dd-MM-yyyy")}");

        LoanModel newLoan = new LoanModel(0, bookId, memberId, staffId, parsedBorrowDate, dueDate);
        bool success = LoanService.Create(newLoan);

        if (success)
            Console.WriteLine("Member created successfully!");
        else
            Console.WriteLine("Failed to create member.");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void Read()
    {
        var allLoans = LoanService.ReadAll();
        if (allLoans.Count == 0)
        {
            Console.WriteLine("No loans found.");
        }
        else
        {
            foreach (var loan in allLoans)
            {
                Console.WriteLine($"ID: {loan.Id}, Book Name: {loan.BookName}, Member Name: {loan.MemberName}, Staff Name: {loan.StaffName}, Borrow Date: {loan.BorrowDate}, Due Date: {loan.DueDate}");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void Update()
    {
        Console.Write("Enter Loan ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            /*
            - Book, Member, and Staff
            - Date of Borrow and Return
            */

            Console.Write("Enter new BookID: ");
            int bookId;
            while (!int.TryParse(Console.ReadLine(), out bookId))
            {
                Console.Write("Invalid input. Please enter a valid numeric BookID: ");
            }

            Console.Write("Enter new MemberID: ");
            int memberId;
            while (!int.TryParse(Console.ReadLine(), out memberId))
            {
                Console.Write("Invalid input. Please enter a valid numeric MemberID: ");
            }

            Console.Write("Enter new StaffID: ");
            int staffId;
            while (!int.TryParse(Console.ReadLine(), out staffId))
            {
                Console.Write("Invalid input. Please enter a valid numeric StaffID: ");
            }

            Console.Write("Enter new Borrow Date (DD-MM-YYYY): ");
            DateTime parsedBorrowDate;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedBorrowDate))
            {
                Console.Write("Invalid format. Please enter date as DD-MM-YYYY: ");
            }

            Console.Write("Enter borrow duration (in days): ");
            DateTime parsedDueDate;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out parsedDueDate))
            {
                Console.Write("Invalid format. Please enter date DD-MM-YYYY: ");
            }

            LoanModel loanToUpdate = new LoanModel(id, bookId, memberId, staffId, parsedBorrowDate, parsedDueDate);
            bool success = LoanService.Update(loanToUpdate);

            if (success)
                Console.WriteLine("Loan updated successfully!");
            else
                Console.WriteLine("Failed to update laon.");
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void Delete()
    {
        Console.Write("Enter Loan ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            bool success = LoanService.Delete(id);
            if (success)
                Console.WriteLine("Loan deleted successfully!");
            else
                Console.WriteLine("Failed to delete loan.");
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}