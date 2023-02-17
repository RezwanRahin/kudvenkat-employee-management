using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EmployeeManagement.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment, ILogger<HomeController> logger)
    {
        _employeeRepository = employeeRepository;
        _hostingEnvironment = hostingEnvironment;
        _logger = logger;
    }

    public ViewResult Index()
    {
        var model = _employeeRepository.GetAllEmployees();
        return View(model);
    }

    public ViewResult Details(int? id)
    {
        // throw new Exception("Error in Details view");

        _logger.LogTrace("Trace Log");
        _logger.LogDebug("Debug Log");
        _logger.LogInformation("Information Log");
        _logger.LogWarning("Warning Log");
        _logger.LogError("Error Log");
        _logger.LogCritical("Critical Log");

        Employee employee = _employeeRepository.GetEmployee(id.Value);

        if (employee == null)
        {
            Response.StatusCode = 404;
            return View("EmployeeNotFound", id.Value);
        }

        HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
        {
            Employee = employee,
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

    [HttpPost]
    public IActionResult Edit(EmployeeEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            Employee employee = _employeeRepository.GetEmployee(model.Id);
            employee.Name = model.Name;
            employee.Email = model.Email;
            employee.Department = model.Department;
            if (model.Photo != null)
            {
                if (model.ExistingPhotoPath != null)
                {
                    string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                    System.IO.File.Delete(filePath);
                }
                employee.PhotoPath = ProcessUploadedFile(model);
            }

            _employeeRepository.Update(employee);
            return RedirectToAction("Index");
        }
        
        return View(model);
    }
}
