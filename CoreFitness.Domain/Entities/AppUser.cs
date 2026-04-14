using Microsoft.AspNetCore.Identity;

namespace CoreFitness.Domain.Entities;

public class AppUser : IdentityUser
{
        //Email, Telefonnummer och Lösenord behöver jag inte skriva ut då dessa redan finns automatiskt i IdentityUser.
        [ProtectedPersonalData]
        public string FirstName { get; set; } = null!;
        [ProtectedPersonalData]
        public string LastName { get; set; } = null!;

        // PROFILBILD
        // Denna gör att vi kan spara "mannen i tröjan" (eller användarens egna profilbild som läggs in på MyAccount)
        public string? ProfileImageUrl { get; set; }
    }


