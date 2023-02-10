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

    private string ProcessUploadedFile(EmployeeCreateViewModel model)
    {
        string uniqueFileName = null;
        
        if (model.Photo != null)
        {
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.Photo.CopyTo(fileStream);
            }
        }

        return uniqueFileName;
    }

    [HttpGet]
    public ViewResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(EmployeeCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            string uniqueFileName = ProcessUploadedFile(model);

            Employee newEmployee = new Employee()
            {
                Name = model.Name,
                Email = model.Email,
                Department = model.Department,
                PhotoPath = uniqueFileName
            };

            _employeeRepository.Add(newEmployee);
            return RedirectToAction("Details", new { id = newEmployee.Id });
        }
        
        return View();
    }

    [HttpGet]
    public ViewResult Edit(int id)
    {
        Employee employee = _employeeRepository.GetEmployee(id);
        EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
        {
            Id = employee.Id,
            Name = employee.Name,
            Email = employee.Email,
            Department = employee.Department,
            ExistingPhotoPath = employee.PhotoPath
        };
        return View(employeeEditViewModel);
    }
}
