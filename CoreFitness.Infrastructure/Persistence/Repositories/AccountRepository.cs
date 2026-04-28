using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using CoreFitness.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Infrastructure.Persistence.Repositories;

public class AccountRepository(UserManager<AppUser> userManager) : IAccountRepository
{

    private readonly UserManager<AppUser> _userManager = userManager;



    public async Task<bool> CheckPasswordAsync(AppUser appUser, string password)
    {
        return await _userManager.CheckPasswordAsync(appUser, password);
    }


    public async Task<AppUser?> FindByIdAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }


    public async Task<bool> UpdateProfileAsync(AppUser appUser)
    {
        var result = await _userManager.UpdateAsync(appUser);
        return result.Succeeded;
    }


    public async Task<bool> DeleteAsync(AppUser appUser)
    {
        var deleteUser = await _userManager.DeleteAsync(appUser);
        return deleteUser.Succeeded;
    }

}
