using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;


namespace CoreFitness.Infrastructure.Services;

public class UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
{

    private readonly UserManager<AppUser> _userManager = userManager;       // UserManager är en del av Asp.net core som hanterar användaren
    private readonly SignInManager<AppUser> _signInManager = signInManager; // Denna hanterar Inloggning, autentisering, utlogg osv

}
