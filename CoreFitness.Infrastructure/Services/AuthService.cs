
using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoreFitness.Application.Models;

namespace CoreFitness.Infrastructure.Services;

public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{

    private readonly UserManager<AppUser> _userManager = userManager;       // UserManager är en del av Asp.net core som hanterar användaren
    private readonly SignInManager<AppUser> _signInManager = signInManager; // Denna hanterar Inloggning, autentisering, utlogg osv



    // GUARD CLAUSE
    // KOLLAR OM DET REDAN FINNS EN IDENTISK EPOST
    public async Task<bool> DoesEmailAlreadyExistAsync(RegisterFormModel form) //En metod som asynkront försöker skapa något (CreateAsync) och sedan svarar med sant eller falskt.
    {
        // Denna del frågar databasen asynkront om det överhuvudtaget existerar någon användare som matchar ett visst villkor.
        if (await _userManager.Users.AnyAsync(u => u.Email == form.Email))  // Inuti () är självaste villkoret: "hitta en användare vars e-postadress är exakt likadan som den som står i formuläret (form)"
            return true;                // = Identisk mail existerar redan.                                                   

            return false;                   // = Mailen finns inte (den är ledig).
    }


    // OM DET INTE FINNS IDENTISK EPOST OVAN,
    // KOMMER DENNA ATT SPARA EMAIL OCH LÖSENORD SOM ANVÄNDAREN SKRIVER IN
    public async Task<bool> CreateAsync(SetPasswordFormModel form, string email)

    {
        var appUser = new AppUser // AppUser är objektet som ska sparas i databasen. Den sparar mailen och undertill sparar den lösenordet
        {
            UserName = email,
            Email = email,

        };

        // Slutskedet där användaren tryckt på spara-knappen
        var result = await _userManager.CreateAsync(appUser, form.Password); // krypterar användarens lösenord, därav står inte lösenordet i appUser
        if (result.Succeeded)
            return true;
        else
            return false;
    }




    // SIGN IN
    public async Task<bool> SignInAsync(SignInFormModel form)
    {

        // GUARD CLAUSE
        // Måste kryssa i terms&conditions
        if (!form.TermsAccepted)                                // Om terms INTE kryssas i, 
        {
            return false;                                       // Gå inte vidare
        }


        // Loggar in användaren
        var result = await _signInManager.PasswordSignInAsync( // _signInManager & sen PasswordSignInAsync via intelliSense. PasswordSignInAsync kör det genom en algoritm (Hashing) och kollar om det matchar den krypterade strängen som ligger i databasen.
            form.Email,
            form.Password,
            false,              // Delen för REMEMBERME. Varje gång hemsidan stängs ner, loggas kund ut
            false               // LockoutOnFailure. False = användaren kan skriva fel hur många gånger som heslt

            );


        return result.Succeeded;
        }



    //SIGN OUT
    public async Task SignOutAsync()  //Ingen FormModel för signout behövs, eftersom den inte behöver ta emot data eller formulär från användaren. Den ska bara göra en sak: logga ut kund
    {

        // Loggar ut användaren
        await _signInManager.SignOutAsync();             


    }
}


/* GUARD CLAUSE:
	1. NEGATOIVT VÄRDE FÖRST: Jag kollar efter ogiltiga tillstånd eller felaktig data direkt i början av metoden.
 if (!form.TermsAccepted)

	2. TIDIG RETUR: Om villkoret uppfylls använder vi return för att avbryta, och koden undertill körs inte.
return false; 

    3. INGEN ELSE: slipper nästla in din kod i else-block.
*/




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