
using CoreFitness.Presentation.Models;
using CoreFitness.Domain.Entities;
using CoreFitness.Application.Services;
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

        //5. Radera användaren från databasen genom att anropa Service.
        var result = await _accountService.DeleteAccountAsync(Guid.Parse(findUser), model.Password, model.ConfirmDelete); //Här raderas kontot

        //6. Resultathantering:
        //> Vid lyckat resultat: Informera användaren om att det är sparat och ladda om eller visa sidan på nytt.
        if (result)
        {
            await _signInManager.SignOutAsync(); //Loggar ut användaren             // OM RADERING LYCKAS
            return RedirectToAction("Index", "Home");
        }

            ModelState.AddModelError("Password", "Please fill in the correct password and try again.");               // OM RADERING MISSLYCKAS
            ModelState.AddModelError("ConfirmPassword", "Please fill in the correct password and try again.");        // OM RADERING MISSLYCKAS
            return View("~/Views/Account/DeleteAccount.cshtml", model);
        }







    //MYACCOUNT SIDAN / SPARA SIDA / SPARAR PROFILBILD
    [HttpGet]
    [Route("myaccount")]
    public async Task<IActionResult> MyAccount()
    {
        // 1. Hämta användaren från databasen
        var user = await _userManager.GetUserAsync(User);

        if (user == null)
            return RedirectToAction("Index", "Home");

        // 2. Fyller i informationen FRÅN databas > view
        var model = new MyAccountFormModel
        {
            FirstName = user.FirstName!,
            LastName = user.LastName!,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            ProfileImageUrl = user.ProfileImageUrl // skickar in profilbilden som valts in i view
        };

        return View("~/Views/Account/MyAccount.cshtml", model);
    }




    //MYACCOUNT SIDAN / SPARA SIDA / SPARAR PROFILBILD
    [HttpPost]
    [Route("myaccount")]
    public async Task<IActionResult> MyAccount(MyAccountFormModel model)
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
        if (!ModelState.IsValid)
        {
            return View("~/Views/Account/MyAccount.cshtml", model);         //> Om fel: Stanna kvar på sidan och visa vyn igen med befintlig data.
        }


        //5. Spara den nya informationen i databasen genom att anropa Service.
        var saveProfile = await _accountService.UpdateProfileAsync(
        Guid.Parse(findUser),
        model.FirstName,
        model.LastName,
        model.Email,
        model.PhoneNumber,
        model.File
    );

        //6. Resultathantering:
        //> Vid lyckat resultat: Informera användaren om att det är sparat och ladda om eller visa sidan på nytt.
        if (saveProfile)
        {
            TempData["StatusMessage"] = "Your profile has now been updated!";
            return RedirectToAction("MyAccount");
        }

        //> Vid misslyckat resultat: Skapa ett felmeddelande och stanna kvar på sidan för att låta användaren försöka igen.
        ModelState.AddModelError("", "Unable to save information");               // OM RADERING MISSLYCKAS
        return View("~/Views/Account/MyAccount.cshtml", model);
    }

}


