using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Domain.Interfaces;

public interface IAccountRepository
{

    Task<AppUser?> FindByIdAsync(string userId);
    Task<bool> CheckPasswordAsync(AppUser appUser, string password);
    Task<bool> UpdateProfileAsync(AppUser appUser);
    Task<bool> DeleteAsync(AppUser appUser);

   }
