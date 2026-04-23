using CoreFitness.Application.Models;
using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Reflection.PortableExecutable;

namespace CoreFitness.Presentation.Controllers;

public class MyAccountController(IWebHostEnvironment env, AccountService accountService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
        {

    private readonly IWebHostEnvironment _env = env; // denna är specifikt för filuppladdningen "upload"
    private readonly AccountService _accountService = accountService;
    private readonly UserManager<AppUser> _userManager = userManager; // För att hämta ID på den inloggade användaren.
    private readonly SignInManager<AppUser> _signInManager = signInManager; // För att logga ut användaren efter radering.





    //MYACCOUNT SIDAN / SPARA SIDA
    [HttpGet]
    [Route("myaccount")]
    public IActionResult MyAccount()
    {
        return View("~/Views/Account/MyAccount.cshtml");
    }






    [HttpPost]
    [Route("myaccount")]

    public IActionResult MyAccount(MyAccountFormModel model)
    {
        

        // 1. ID: Identifiera vilken användare som är inloggad.
         var findUser = _userManager.GetUserId(User);    

        // 2. Säkerhetskontroll: Avbryt och skicka bort besökaren om ingen användare hittas.
        if (findUser == null)
        {
            TempData["Error"] = "";
            return RedirectToAction("Index", "Home");
        }

        //3. Validering: Kontrollera att det som skrivits i formuläret stämmer överens med dina krav i formModel.
        if(!ModelState.IsValid)
        {
            return View("~/Views/Account/DeleteAccount.cshtml", model);
        }



        //> Om fel: Stanna kvar på sidan och visa vyn igen med befintlig data.

        //5. Spara den nya informationen i databasen genom att anropa Service.



        //6. Resultathantering:
        //> Vid lyckat resultat: Informera användaren om att det är sparat och ladda om eller visa sidan på nytt.
        //> Vid misslyckat resultat: Skapa ett felmeddelande och stanna kvar på sidan för att låta användaren försöka igen.

    }























    /* RADERA KONTO */
    [HttpGet]
    [Route("removeaccount")]
    public async Task<IActionResult> DeleteAccount()

    {
        return View("~/Views/Account/DeleteAccount.cshtml");
    }
       
   
  /* RADERA KONTO */
    [HttpPost]
    [Route("removeaccount")]
    public async Task<IActionResult> DeleteAccount(DeleteAccountFormModel model)
    {

        // 1. ID: Identifiera vilken användare som är inloggad.
        var findUser = _userManager.GetUserId(User);    // _userManager kollar bara i User. User är en inbyggd egenskap i Controller. Den innehåller all information om personen som surfar på sidan just nu – men informationen ligger i en så kallad Cookie i webbläsaren. Denna information kallas för "Claims".
                                                        // Ingen await = Eftersom vi endast läser webbläsarens cookie, behöver programmet inte vänta på svar från databasen för att hämta det.

        // 2. Säkerhetskontroll: Avbryt och skicka bort besökaren om ingen användare hittas.
        if (findUser == null)                       // OM användaren inte hittas, laddas sidan om.
        {
            TempData["Error"] = "";
            return RedirectToAction("Index", "Home");
        }

        //3. Validering: Kontrollera att det som skrivits i formuläret stämmer överens med dina krav i formModel.
        if (!ModelState.IsValid)                // > 1A CHECK - Har användaren fyllt i formuläret korrekt? (FormModel)
        {
            return View("~/Views/Account/DeleteAccount.cshtml", model);
        }
        
       
        if(!model.ConfirmDelete)                // > 2A CHECK - ÄR CHECKBOX IFYLLD?*
        {
            ModelState.AddModelError("ConfirmDelete", "Please confirm that you have read the terms and conditions.");
            return View("~/Views/Account/DeleteAccount.cshtml", model);
        }

        // ***************************************************************************

        // Kör din service-metod för RADERING av kontot i databasen.

        var result = await _accountService.DeleteAccountAsync(Guid.Parse(findUser), model); //Här raderas kontot


        if(result)
        {
            await _signInManager.SignOutAsync(); //Loggar ut användaren             // OM RADERING LYCKAS
            return RedirectToAction("Index", "Home");
        }

            ModelState.AddModelError("Password", "Please fill in the correct password and try again.");               // OM RADERING MISSLYCKAS
            ModelState.AddModelError("ConfirmPassword", "Please fill in the correct password and try again.");        // OM RADERING MISSLYCKAS
            return View("~/Views/Account/DeleteAccount.cshtml", model);
        }



        //FILUPPLADDNING
        [HttpGet]
        public IActionResult Upload()
        {

            return View();
        }


        //FILUPPLADDNING
        [HttpPost]
        public async Task<IActionResult> Upload(MyAccountFormModel formData) //Inuti Paranteserna skriver vi FormModel som styr filuppladdning. I detta fallet finns den i MyAccountFormModel.
        {
            if (!ModelState.IsValid || formData.File == null || formData.File.Length == 0)
                return View("~/Views/Account/MyAccount.cshtml", formData);

            var uploadFolder = Path.Combine(_env.WebRootPath, "Uploads");               //folder för bild
            Directory.CreateDirectory(uploadFolder);                                    //Om foldern inte finns så skapas en

            var filePath = Path.Combine(uploadFolder, $"{Guid.NewGuid()}_{Path.GetFileName(formData.File.FileName)}"); // bILDen användaren laddar upp sparas här, samt får den ett unikt GUID så man kan lägga in samma bild fler gånger där den får ett unikt namn

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formData.File.CopyToAsync(stream);
            }

            ViewBag.Message = "File was uploaded successfully.";
            return View("~/Views/Account/MyAccount.cshtml", formData);
     }
}


