using CoreFitness.Application.Models;
using CoreFitness.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;


namespace CoreFitness.Controllers;



/* I DENNA CONTROLLER HAR JAG 
    REGISTRERING AV KONTO,
    INLOGGNING
    UTLOGGNING */


public class AuthController(AuthService authService) : Controller
{

    //KONSTRUKTORN FÖR AUTHSERVICE
    private readonly AuthService _authService = authService;



    // REGISTER GET
    [HttpGet]
    [Route("register")] //Det du skriver inuti [Route("...")] är den URL-adress som användaren ser i webbläsarens adressfält.
                        // (Standard är med små bokstäver, och inga mellanslag i namnet.)
    public IActionResult Register()
    {
        return View();
    }



    // CheckEmailExistAsync + RegisterFormModel regex
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterFormModel formData) //Om servicen har async, så måste Controllern ha det med!
    {
             
                // 1A CHECK - regex, reguired etc
                if (!ModelState.IsValid)    //Kollar vilkoren för FormModel. När ModelState anropas så har C# redan kört igenom hela min RegisterFormModel och kollat alla mina [EmailAddress], [RegularExpression] och [Required].
                {
                    return View(formData); // Går tillbaka men behåller allt som användaren skrivit (formdata)
                }

                // 2A CHECK - finns en identisk mail?
                var emailAlreadyExists = await _authService.DoesEmailAlreadyExistAsync(formData);

                if (emailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "Identical Email already exist.");
                    return View(formData); // Går tillbaka men behåller allt som användaren skrivit (formdata)
                }


       // Om både Regex och EmailAlreadyExist funkar, går den vidare till SetPassword sidan
        return RedirectToAction("SetPassword", "Auth", new { email = formData.Email });
    }




    // SET PASSWORD GET
    [HttpGet]
    [Route("setpassword")]
    public IActionResult SetPassword(string email)
    {
        ViewBag.UserEmail = email;  // Denna delen, gör så emailen användaren skrev in på sidan innan, syns direkt från start på denna sidan

        return View(new SetPasswordFormModel()); // Vi skickar med en tom modell till vyn
    }



    // SET PASSWORD POST
    [HttpPost]
    [Route("setpassword")]
    public async Task<IActionResult> SetPassword(SetPasswordFormModel formData, string email) //tar emot email
    {
        ViewBag.UserEmail = email; // Skickar tillbaka emailen till ViewBag varje gång sidan laddas om (vid fel)


        // 1A CHECKEN - SÄKERSTÄLLER att lösenorden stämmer
        if (!ModelState.IsValid)

        {
            return View(formData);
        }


        // 2A CHECKEN - SÄKERSTÄLLER ATT CHECKBOXEN KRYSSAS I, INNAN MAN GÅR VIDARE TILL NÄSTA SIDA
        if (!formData.TermsAccepted)
        {
            ModelState.AddModelError("TermsAccepted", "Please confirm that you have read the terms and conditions.");
            return View(formData);
        }


        // 3A CHECKEN - 
        var CreateAccount = await _authService.CreateAsync(formData, email);

        if (CreateAccount)
        {
            // 1. Om allt är OK > Gå vidare till MyAccountController
            return RedirectToAction("MyAccount", "MyAccount"); //skickas till MyAccountController
        }


        // 2. Om något blir fel, så skickas istället femleddelande: 
        // Vi skickar tillbaka formData. Nu kommer asp-validation-for i SetPassword.cshtml att skriva ut felmeddelnande
        ModelState.AddModelError("", "Account creation failed. Please try again or contact support.");
        return View(formData);
    }
    /* return RedirectToAction  = när vi vill skickas till NY sida*/
    /* return View              = samma sida */




    // SIGN IN GET
    [HttpGet]
    [Route("signin")]
    public IActionResult SignIn()
    {
        return View();
    }




    // SIGN IN POST
    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> SignIn(SignInFormModel formData)   //Inuti Paranteserna skriver vi FormModel som styr inloggning.
    {

        // 1A CHECK - Regex och allt som står i FormModels (förutom TermsAccepter som måste kollas manuellt nedan.)
        if (!ModelState.IsValid)
        {
            return View(formData);
        }

        // 2A CHECK - Kryssa i terms
        if (!formData.TermsAccepted)  // Om checkboxen inte kryssas i
        {
            ModelState.AddModelError("TermsAccepted", "Please confirm that you have read the terms and conditions.");
            return View(formData);
        }

        // 3A CHECK - 
        var LogInSuccessfull = await _authService.SignInAsync(formData);

        if (!LogInSuccessfull)
        {
            ModelState.AddModelError("Email", "Log in unsuccessfull. Please try again.");
            return View(formData); // Går tillbaka men behåller allt som användaren skrivit (formdata)
        }

        // 1. Om allt är OK > Gå vidare till MyAccountController
        return RedirectToAction("MyAccount", "MyAccount"); //skickas till MyAccountController
    }





    // SIGN OUT
    [HttpPost]
    public async Task<IActionResult> SignOff()
    {
        await _authService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}
