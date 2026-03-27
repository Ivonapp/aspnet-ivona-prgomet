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
    [Route("register")]
    public IActionResult Register(RegisterFormModel formData)
    {

        if (ModelState.IsValid)
        //return View(formData);
        {
            return RedirectToAction("SetPassword", "Auth");
        }
            
            return View(formData);
    }




        [Route("setpassword")]
            public IActionResult SetPassword()
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
