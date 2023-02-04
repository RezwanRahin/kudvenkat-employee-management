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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
