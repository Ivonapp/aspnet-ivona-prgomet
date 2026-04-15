using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoreFitness.Application.Models;

namespace CoreFitness.Infrastructure.Services;

public class UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{

    private readonly UserManager<AppUser> _userManager = userManager;       // UserManager är en del av Asp.net core som hanterar användaren
    private readonly SignInManager<AppUser> _signInManager = signInManager; // Denna hanterar Inloggning, autentisering, utlogg osv



    public async Task<bool> CreateAsync(RegisterFormModel form) //En metod som asynkront försöker skapa något (CreateAsync) och sedan svarar med sant eller falskt.
    {
        // Denna del frågar databasen asynkront om det överhuvudtaget existerar någon användare som matchar ett visst villkor.
        if (await _userManager.Users.AnyAsync(u => u.Email == form.Email))  // Inuti () är självaste villkoret: "hitta en användare vars e-postadress är exakt likadan som den som står i formuläret (form)"
            return false;                                                   // om den hittar samma mail > avbryt med FALSKT.


        // UserName = form.Email  = "Ta värdet från min Model och stoppa in det i min AppUser.
        var appUser = new AppUser {
            UserName = form.Email,
            Email = form.Email,
        };

        // Slutskedet där användaren tryckt på spara-knappen
        var result = await _userManager.CreateAsync(appUser); // , form.Password - krypterar användarens lösenord
        if (result.Succeeded)
            return true;
        else
            return false;
    }
}


// form =  det som finns i min Model.
// AppUser = styr hur användaren ser ut i databasen.





/*
 
STRUKTURERA UPP HUR MPNGA SERVICES JAG SKA HA:



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