
using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoreFitness.Application.Models;

namespace CoreFitness.Infrastructure.Services;

public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{

    private readonly UserManager<AppUser> _userManager = userManager;       // UserManager är en del av Asp.net core som hanterar användaren
    private readonly SignInManager<AppUser> _signInManager = signInManager; // Denna hanterar Inloggning, autentisering, utlogg osv


    //REGISTERFORMMODEL - ***KOLLAR ENDAST SÅ DET INTE FINNS FLER EPOST***
    public async Task<bool> CheckEmailExistAsync(RegisterFormModel form) //En metod som asynkront försöker skapa något (CreateAsync) och sedan svarar med sant eller falskt.
    {
        // Denna del frågar databasen asynkront om det överhuvudtaget existerar någon användare som matchar ett visst villkor.
        if (await _userManager.Users.AnyAsync(u => u.Email == form.Email))  // Inuti () är självaste villkoret: "hitta en användare vars e-postadress är exakt likadan som den som står i formuläret (form)"
            return false;                                                   // om den hittar samma mail > avbryt med FALSKT.


        // Annars returnera True (Mailen är ledig, kör på!)
        return true;
    }

    public async Task<bool> CreateAsync(SetPasswordFormModel form, string email)

    {
        var appUser = new AppUser // AppUser är objektet som ska sparas i databasen. Den sparar mailen och undertill sparar den lösenordet
        {
            UserName = email,
            Email = email

        };

        // Slutskedet där användaren tryckt på spara-knappen
        var result = await _userManager.CreateAsync(appUser, form.Password); // krypterar användarens lösenord, därav står inte lösenordet i appUser
        if (result.Succeeded)
            return true;
        else
            return false;
    }

}




/*


AuthService
BERÖR SKAPA KONTO/INLOGG
* RegisterFormModel (become a member)
- kolla om det finns en likadan email, om inte > gå vidare till setPassword




* SetPasswordFormModel
- Skriv in lösenord + confirma lösenord > 
- skapa och SPARA användare med email + lösenord





* SignInFormModel
- Logga in redan skapad användare med korrekt email och lösenord 


 
 */






/*
 
ALLA SERVICES



AccountService
ANVÄNDARENS PERSONLIGA INFORMATION OCH INSTÄLLNINGAR
* MyAccountFormModel - (Lägg IFormFile i egen service?)

FileService
* IFormFile


AuthService
BERÖR SKAPA KONTO/INLOGG
* RegisterFormModel
* SetPasswordFormModel
* SignInFormModel


 
 */