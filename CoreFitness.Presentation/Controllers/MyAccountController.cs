using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Presentation.Controllers;

public class MyAccountController : Controller
{
    public IActionResult MyAccount()
    {
        return View("~/Views/Account/MyAccount.cshtml");
    }

}
