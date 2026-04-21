using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CoreFitness.Application.Models;


namespace CoreFitness.Infrastructure.Services;



public class AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{

    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;


    /*

    1. *SPARA ANVÄNDARENS UPPGIFTER*
     Model: MyAccountFormModel



    2. RADERA KONTO (userManager - DeleteAsync(TUser)
    Model: DeleteAccountFormModel */

    public async Task<bool> DeleteAccountAsync(Guid userId, DeleteAccountFormModel form)
    {

        // Hämta användaren: Använd userId för att slå upp användaren i databasen.
        // Identity har en egenskap som heter Id. Vi döper den sen til userID och systemet förstår kopplingen. 
        // userId jämförs sen med kolumnen Id i tabellen "Dbo.AspNetUsers" i min Databas som du kan SE direkt i databasen.)
        var findUser = await _userManager.FindByIdAsync(userId.ToString());

        // Säkerhetskoll 1: FINNS ANVÄNDAREN? (Om inte, returnera false).
        if (findUser == null)
            {
                return false;
            }

        // Säkerhetskoll 2: STÄMMER LÖSENORDET I FORMMODEL MED DET I DATABASEN?
        // _userManager.CheckPasswordAsync = ett verktyg från Identity-ramverket. Den förstår att lösenord i databasen är krypterade. Den tar det vanliga lösenordet som användaren skrev in, krypterar det på samma sätt, och kollar om de matchar. 
        // finduser och form.Password =  vi skickar in tidigare finduser där det krypterade lösenordet finns, och jänför med "password" från DeleteAccountFormModel
        // kortfattat: jag ber Identity jämföra det inskrivna lösenordet med det som finns i databasen för just den användaren.
        var checkPassword = await _userManager.CheckPasswordAsync(findUser, form.Password);

        if(!checkPassword) //Om CheckPassword är falskt
            {
                return false;
            }

            // Säkerhetskoll 3: Är CHECKBOXEN form.ConfirmDelete markerad av användaren?

        if(!form.ConfirmDelete)         //Om checkboxen INTE är ikryssad >
            {
                return false;           // returnera falskt
            }


        // Utför: Om allt ovan är OK – radera användaren.
        // Eftersom allt som ni passerat ovan if-satser ÄR true, så VWET jag nu att användaren kan fortsätta till radering,

        var deleteUser = await _userManager.DeleteAsync(findUser); // vi skickar nu in hela användaren (findUser) i raderings-maskinen

        if()

            // Returnera true om raderingen lyckades, annars false

            }






}




