using System;
using Example_IMES;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class DatabaseManager : DbContext
{
    private string _mainDbConnectionString;
    private string _newDbConnectionString;
    private List<string> _dbPaths;

    public DatabaseManager(string newDbPath, List<string> dbPaths)// Constructor to initialize the database manager with the path of the new database and a list of existing database paths
    {
        if (System.IO.File.Exists(newDbPath))
        {
            System.IO.File.Delete(newDbPath);
        }
        _newDbConnectionString = $"Data Source={newDbPath}";
        _dbPaths = dbPaths;
    }

    public void MergeDatabases()// Method to merge databases
    {
        // Create a new database
        using (SqliteConnection newConnection = new SqliteConnection(_newDbConnectionString))
        {
            newConnection.Open();

            // Create necessary tables in the new database
            CreateTables(newConnection);

            // Merge data from all databases
            foreach (string dbPath in _dbPaths)
            {
                using (SqliteConnection oldConnection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    oldConnection.Open();

                    // Read data from old database
                    string query = "SELECT id, name, factory, departmentName FROM Employees;";
                    using (SqliteCommand command = new SqliteCommand(query, oldConnection))
                    {
                        using (SqliteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Insert data into new database
                                InsertData(newConnection, reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                            }
                        }
                    }
                }
            }
        }
    }

    private void CreateTables(SqliteConnection connection)// Method to create tables in the database
    {
        string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Employees (
                id INTEGER NOT NULL,
                name TEXT NOT NULL,
                factory TEXT NOT NULL,
                departmentName TEXT NOT NULL
            );";

        using (SqliteCommand command = new SqliteCommand(createTableQuery, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    private void InsertData(SqliteConnection connection, int id, string name, string factory, string departmentName) // Method to insert data into the database
    {
        string insertQuery = @"
            INSERT INTO Employees (id, name, factory, departmentName) 
            VALUES ($id, $name, $factory, $departmentName);";

        using (SqliteCommand command = new SqliteCommand(insertQuery, connection))
        {
            command.Parameters.AddWithValue("$id", id);
            command.Parameters.AddWithValue("$name", name);
            command.Parameters.AddWithValue("$factory", factory);
            command.Parameters.AddWithValue("$departmentName", departmentName);

            command.ExecuteNonQuery();
        }
    }

    public void SaveEmployeesToDatabase(List<Employee> employees,string dbFilePath)// Method to save employees to a database
    {
        if (System.IO.File.Exists(dbFilePath))
        {
            System.IO.File.Delete(dbFilePath);
        }
        string connectionString = $"Data Source={dbFilePath}";
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            // Create Employees table if it does not exist
            var command = connection.CreateCommand();
            command.CommandText = @"CREATE TABLE IF NOT EXISTS Employees (
                                        id INTEGER NOT NULL,
                                        name TEXT NOT NULL,
                                        factory TEXT NOT NULL,
                                        departmentName TEXT NOT NULL,
                                        IMES TEXT NOT NULL,
                                        WorkerCodIMES TEXT NOT NULL
                                    )";

            command.ExecuteNonQuery();
        }
        // Insert employees into the database
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                foreach (var employee in employees)
                {
                    var command = connection.CreateCommand();
                    command.CommandText = $"INSERT INTO Employees (id, name, factory, departmentName, IMES,WorkerCodIMES) VALUES ({employee.id}, '{employee.name}', '{employee.factory}', '{employee.departmentName}', '{employee.IMES}', '{employee.WorkerCodIMES}')";
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
            }
        }
    }
}
