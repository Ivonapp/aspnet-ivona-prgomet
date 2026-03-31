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
            return RedirectToAction("SetPassword", "Auth", new { email = formData.Email }); //skickar iväg emailen som ska synas i SetPassword sidan
        }
            
            return View(formData);
    }





        [HttpGet]
        [Route("setpassword")]
        public IActionResult SetPassword(string email)
        {
            // Vi sparar emailen i ViewBag så den syns på sidan direkt
            ViewBag.UserEmail = email;
            
            // Vi skickar med en tom modell till vyn
            return View(new SetPasswordFormModel());
        }


        [HttpPost]
        [Route("setpassword")]
            public IActionResult SetPassword(SetPasswordFormModel formData, string email) //tar emot email
            {
                // Vi måste skicka tillbaka emailen till ViewBag varje gång sidan laddas om (vid fel)
                ViewBag.UserEmail = email;


            if (!formData.TermsAccepted) 
            {
                ModelState.AddModelError("TermsAccepted", "Please confirm that you have read the terms and conditions.");
                return View(formData);
            }

                if (ModelState.IsValid)
                {
                // Om allt är OK > Gå vidare till MyAccountController
                return RedirectToAction("MyAccount", "MyAccount"); //skickas till MyAccountController
                }

                // OM DET FINNS FEL: 
                // Vi skickar tillbaka formData. Nu kommer asp-validation-for att skriva ut felmeddelnande
                return View(formData);
            }



/* return RedirectToAction = när vi vill skickas till NY sida*/
/* return View = samma sida */
/**/






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
