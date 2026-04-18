using Microsoft.AspNetCore.Identity;

namespace CoreFitness.Domain.Entities;

public class AppUser : IdentityUser
{
        //Email, Telefonnummer och Lösenord behöver jag inte skriva ut då dessa redan finns automatiskt i IdentityUser.
        [ProtectedPersonalData]
        public string? FirstName { get; set; }       // Lagt denna som Null. Annars går det inte att skapa ett konto. 
        [ProtectedPersonalData]
        public string? LastName { get; set; }        // Lagt denna som Null. Annars går det inte att skapa ett konto. 



        // PROFILBILD
        public string? ProfileImageUrl { get; set; }
    }


