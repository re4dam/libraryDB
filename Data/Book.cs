using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using LibraryConsole;

namespace LibraryConsole;

public class Book
{
    private Database database;

    public Book(Database database)
    {
        this.database = database;
    }

    public bool Create(BookModel bookModel)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "INSERT INTO Books (Title, Author) VALUES (@Title, @Author)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", bookModel.Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Author", bookModel.Author ?? (object)DBNull.Value);
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating book: {ex.Message}");
            return false;
        }
    }

    public BookModel Read(int id)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "SELECT Id, Title, Author FROM Books WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new BookModel(
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

    public List<BookModel> ReadAll()
    {
        List<BookModel> books = new List<BookModel>();
        
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "SELECT Id, Title, Author FROM Books ORDER BY Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new BookModel(
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
            Console.WriteLine($"Error reading all books: {ex.Message}");
        }
        
        return books;
    }

    public bool Update(BookModel bookModel)
    {
        try
        {
            using (SqlConnection connection = database.GetConnection())
            {
                connection.Open();
                
                string query = "UPDATE Books SET Title = @Title, Author = @Author WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", bookModel.Id);
                    command.Parameters.AddWithValue("@Title", bookModel.Title ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Author", bookModel.Author ?? (object)DBNull.Value);
                    
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating book: {ex.Message}");
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
                
                string query = "DELETE FROM Books WHERE Id = @Id";
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
