using Microsoft.EntityFrameworkCore;
using atlasCDCPayroll.Models;
using System;

namespace atlasCDCPayroll.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payslip> Payslips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed an Employee like 'ram' from legacy system
            modelBuilder.Entity<Employee>().HasData(
                new Employee 
                { 
                    EmployeeId = 101, 
                    Username = "ram", 
                    Password = "ram123", 
                    Name = "Ram Employee", 
                    Email = "ram@company.com", 
                    Phone = "555-0192", 
                    Designation = "Staff", 
                    Level = "1" 
                }
            );

            // Seed a Payslip for 'ram' with static times to prevent EF Warning
            modelBuilder.Entity<Payslip>().HasData(
                new Payslip
                {
                    PayslipId = 1,
                    EmployeeId = 101,
                    Month = 6,
                    MonthName = "June",
                    Year = 2026,
                    GeneratedOn = new DateTime(2026, 6, 15, 12, 0, 0),
                    BasicSalary = 50000m,
                    SalaryPerDay = 1666.67m,
                    NoOfLeaves = 0,
                    DeductionForLeaves = 0m,
                    NetSalary = 50000m
                }
            );
        }
    }
}