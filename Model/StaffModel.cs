using System;
namespace LibraryConsole;

public class StaffModel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Position { get; private set; }

    public StaffModel(int id, string name, string position)
    {
        Id = id;
        Name = name;
        Position = position;
    }

}