using CoreFitness.Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreFitness.Controllers;

public class AuthController : Controller
{


// REGISTER GET
    [HttpGet]
    [Route("register")]
    public IActionResult Register()
    {
        return View();
    }


// REGISTER POST
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









// SET PASSWORD GET
        [HttpGet]
        [Route("setpassword")]
        public IActionResult SetPassword(string email)
        {
            // Vi sparar emailen i ViewBag så den syns på sidan direkt
            ViewBag.UserEmail = email;
            
            // Vi skickar med en tom modell till vyn
            return View(new SetPasswordFormModel());
        }


// SET PASSWORD POST
        [HttpPost]
        [Route("setpassword")] 
            public IActionResult SetPassword(SetPasswordFormModel formData, string email) //tar emot email
            {
                // Vi måste skicka tillbaka emailen till ViewBag varje gång sidan laddas om (vid fel)
                ViewBag.UserEmail = email;


            // CHECKBOX
            // SÄKERSTÄLLER ATT CHECKBOXEN KRYSSAS I, INNAN MAN GÅR VIDARE TILL NÄSTA SIDA
            if (!formData.TermsAccepted) 
            {
                ModelState.AddModelError("TermsAccepted", "Please confirm that you have read the terms and conditions.");
                return View(formData);
            }

                if (ModelState.IsValid)
                {
                // 1. Om allt är OK > Gå vidare till MyAccountController
                return RedirectToAction("MyAccount", "MyAccount"); //skickas till MyAccountController
                }

                // 2. Om något blir fel, så skickas istället femleddelande: 
                // Vi skickar tillbaka formData. Nu kommer asp-validation-for i SetPassword.cshtml att skriva ut felmeddelnande
                return View(formData);
            }
                /* return RedirectToAction  = när vi vill skickas till NY sida*/
                /* return View              = samma sida */









// SIGN OUT
    public new IActionResult SignOut()
    {
        return RedirectToAction("Index", "Home");
    }










// SIGN IN GET
    [HttpGet]
    [Route("signin")]   //Det du skriver inuti [Route("...")] är den URL-adress som användaren ser i webbläsarens adressfält.
                        // (Standard är med små bokstäver, och inga mellanslag i namnet.)
    public IActionResult SignIn()
    {
        return View();
    }




// SIGN IN POST
    [HttpPost]
    [Route("signin")]
    public IActionResult SignIn(SignInFormModel formData)   //Inuti Paranteserna skriver vi FormModel som styr inloggning.
    {
            // CHECKBOX
            // SÄKERSTÄLLER ATT CHECKBOXEN KRYSSAS I, INNAN MAN GÅR VIDARE TILL NÄSTA SIDA

            // OM CHECKBOX _INTE_ ÄR IKRYSSAD:
                if (!formData.TermsAccepted) 
                {
                    ModelState.AddModelError("TermsAccepted", "Please confirm that you have read the terms and conditions.");
                    return View(formData); // Om checkboxen INTE är ikryssad.
                }

            // OM CHECKBOX _ÄR_ IKRYSSAD:
                if (ModelState.IsValid)
                {
                // 1. Om allt är OK > Gå vidare till MyAccountController
                return RedirectToAction("MyAccount", "MyAccount"); //skickas till MyAccountController
                }

                // 2. Om något blir fel, så skickas istället femleddelande. 
                // Vi skickar tillbaka formData. Nu kommer asp-validation-for i SignIn.cshtml att skriva ut felmeddelnande
                return View(formData); //Om checkboxen är ikryssad, men något ANNAT är fel (t.ex. felaktigt e-postformat),
            }

  
}
