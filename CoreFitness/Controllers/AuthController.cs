using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class AuthController : Controller
{


    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }


    [Route("signin")]
    public IActionResult SignIn()
    {
        return View();
    }



    public new IActionResult SignOut()
    {
        return RedirectToAction("Index", "Home");
    }

}
