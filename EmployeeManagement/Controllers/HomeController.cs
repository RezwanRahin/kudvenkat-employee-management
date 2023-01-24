using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class HomeController : Controller
{
    private IEmployeeRepository _employeeRepository;
    
    public HomeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    public JsonResult Index()
    {
        return Json(new { id = 1, name = "dotnet" });
    }
}
