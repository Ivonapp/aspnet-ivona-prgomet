using CoreFitness.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class ErrorController : Controller
{

    [Route("Error/{statusCode}")]
    public IActionResult ErrorHandler(int statusCode)
    {
        return statusCode switch
        {
            404 => View("Error"),
            _ => View()
        };

        }
}


