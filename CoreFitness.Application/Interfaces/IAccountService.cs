using CoreFitness.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Interfaces;

public interface IAccountService
{

    Task<bool> UpdateProfileAsync(
        Guid userId,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        IFormFile? file);

    Task<bool> DeleteAccountAsync(
        Guid userId,
        string password,
        bool confirmDelete);

}
