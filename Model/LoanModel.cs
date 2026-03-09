using System;
namespace LibraryConsole;

public class LoanModel
{
    public int Id { get; private set; }
    public int BookId { get; private set; }
    public int MemberId { get; private set; }
    public int StaffId { get; private set; }
    public DateTime LoanDate { get; private set; }

    public LoanModel(int id, int bookId, int memberId, int staffId, DateTime loanDate)
    {
        Id = id;
        BookId = bookId;
        MemberId = memberId;
        StaffId = staffId;
        LoanDate = loanDate;
    }

}