using System;
namespace LibraryConsole;

public class BookModel
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }

    public BookModel(int id, string title, string author)
    {
        Id = id;
        Title = title;
        Author = author;
    }
}