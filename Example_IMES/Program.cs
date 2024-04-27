using System.Collections.Generic;
using Example_IMES;


public class Program
{
    static void Main()
    {
        Console.WriteLine("Choose operation \n1.generate data, \n2.Employee search (after data generation):\n");

        string operation= Console.ReadLine();
        if (operation == "1")
        {
            GenerateData();
        }
        // Check Finish.db
        if (!System.IO.File.Exists("Finish.db"))
        {
            Console.WriteLine("Data has not been generated, or generation is incomplete");
            Console.WriteLine("Start generation");
            GenerateData();
        }
        //emulation of IMES login

        EmulationIMES emulationIMES = new("Finish.db");
        Console.WriteLine("Enter your code");
        string code = Console.ReadLine();
        emulationIMES.Login(code);
    }

    static private void GenerateData()
    {
        // data generation

        EmployeeRecords emp1 = new("WorkersFactory1.db");
        EmployeeRecords emp2 = new("WorkersFactory2.db");
        EmployeeRecords emp3 = new("WorkersFactory3.db");


        Random rnd = new Random();
        int WorkersFactory1 = rnd.Next(500, 20001);
        int WorkersFactory2 = rnd.Next(500, 20001);
        int WorkersFactory3 = rnd.Next(500, 20001);

        for (int i = 0; i < WorkersFactory1; i++)
        {
            emp1.AddEmployee();
        }
        Console.WriteLine("1 file created successfully ");
        for (int i = 0; i < WorkersFactory2; i++)
        {
            emp2.AddEmployee();
        }
        Console.WriteLine("2 file created successfully ");
        for (int i = 0; i < WorkersFactory3; i++)
        {
            emp3.AddEmployee();
        }
        Console.WriteLine("3 file created successfully ");

        //data Merge


        List<string> databasePaths = new List<string> { "WorkersFactory2.db", "WorkersFactory3.db", "WorkersFactory1.db" };
        string mergedDatabasePath = "merged_employees.db";
        DatabaseManager dbManager = new(mergedDatabasePath, databasePaths);
        dbManager.MergeDatabases();
        Console.WriteLine("File marged successfully ");

        //procesing Data

        ProcessingData pd = new("merged_employees.db");
        pd.GenerateUNIQIMES();
        Console.WriteLine("IMES generated successfully");
        pd.GenerateWorkerCodIMES();
        Console.WriteLine("Worker Cod IMES generated successfully");
        dbManager.SaveEmployeesToDatabase(pd.GetEmployeer(), "Finish.db");
        Console.WriteLine("Data save successfully");
    }
}
