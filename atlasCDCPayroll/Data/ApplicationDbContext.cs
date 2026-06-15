using Microsoft.EntityFrameworkCore;
using atlasCDCPayroll.Models;

namespace atlasCDCPayroll.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<MessageModel> Messages { get; set; }
        public DbSet<Payslip> Payslips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Explicitly define primary keys to guarantee metadata validation passes cleanly
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeID);
            modelBuilder.Entity<MessageModel>().HasKey(m => m.MessageID);
            modelBuilder.Entity<Payslip>().HasKey(p => p.PayslipID);

            // Seed initial data matching structural system roles
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    EmployeeID = 1,
                    Name = "Admin Account",
                    Phone = "0911",
                    Email = "admin@atlas.com",
                    Designation = "System Admin",
                    Level = 1,
                    Username = "admin",
                    Password = "admin123"
                },
                new Employee
                {
                    EmployeeID = 2,
                    Name = "Ram",
                    Phone = "9348394834",
                    Email = "ram@gmail.com",
                    Designation = "Asst. Manager",
                    Level = 2,
                    Username = "ram",
                    Password = "ram123"
                },
                new Employee
                {
                    EmployeeID = 3,
                    Name = "kristian",
                    Phone = "09274828",
                    Email = "angelo@gmail.com",
                    Designation = "Developer",
                    Level = 4,
                    Username = "kristian",
                    Password = "krissy"
                }
            );
        }
    }
}