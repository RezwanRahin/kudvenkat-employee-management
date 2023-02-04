using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models;

public class SQLEmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext context;
    public SQLEmployeeRepository(AppDbContext context)
    {
        this.context = context;
    }
    
    public Employee GetEmployee(int Id)
    {
        return context.Employees.Find(Id);
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return context.Employees;
    }

    public Employee Add(Employee employee)
    {
        context.Employees.Add(employee);
        context.SaveChanges();
        return employee;
    }

    public Employee Update(Employee employeeChanges)
    {
        var employee = context.Employees.Attach(employeeChanges);
        employee.State = EntityState.Modified;
        context.SaveChanges();
        return employeeChanges;
    }

    public Employee Delete(int Id)
    {
        Employee employee = context.Employees.Find(Id);
        if (employee != null)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
        }
        return employee;
    }
}
