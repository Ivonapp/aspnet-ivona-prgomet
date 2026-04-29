using CoreFitness.Application.Services;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Interfaces;
using CoreFitness.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CoreFitness.Tests.ServiceTests;


public class AuthServiceTests
{
    private readonly Mock<IAuthRepository> _authRepoMock;
    private readonly AuthService _service;

    public AuthServiceTests()
    {
        _authRepoMock = new Mock<IAuthRepository>();
        _service = new AuthService(_authRepoMock.Object);
    }



    // ** DoesEmailAlreadyExistAsync **
    // Returnerar true när en e-postadress redan finns
    [Fact]
    public async Task DoesEmailAlreadyExistAsync_EmailExists_ReturnsTrue()
    {
        // ** ARRANGE **
        _authRepoMock.Setup(r => r.DoesEmailAlreadyExistAsync(It.IsAny<string>()))
        .ReturnsAsync(true);

        // ** ACT **
        // Skapa en variabel som tar in en epost
        var result = await _service.DoesEmailAlreadyExistAsync(
            "test@test.com");

        // ** ASSERT **
        Assert.True(result);
    }





    // ** CreateAsync **
    // Returnerar true när tjänsten lyckas skapa en ny användare
    [Fact]
    public async Task CreateAsync_ValidUser_ReturnsTrue()
    {
        // ARRANGE
        _authRepoMock.Setup(r => r.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
        .ReturnsAsync(true);

        // ACT
        // Skapa en variabel som tar in en epost + lösenord
        var result = await _service.CreateAsync(
            "Password",
            "test@test.com");

        // ASSERT
        Assert.True(result);
    }



    //  ** PasswordSignInAsync **
    // returnerar true när användaren lyckas loggas in med korrekt email, lösenord och kryssat i checkbox
    [Fact]
    public async Task SignInAsync_ValidCredentialsAndTermsAccepted_ReturnsTrue()
    {
        // ARRANGE
            _authRepoMock.Setup(r => r.PasswordSignInAsync(
            It.IsAny<string>(),     // email
            It.IsAny<string>(),     // password
            It.IsAny<bool>()        // checkboxen
            ))
            .ReturnsAsync(true);

        // ACT
        // Skapa variabeln result och kör await _service.SignInAsync().
        var result = await _service.SignInAsync(
            "test@test.com",
            "Password",
            true);

        // ASSERT
        Assert.True(result);
    }



    // ** SignOutAsync **
    // säkerställer att det skickas en begäran om utloggning EN gång
    [Fact]
    public async Task SignOutAsync_CallsRepositoryOnce()
    {
        // ARRANGE
        _authRepoMock.Setup(r => r.SignOutAsync());


        // ACT
        await _service.SignOutAsync();


        // ASSERT
        _authRepoMock.Verify(r => r.SignOutAsync(), Times.Once);

    }
}