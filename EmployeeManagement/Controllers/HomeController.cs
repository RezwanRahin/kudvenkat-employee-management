using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EmployeeManagement.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IHostingEnvironment _hostingEnvironment;

    public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment)
    {
        _employeeRepository = employeeRepository;
        _hostingEnvironment = hostingEnvironment;
    }

    public ViewResult Index()
    {
        var model = _employeeRepository.GetAllEmployees();
        return View(model);
    }

    public ViewResult Details(int? id)
    {
        HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        {
            Employee = _employeeRepository.GetEmployee(id??1),
            PageTitle = "Employee Details"
        };

        return View(homeDetailsViewModel);
    }

    [HttpGet]
    public ViewResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Employee employee)
    {
        if (ModelState.IsValid)
        {
            Employee newEmployee = _employeeRepository.Add(employee);
            // return RedirectToAction("Details", new { id = newEmployee.Id });
        }
        
        return View();
    }
}
