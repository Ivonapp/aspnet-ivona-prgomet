using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Interfaces;

public interface IAuthRepository
{

    // public async Task<bool> DoesEmailAlreadyExistAsync(string email) 
    Task<bool> DoesEmailAlreadyExistAsync(string email);

    Task<bool> CreateAsync(AppUser user, string password);

    // Loggar in användaren
    Task<bool> PasswordSignInAsync(string email, string password, bool isPersistent);

    //SIGN OUT
    Task<bool> SignOutAsync();

}