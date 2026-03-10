using System;
namespace LibraryConsole;

public class LoanModel
{
    public int Id { get; private set; }
    public int BookId { get; private set; }
    public int MemberId { get; private set; }
    public int StaffId { get; private set; }
    public DateTime BorrowDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public LoanModel(int id, int bookid, int memberid, int staffid, DateTime borrowDate, DateTime dueDate)
    {
        Id = id;
        BookId = bookid;
        MemberId = memberid;
        StaffId = staffid;
        BorrowDate = borrowDate;
        DueDate = dueDate;
    }

}