
using CoreFitness.Application.Interfaces;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CoreFitness.Application.Services;

public class AuthService(IAuthRepository authRepository) : IAuthService
{
    private readonly IAuthRepository _authRepository = authRepository;



    // GUARD CLAUSE
    // KOLLAR OM DET REDAN FINNS EN IDENTISK EPOST
    public async Task<bool> DoesEmailAlreadyExistAsync(string email) //En metod som asynkront försöker skapa något (CreateAsync) och sedan svarar med sant eller falskt.
    {
        return await _authRepository.DoesEmailAlreadyExistAsync(email);
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
        return await _authRepository.CreateAsync(appUser, password);
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
        return await _authRepository.PasswordSignInAsync(email, password, isPersistent: false);
    }



    //SIGN OUT
    public async Task SignOutAsync()  //Ingen FormModel för signout behövs, eftersom den inte behöver ta emot data eller formulär från användaren. Den ska bara göra en sak: logga ut kund
    {

        // Loggar ut användaren
        await _authRepository.SignOutAsync();             

    }
}