using System;
namespace LibraryConsole;

/*
Book tables Management
Allowing features to Create new book, Show all books,
Update existing book, and delete book
*/

class BookMenu
{
    private Book BookService;

    public BookMenu(Database database)
    {
        BookService = new Book(database);
    }

    public void Show()
    {
        bool running = true;
        while(running)
        {
            Console.Clear(); // ill consider this for a moment
            Console.WriteLine("Book Menu");
            Console.WriteLine("1. Create Book");
            Console.WriteLine("2. Read Book");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Delete Book");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");

            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Console.WriteLine("You selected: Create Book");
                    Create();
                    break;
                case "2":
                    Console.WriteLine("You selected: Read Book");
                    Read();
                    break;
                case "3":
                    Console.WriteLine("You selected: Update Book");
                    Update();
                    break;
                case "4":
                    Console.WriteLine("You selected: Delete Book");
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
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();
        Console.Write("Enter book author: ");
        string author = Console.ReadLine();

        BookModel newBook = new BookModel(0, title, author);
        bool success = BookService.Create(newBook);

        if (success)
            Console.WriteLine("Book created successfully!");
        else
            Console.WriteLine("Failed to create book.");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void Read()
    {
        var allBooks = BookService.ReadAll();
        if (allBooks.Count == 0)
        {
            Console.WriteLine("No books found.");
        }
        else
        {
            foreach (var book in allBooks)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void Update()
    {
        Console.Write("Enter book ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Enter new title (leave blank to keep current): ");
            string newTitle = Console.ReadLine();
            string finalTitle = string.IsNullOrWhiteSpace(newTitle) ? BookService.Read(id)?.Title : newTitle;
            Console.Write("Enter new author (leave blank to keep current): ");
            string newAuthor = Console.ReadLine();
            string finalAuthor = string.IsNullOrWhiteSpace(newAuthor) ? BookService.Read(id)?.Author : newAuthor;

            BookModel bookToUpdate = new BookModel(id, finalTitle, finalAuthor);
            bool success = BookService.Update(bookToUpdate);

            if (success)
                Console.WriteLine("Book updated successfully!");
            else
                Console.WriteLine("Failed to update book.");
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
        Console.Write("Enter book ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            bool success = BookService.Delete(id);
            if (success)
                Console.WriteLine("Book deleted successfully!");
            else
                Console.WriteLine("Failed to delete book.");
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}