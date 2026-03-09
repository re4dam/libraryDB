using System;
using Microsoft.Data.SqlClient;
namespace LibraryConsole;

public class Database
{
    private string connectionString;

    public Database(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}