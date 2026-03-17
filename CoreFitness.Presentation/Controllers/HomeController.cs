using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class HomeController : Controller
{



    public IActionResult Index()
    {
        return View();
    }



    [Route("memberships")]
    public IActionResult Memberships()
    {
        return View();
    }



    [Route("training")]
    public IActionResult Training()
    {
        return View();
    }



    [Route("fitnesscenters")]
    public IActionResult FitnessCenters()
    {
        return View();
    }


    [Route("customerservice")]
    public IActionResult CustomerService()
    {
        return View();
    }



    [Route("store")]
    public IActionResult Store()
    {
        return View();
    }


}
