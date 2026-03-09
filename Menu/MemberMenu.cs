using System;
namespace LibraryConsole;

class MemberMenu
{
    private Member MemberService;

    public MemberMenu(Database database)
    {
        MemberService = new Member(database);
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
        Console.Write("Enter member name: ");
        string name = Console.ReadLine();
        Console.Write("Enter member email: ");
        string email = Console.ReadLine();

        MemberModel newMember = new MemberModel(0, name, email);
        bool success = MemberService.Create(newMember);

        if (success)
            Console.WriteLine("Member created successfully!");
        else
            Console.WriteLine("Failed to create member.");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void Read()
    {
        var allMembers = MemberService.ReadAll();
        if (allMembers.Count == 0)
        {
            Console.WriteLine("No members found.");
        }
        else
        {
            foreach (var member in allMembers)
            {
                Console.WriteLine($"ID: {member.Id}, Name: {member.Name}, Email: {member.Email}");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void Update()
    {
        Console.Write("Enter member ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Enter new name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            string finalName = string.IsNullOrWhiteSpace(newName) ? MemberService.Read(id)?.Name : newName;
            string Email = MemberService.Read(id)?.Email;
            
            MemberModel memberToUpdate = new MemberModel(id, finalName, Email);
            bool success = MemberService.Update(memberToUpdate);

            if (success)
                Console.WriteLine("Member updated successfully!");
            else
                Console.WriteLine("Failed to update member.");
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
        Console.Write("Enter member ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            bool success = MemberService.Delete(id);
            if (success)
                Console.WriteLine("Member deleted successfully!");
            else
                Console.WriteLine("Failed to delete member.");
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}