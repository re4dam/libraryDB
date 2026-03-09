using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using LibraryConsole;

namespace LibraryConsole;

public class Staff
{
    private Database database;

    public Staff(Database database)
    {
        this.database = database;
    }

    public bool Create(StaffModel staffModel)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "INSERT INTO Staffs (Name, Position) VALUES (@Name, @Position)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", staffModel.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Position", staffModel.Position ?? (object)DBNull.Value);
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating staff member: {ex.Message}");
            return false;
        }
    }

    public StaffModel Read(int id)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "SELECT Id, Name, Position FROM Staffs WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StaffModel(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                            );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading staff: {ex.Message}");
        }
        
        return null;
    }

    public List<StaffModel> ReadAll()
    {
        List<StaffModel> staffs = new List<StaffModel>();
        
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "SELECT Id, Name, Position FROM Staffs ORDER BY Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            staffs.Add(new StaffModel(
                                reader.GetInt32(0),
                                reader.GetString(1),
                                reader.GetString(2)
                            ));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading all staffs: {ex.Message}");
        }
        
        return staffs;
    }

    public bool Update(StaffModel staffModel)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "UPDATE Staffs SET Name = @Name, Position = @Position WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", staffModel.Id);
                    command.Parameters.AddWithValue("@Name", staffModel.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Position", staffModel.Position ?? (object)DBNull.Value);
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating staff member: {ex.Message}");
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
                
                string query = "DELETE FROM Staffs WHERE Id = @Id";
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
