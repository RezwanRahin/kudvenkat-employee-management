using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class HomeController : Controller
{
    public string Index()
    {
        return "Hello from MVC";
    }
}
