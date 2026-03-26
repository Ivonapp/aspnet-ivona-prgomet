using CoreFitness.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class AuthController : Controller
{


    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterFormModel formData)
    {

        if (!ModelState.IsValid)
            return View(formData);

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


        [Route("setpassword")]
    public IActionResult SetPassword()
    {
        return View();
    }

}
