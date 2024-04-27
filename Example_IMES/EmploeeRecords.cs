using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Example_IMES
{
    public class EmployeeRecords
    {
        // Arrays of sample names, surnames, factory names, and department names
        private static readonly string[] names = { "John", "Mary", "James", "Linda", "Robert", "Patricia", "Michael", "Jennifer", "William", "Elizabeth", "David", "Barbara", "Joseph", "Jessica", "Richard", "Sarah", "Thomas", "Karen", "Charles", "Nancy", "Daniel", "Margaret", "Matthew", "Lisa", "Anthony", "Betty", "Donald", "Dorothy", "Mark", "Sandra", "Paul", "Ashley", "Steven", "Kimberly", "Andrew", "Donna", "Kenneth", "Emily", "George", "Carol", "Joshua", "Michelle", "Kevin", "Amanda", "Brian", "Melissa", "Edward", "Deborah", "Ronald", "Stephanie" };
        private static readonly string[] surnames = { "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King", "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sanchez", "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy", "Bailey", "Rivera", "Cooper", "Richardson", "Cox", "Howard", "Ward", "Torres", "Peterson", "Gray", "Ramirez", "James", "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett", "Wood", "Barnes", "Ross", "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes", "Flores", "Washington", "Butler", "Simmons", "Foster", "Gonzales", "Bryant", "Alexander", "Russell", "Griffin", "Diaz", "Hayes" };
        private static readonly string[] FactoryNames = {"Gates USA","Gates Canada","Gates UK","Gates Germany","Gates France","Gates Australia","Gates Poland","Gates Spain", "Gates Italy","Gates Japan"};
        private static readonly string[] DepartmentName = {"Production Department","Research and Development Department","Marketing Department","Sales Department","Human Resources Department","Finance Department","Information Technology Department","Customer Service Department","Quality Assurance Department","Supply Chain Department"};
        
        private List<Employee> employees = new();// List to store employees
        private readonly string databasePath;// Path to the database
        private readonly EmployeeContext context;// Database context
        private string curentFactory="";// Current factory name

        
        public EmployeeRecords(string databasePath)// Constructor
        {
            this.databasePath = databasePath;
            if (System.IO.File.Exists(databasePath))
            {
                System.IO.File.Delete(databasePath);
            }
            context = new EmployeeContext(databasePath);
            
            curentFactory = GenerateRandomFactoryName();// Generate a random factory name

        }

        public void AddEmployee()// Method to add an employee
        {
            var employee = new Employee(GenerateRandomName(), curentFactory, GenerateRandomDepartmentName()); // Generate a new employee with random details
            employees.Add(employee);
            context.Employees.Add(employee); // Add the employee to the context
            context.SaveChanges(); // Save changes to the database
        }
        
        private string GenerateRandomFactoryName()  // Method to generate a random factory name
        {  
            Random rnd = new Random();
            return (FactoryNames[rnd.Next(FactoryNames.Length)]);
        }
        private string GenerateRandomDepartmentName() // Method to generate a random factory name
        {
            Random rnd = new Random();
            return (DepartmentName[rnd.Next(DepartmentName.Length)]);
        }
        private string GenerateRandomName() // Method to generate a random factory name
        {
            Random rnd = new Random();
            return (names[rnd.Next(names.Length)] + " " + surnames[rnd.Next(surnames.Length)]);
        }
        
    }
}
