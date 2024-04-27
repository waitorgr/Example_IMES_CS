using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Example_IMES
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        private readonly string databasePath;

        public EmployeeContext(string databasePath)
        {
            this.databasePath = databasePath;
            Database.EnsureCreated(); // Create the database if it doesn't exist
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={databasePath}");
        }
    }
}
