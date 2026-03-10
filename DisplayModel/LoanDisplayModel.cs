using System;
namespace LibraryConsole;

class LoanDisplayModel
{
    public int Id { get; private set; }
    public string BookName { get; private set; }
    public string MemberName { get; private set; }
    public string StaffName { get; private set; }
    public DateTime BorrowDate { get; private set; }
    public DateTime DueDate { get; private set; }

    public LoanDisplayModel(int id, string bookName, string memberName, string staffName, DateTime borrowDate, DateTime dueDate)
    {
        Id = id;
        BookName = bookName;
        MemberName = memberName;
        StaffName = staffName;
        BorrowDate = borrowDate;
        DueDate = DueDate;
    }
}