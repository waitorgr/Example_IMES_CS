using System;
using Microsoft.Data.Sqlite;

public class EmulationIMES
{
    private string _connectionString;

    public EmulationIMES(string connectionString)// Constructor to initialize EmulationIMES with a database connection string
    {
        _connectionString = $"Data Source={connectionString}";
    }

    public void Login(string workerCodIMES)// Method to simulate login using WorkerCodIMES
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            // Query to retrieve employee data based on WorkerCodIMES
            string query = $"SELECT * FROM Employees WHERE WorkerCodIMES = '{workerCodIMES}'";

            using (var command = new SqliteCommand(query, connection))
            {
                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Retrieve employee data
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string factory = reader.GetString(2);
                        string departmentName = reader.GetString(3);
                        string IMES = reader.GetString(4);
                        string workerCodIMESResult = reader.GetString(5);

                        // Print employee information
                        Console.WriteLine($"ID: {id}");
                        Console.WriteLine($"Name: {name}");
                        Console.WriteLine($"Factory: {factory}");
                        Console.WriteLine($"Department Name: {departmentName}");
                        Console.WriteLine($"IMES: {IMES}");
                        Console.WriteLine($"WorkerCodIMES: {workerCodIMESResult}");
                    }
                    else
                    {
                        Console.WriteLine("User not found");
                    }
                }
            }
        }
    }
}
