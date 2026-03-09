using System;
namespace LibraryConsole;

public class MemberModel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    public MemberModel(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

}