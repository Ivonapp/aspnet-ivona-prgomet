using CoreFitness.Application.Models;
using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;


namespace CoreFitness.Infrastructure.Services;



public class AccountService(UserManager<AppUser> userManager)
{

    private readonly UserManager<AppUser> _userManager = userManager;


    /* 1. *SPARA ANVÄNDARENS UPPGIFTER*
     Model: MyAccountFormModel */

    public async Task<bool> UpdateProfileAsync(Guid userId, MyAccountFormModel model)
    {
        // 1. Hitta användaren: Använd userId för att hämta användaren från databasen.
        var findUser = await _userManager.FindByIdAsync(userId.ToString());

        if (findUser == null)
        {
            return false;
        }


        // 2. Skriv över fälten (firstname, lastname, email, phone etc) i användarobjektet med den NYA infon från form.
        // UpdateNormalizedUserNameAndEmailAsync = en metod i identity

        findUser.UserName = model.Email; //denna gör så kunden kan logga in med sin NYA email, för email är ju tekniskt sät usernamet vi skriver in 
        findUser.FirstName = model.FirstName;
        findUser.LastName = model.LastName;
        findUser.Email = model.Email;
        findUser.PhoneNumber = model.PhoneNumber;

        var UpdatedProfileFields = await _userManager.UpdateAsync(findUser); //Kör UpdateAsync för att skicka ändringarna till databasen med _userManager.

        // 4. Kontrollera om uppdateringen lyckades via Succeeded.

        if(UpdatedProfileFields.Succeeded)
        {
            return true;
        }

        return false;

        // 5. Returnera: Skicka tillbaka true eller false på om det sparades eller inte.
    }


    // ** Skriv in fileUpload inuti ovan kod, så allt som hanterar sparning, är både profilinfo OCH kundens profilbild. **

    // FILEUPLOAD

    //      1. Parametrar: Lägg till objektet för den uppladdade filen i metoden.

    //      2. Koll: Kontrollera att filen existerar och har ett innehåll.

    //      3. Miljö: Se till att klassen har tillgång till serverns fysiska sökväg (injicera rätt gränssnitt i konstruktorn).

    //      4. Mapp: Skapa den fullständiga vägen till din lagringsmapp och verka för att den skapas om den saknas.

    //      5. Unikt namn: Generera ett namn som inte kan krocka med befintliga filer.

    //      6. Spara: Öppna en ström till hårddisken och kopiera över filens innehåll asynkront.

    //      7. ** Koppling:** Sätt den relativa sökvägen (strängen) på din användar-entitet.

    //      Slutför: Kör den asynkrona uppdateringen av hela användarobjektet mot databasen.



















    /*2. RADERA KONTO (userManager - DeleteAsync(TUser)
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


        // Säkerhetskoll 2: Checka i terms.
        if (!form.ConfirmDelete)                                // Om terms INTE kryssas i, 
        {
            return false;                                       // Gå inte vidare
        }

        // Säkerhetskoll 3: STÄMMER LÖSENORDET I FORMMODEL MED DET I DATABASEN?
        // _userManager.CheckPasswordAsync = ett verktyg från Identity-ramverket. Den förstår att lösenord i databasen är krypterade. Den tar det vanliga lösenordet som användaren skrev in, krypterar det på samma sätt, och kollar om de matchar. 
        // finduser och form.Password =  vi skickar in tidigare finduser där det krypterade lösenordet finns, och jänför med "password" från DeleteAccountFormModel
        // kortfattat: jag ber Identity jämföra det inskrivna lösenordet med det som finns i databasen för just den användaren.
        var checkPassword = await _userManager.CheckPasswordAsync(findUser, form.Password);

        if(!checkPassword) //Om CheckPassword är falskt
            {
                
                return false;
            }


        // Utför: Om allt ovan är OK – radera användaren.
        // Eftersom allt som ni passerat ovan if-satser ÄR true, så VWET jag nu att användaren kan fortsätta till radering,

        var deleteUser = await _userManager.DeleteAsync(findUser); // vi skickar nu in hela användaren (findUser) i raderings-maskinen

        if(deleteUser.Succeeded) //Om deleteUser succeeded >
        {
            return true;        // returnera true
        }

        return false;           // om den INTE succeeded > returnera false

    }
}




