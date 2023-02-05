using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Mary",
                    Department = Dept.IT,
                    Email = "mary@g.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "John",
                    Department = Dept.IT,
                    Email = "john@g.com"
                }
            );
    }
}
