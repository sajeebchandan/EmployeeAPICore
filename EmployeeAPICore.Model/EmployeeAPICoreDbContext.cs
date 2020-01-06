using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeAPICore.Model
{
    public class EmployeeAPICoreDbContext : DbContext
    {
        public EmployeeAPICoreDbContext()
        {

        }
        public EmployeeAPICoreDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-L8V077A\\ADVANCEPRO;Database=EmployeeDB;Trusted_Connection=True;");
            }
        }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Department> Department { get; set; }
    }
}
