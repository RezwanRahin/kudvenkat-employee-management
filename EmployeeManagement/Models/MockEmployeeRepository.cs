namespace EmployeeManagement.Models;

public class MockEmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employeeList;
    
    public MockEmployeeRepository()
    {
        _employeeList = new List<Employee>()
        {
            new Employee() { Id=1, Name="Mary", Department=Dept.HR, Email="mary@g.com" },
            new Employee() { Id=2, Name="John", Department=Dept.IT, Email="john@g.com" },
            new Employee() { Id=3, Name="Sam", Department=Dept.IT, Email="sam@g.com" }
        };
    }

    public IEnumerable<Employee> GetAllEmployees()
    {
        return _employeeList;
    }

    public Employee Add(Employee employee)
    {
        employee.Id = _employeeList.Max(e => e.Id) + 1;
        _employeeList.Add(employee);
        return employee;
    }

    public Employee Update(Employee employeeChanges)
    {
        Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
        if (employee != null)
        {
            employee.Name = employeeChanges.Name;
            employee.Email = employeeChanges.Email;
            employee.Department = employee.Department;
        }
        return employee;
    }

    public Employee Delete(int Id)
    {
        Employee employee = _employeeList.FirstOrDefault(e => e.Id == Id);
        if (employee != null)
        {
            _employeeList.Remove(employee);
        }
        return employee;
    }

    public Employee GetEmployee(int Id)
    {
        return _employeeList.FirstOrDefault(e => e.Id == Id);
    }
}
