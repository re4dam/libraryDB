using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using LibraryConsole;

namespace LibraryConsole;

public class Member
{
    private Database database;

    public Member(Database database)
    {
        this.database = database;
    }

    public bool Create(MemberModel memberModel)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "INSERT INTO Members (Name, Email) VALUES (@Name, @Email)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", memberModel.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", memberModel.Email ?? (object)DBNull.Value);
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating member: {ex.Message}");
            return false;
        }
    }
    public MemberModel? Read(int id)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "SELECT Id, Name, Email FROM Members WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new MemberModel(
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
            Console.WriteLine($"Error reading book: {ex.Message}");
        }
        
        return null;
    }
    public List<MemberModel> ReadAll()
    {
        List<MemberModel> members = new List<MemberModel>();
        
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "SELECT Id, Name, Email FROM Members ORDER BY Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            members.Add(new MemberModel(
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
            Console.WriteLine($"Error reading all members: {ex.Message}");
        }
        
        return members;
    }
    public bool Update(MemberModel memberModel)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "UPDATE Members SET Name = @Name, Email = @Email WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", memberModel.Id);
                    command.Parameters.AddWithValue("@Name", memberModel.Name ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Email", memberModel.Email ?? (object)DBNull.Value);
                    
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
                
                string query = "DELETE FROM Members WHERE Id = @Id";
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
