using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }
    
    [Route("Error/{statusCode}")]
    public IActionResult HttpStatusCodeHandler(int statusCode)
    {
        var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

        switch (statusCode)
        {
            case 404:
                ViewBag.ErrorMessage = "Sorry, the rosource you requested could not be found";
                ViewBag.Path = statusCodeResult.OriginalPath;
                ViewBag.QS = statusCodeResult.OriginalQueryString;
                break;
        }

        return View("NotFound");
    }

    [Route("Error")]
    [AllowAnonymous]
    public IActionResult Error()
    {
        var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        _logger.LogError($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");

        return View("Error");
    }
}
