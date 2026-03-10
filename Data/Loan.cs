using System;
using Microsoft.Data.SqlClient;
namespace LibraryConsole;

class Loan
{
    private Database database;

    public Loan(Database database)
    {
        this.database = database;
    }

    public bool Create(LoanModel loanModel)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO Loans (BookId, MemberId, StaffId, BorrowDate, DueDate) VALUES (@BookId, @MemberId, @StaffId, @BorrowDate, @DueDate)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookId", (object)loanModel.BookId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@MemberId", (object)loanModel.MemberId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@StaffId", (object)loanModel.StaffId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@BorrowDate", (object)loanModel.BorrowDate ?? DateTime.Now);
                    command.Parameters.AddWithValue("@DueDate", (object)loanModel.DueDate ?? DateTime.Now.AddDays(7)); 

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        } catch (Exception ex)
        {
            Console.WriteLine($"Error creating member: {ex.Message}");
            return false;
        }
    }
    public LoanDisplayModel? Read(int id)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                string query = @"
                SELECT 
                    l.Id AS LoanId,
                    b.Title AS BookTitle,
                    m.Name AS MemberName,
                    s.Name AS StaffName,
                    l.BorrowDate,
                    l.DueDate
                FROM Loans l WHERE Id = @Id
                INNER JOIN Books b ON l.BookId = b.Id
                INNER JOIN Members m ON l.MemberId = m.Id
                INNER JOIN Staffs s ON l.StaffId = s.Id
                ORDER BY l.Id;";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new LoanDisplayModel(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetDateTime(4),
                                reader.GetDateTime(5)
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading loan: {ex.Message}");
        }
        
        return null;
    }
    public List<LoanDisplayModel> ReadAll()
    {
        List<LoanDisplayModel> loans = new List<LoanDisplayModel>();
        
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = @"
                SELECT 
                    l.Id AS LoanId,
                    b.Title AS BookTitle,
                    m.Name AS MemberName,
                    s.Name AS StaffName,
                    l.BorrowDate,
                    l.DueDate
                FROM Loans l
                INNER JOIN Books b ON l.BookId = b.Id
                INNER JOIN Members m ON l.MemberId = m.Id
                INNER JOIN Staffs s ON l.StaffId = s.Id
                ORDER BY l.Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            loans.Add(new LoanDisplayModel(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                reader.GetDateTime(4),
                                reader.GetDateTime(5)
                            ));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading all loans: {ex.Message}");
        }
        return loans;
    }
    public bool Update(LoanModel loanModel)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "UPDATE Loans SET BookId = @BookId, MemberId = @MemberId, StaffId = @StaffId, BorrowDate = @BorrowDate, DueDate = @DueDate WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookId", (object)loanModel.BookId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@MemberId", (object)loanModel.MemberId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@StaffId", (object)loanModel.StaffId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@BorrowDate", (object)loanModel.BorrowDate ?? DateTime.Now);
                    command.Parameters.AddWithValue("@DueDate", (object)loanModel.DueDate ?? DateTime.Now.AddDays(7));
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating member: {ex.Message}");
            return false;
        }
    }
    public bool Delete(int id)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "DELETE FROM Loans WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting book: {ex.Message}");
            return false;
        }
    }
}