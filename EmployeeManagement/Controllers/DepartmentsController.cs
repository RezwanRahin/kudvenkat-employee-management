using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class DepartmentsController : Controller
{
    public string List()
    {
        return "List() of DepartmentsController";
    }

    public string Details()
    {
        return "Details() of DepartmentsController";
    }
}
