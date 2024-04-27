using Example_IMES;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;


internal class ProcessingData
{
    private List<Employee> employees = new();// List to store employees


    public ProcessingData(string connectionString) // Constructor to read employees from the database upon initialization
    {
        ReadEmployeesFromDatabase($@"Data Source={connectionString}");
    }

    private void ReadEmployeesFromDatabase(string connectionString)// Method to read employees from the database
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT id, name, factory, departmentName FROM Employees";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string factory = reader.GetString(2);
                    string departmentName = reader.GetString(3);

                    AddEmployee(name, factory, departmentName, id);
                }
            }
        }
    }

    public void AddEmployee(string Name, string Factory, string DepartmentName, int id)// Method to add an employee to the list
    {
        var employee = new Employee(Name, Factory, DepartmentName, id);
        employees.Add(employee);
    }

    public void PrintEmployeesTable()// Method to print the employees table
    {
        Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-20} {4,-20} {5,-20}", "ID", "Name", "Factory", "Department", "IMES", "WorkerCodIMES");
        Console.WriteLine(new string('-', 100));

        foreach (var employee in employees)
        {
            Console.WriteLine("{0,-5} {1,-20} {2,-20} {3,-20} {4,-20} {5,-20}", employee.id, employee.name, employee.factory, employee.departmentName, employee.IMES, employee.WorkerCodIMES);
        }
    }
    public void GenerateUNIQIMES()// Method to generate unique IMES for each employee
    {
        HashSet<string> generatedIMES = new HashSet<string>();

        foreach (var employee in employees)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            string IMES;

            
            do
            {
                StringBuilder builder = new StringBuilder(20);
                for (int i = 0; i < 20; i++)
                {
                    builder.Append(chars[random.Next(chars.Length)]);
                }
                IMES = builder.ToString();
            } while (!generatedIMES.Add(IMES)); 

            employee.addIMES(IMES);
        }
    }
    public void GenerateWorkerCodIMES()// Method to generate Worker Code for IMES
    {
        Dictionary<int, int> idCounter = new Dictionary<int, int>();

        foreach (var employee in employees)
        {
            if (!idCounter.ContainsKey(employee.id))
            {
                idCounter[employee.id] = 0;
            }

            string keySuffix = IntToBase26(idCounter[employee.id]); 
            employee.AddWorkerCodIMES(Convert.ToString(employee.id + keySuffix));

            idCounter[employee.id]++;
        }
    }
    static private string IntToBase26(int value)// Method to convert an integer to base 26
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz";
        string result = "";

        while (true)
        {
            result = chars[value % 26] + result;
            value /= 26;
            if (value == 0)
            {
                break;
            }
            value--; // Decrease value by 1 for indexing: "a" = 0, "b" = 1, ..., "z" = 25
        }

        return result;
    }

    public List<Employee> GetEmployeer()// Method to retrieve the list of employees
    {
        return employees;
    }
}
