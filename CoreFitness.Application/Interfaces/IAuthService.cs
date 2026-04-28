using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFitness.Application.Interfaces;

public interface IAuthService
{


    Task<bool> DoesEmailAlreadyExistAsync(string email);

    Task<bool> CreateAsync(string password, string email);

    Task<bool> SignInAsync(string email, string password, bool termsAccepted);

    Task SignOutAsync();

}
