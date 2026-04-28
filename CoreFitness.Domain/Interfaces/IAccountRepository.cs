using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Interfaces;

public interface IAccountRepository
{

    Task<AppUser?> FindByIdAsync(string userId);

    // var checkPassword = await _userManager.CheckPasswordAsync(findUser, password);
    Task<bool> CheckPasswordAsync(AppUser user, string password);

    //<Retur, Resultat> //namn // (In-data)
    Task<bool> UpdateProfileAsync(AppUser user);

    // public async Task<bool> DeleteAccountAsync(Guid userId, string password, bool confirmDelete)
    Task<bool> DeleteAccountAsync(Guid userId, string password, bool confirmDelete);


    // var deleteUser = await _userManager.DeleteAsync(findUser);
    Task<bool> DeleteAsync(AppUser user);

   }
