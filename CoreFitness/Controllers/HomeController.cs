using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewData["Title"] = "Home";

        return View();
    }
}
