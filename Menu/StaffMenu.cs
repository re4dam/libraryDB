using System;
namespace LibraryConsole;

class StaffMenu
{
    private Staff StaffService;

    public StaffMenu(Database database)
    {
        StaffService = new Staff(database);
    }

    public void Show()
    {
        bool running = true;
        while(running)
        {
            Console.Clear(); // ill consider this for a moment
            Console.WriteLine("Staff Menu");
            Console.WriteLine("1. Register Staff");
            Console.WriteLine("2. Read Staff");
            Console.WriteLine("3. Update Staff");
            Console.WriteLine("4. Delete Staff");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Select an option: ");

            string input = Console.ReadLine();

            switch(input)
            {
                case "1":
                    Console.WriteLine("You selected: Register Staff");
                    Create();
                    break;
                case "2":
                    Console.WriteLine("You selected: Read Staff");
                    Read();
                    break;
                case "3":
                    Console.WriteLine("You selected: Update Staff");
                    Update();
                    break;
                case "4":
                    Console.WriteLine("You selected: Delete Staff");
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
        Console.Write("Enter staff name: ");
        string name = Console.ReadLine();
        Console.Write("Enter staff position: ");
        string position = Console.ReadLine();

        StaffModel newStaff = new StaffModel(0, name, position);
        bool success = StaffService.Create(newStaff);

        if (success)
            Console.WriteLine("Staff registered successfully!");
        else
            Console.WriteLine("Failed to register staff.");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey(); 
    }
    public void Read()
    {
        var allStaffs = StaffService.ReadAll();
        if (allStaffs.Count == 0)
        {
            Console.WriteLine("No staffs found.");
        }
        else
        {
            foreach (var staff in allStaffs)
            {
                Console.WriteLine($"ID: {staff.Id}, Name: {staff.Name}, Position: {staff.Position}");
            }
        }
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    public void Update()
    {
        Console.Write("Enter staff ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.Write("Enter new name (leave blank to keep current): ");
            string newName = Console.ReadLine();
            string finalName = string.IsNullOrWhiteSpace(newName) ? StaffService.Read(id)?.Name : newName;
            Console.Write("Enter new position (leave blank to keep current): ");
            string newPosition = Console.ReadLine();
            string finalPosition = string.IsNullOrWhiteSpace(newPosition) ? StaffService.Read(id)?.Position : newPosition;

            StaffModel staffToUpdate = new StaffModel(id, finalName, finalPosition);
            bool success = StaffService.Update(staffToUpdate);

            if (success)
                Console.WriteLine("Staff updated successfully!");
            else
                Console.WriteLine("Failed to update staff.");
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
        Console.Write("Enter staff ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            bool success = StaffService.Delete(id);
            if (success)
                Console.WriteLine("Staff deleted successfully!");
            else
                Console.WriteLine("Failed to delete staff.");
        }
        else
        {
            Console.WriteLine("Invalid ID format.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}