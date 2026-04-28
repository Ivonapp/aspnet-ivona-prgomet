using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public class AuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : IAuthRepository
{

    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;



    public async Task<bool> CreateAsync(AppUser appUser, string password)
    {

        var result = await _userManager.CreateAsync(appUser, password);
        if (result.Succeeded)
        {
            return true;
        }
        else
        {
            return false;
        }

    }




    public async Task<bool> DoesEmailAlreadyExistAsync(string email)
    {
        return await _userManager.Users.AnyAsync(u => u.Email == email);
    }



    public async Task<bool> PasswordSignInAsync(string email, string password, bool isPersistent)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure: false);
        return result.Succeeded;

    }




    public async Task<bool> SignOutAsync()
    {
        await _signInManager.SignOutAsync();
        return true;
    }
}
