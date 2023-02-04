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
        throw new NotImplementedException();
    }

    public Employee Add(Employee employee)
    {
        throw new NotImplementedException();
    }

    public Employee Update(Employee employeeChanges)
    {
        throw new NotImplementedException();
    }

    public Employee Delete(int Id)
    {
        throw new NotImplementedException();
    }
}
