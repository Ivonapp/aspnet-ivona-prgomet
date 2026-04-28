
using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CoreFitness.Application.Services;

public class AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{

    private readonly UserManager<AppUser> _userManager = userManager;       // UserManager är en del av Asp.net core som hanterar användaren
    private readonly SignInManager<AppUser> _signInManager = signInManager; // Denna hanterar Inloggning, autentisering, utlogg osv


    // GUARD CLAUSE
    // KOLLAR OM DET REDAN FINNS EN IDENTISK EPOST
    public async Task<bool> DoesEmailAlreadyExistAsync(string email) //En metod som asynkront försöker skapa något (CreateAsync) och sedan svarar med sant eller falskt.
    {
        // Denna del frågar databasen asynkront om det överhuvudtaget existerar någon användare som matchar ett visst villkor.
        if (await _userManager.Users.AnyAsync(u => u.Email == email))  // Inuti () är självaste villkoret: "hitta en användare vars e-postadress är exakt likadan som den som står i formuläret (form)"
            return true;                // = Identisk mail existerar redan.                                                   

            return false;                   // = Mailen finns inte (den är ledig).
    }


    // OM DET INTE FINNS IDENTISK EPOST OVAN,
    // KOMMER DENNA ATT SPARA EMAIL OCH LÖSENORD SOM ANVÄNDAREN SKRIVER IN
    public async Task<bool> CreateAsync(string password, string email)

    {
        var appUser = new AppUser // AppUser är objektet som ska sparas i databasen. Den sparar mailen och undertill sparar den lösenordet
        {
            UserName = email,
            Email = email,

        };

        // Slutskedet där användaren tryckt på spara-knappen
        var result = await _userManager.CreateAsync(appUser, password); // krypterar användarens lösenord, därav står inte lösenordet i appUser
        if (result.Succeeded)
            return true;
        else
            return false;
    }




    // SIGN IN
    public async Task<bool> SignInAsync(string email, string password, bool termsAccepted)
    {

        // GUARD CLAUSE
        // Måste kryssa i terms&conditions
        if (!termsAccepted)                                // Om terms INTE kryssas i, 
        {
            return false;                                       // Gå inte vidare
        }


        // Loggar in användaren
        var result = await _signInManager.PasswordSignInAsync( // _signInManager & sen PasswordSignInAsync via intelliSense. PasswordSignInAsync kör det genom en algoritm (Hashing) och kollar om det matchar den krypterade strängen som ligger i databasen.
            
            email,
            password,
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