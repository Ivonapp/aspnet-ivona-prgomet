using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class MyAccountController : Controller
{

//MYACCOUNT SIDAN
    public IActionResult MyAccount()
    {
        return View("~/Views/Account/MyAccount.cshtml");
    }



//FILUPPLADDNING
    [HttpPost]
    public IActionResult Upload()
    {
        // Logik för uppladdning kommer här sen
        return RedirectToAction("MyAccount");
    }
}


