using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        switch (statusCode)
        {
            case 404:
                ViewBag.ErrorMessage = "Sorry, the rosource you requested could not be found";
                break;
        }

        return View("NotFound");
    }
}
