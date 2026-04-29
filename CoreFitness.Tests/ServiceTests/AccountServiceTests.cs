using CoreFitness.Application.Interfaces;
using CoreFitness.Application.Services;
using CoreFitness.Domain.Entities;
using CoreFitness.Domain.Interfaces;
using CoreFitness.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoreFitness.Tests.ServiceTests;

public class AccountServiceTests
{
    private readonly Mock<IAccountRepository> _repoMock;
    private readonly AccountService _service;
    private readonly Mock<IWebHostEnvironment> _envMock;

    public AccountServiceTests()
    {
        _repoMock = new Mock<IAccountRepository>();
        _envMock = new Mock<IWebHostEnvironment>();
        _service = new AccountService(_repoMock.Object, _envMock.Object);
    }



    //TESTAR ATT DET RETURNERAS FALSKT NÄR ANVÄNDARE INTE HITTAS
    [Fact]
    public async Task UpdateProfileAsync_UserDoesNotExist_ReturnsFalse()
    {
        //ARRANGE
        var repoMock = new Mock<IAccountRepository>();              // Skapa mocken
        var envMock = new Mock<IWebHostEnvironment>();

        repoMock.Setup(r => r.FindByIdAsync(It.IsAny<string>()))    // Mocken svarar här att INGEN ANVÄNDARE hittas
                .ReturnsAsync((AppUser)null!);

        var service = new AccountService(repoMock.Object, envMock.Object);  // Skapa servicen med mocken

        //ACT
        var result = await service.UpdateProfileAsync(      // Anropa UpdateProfileAsync
         Guid.NewGuid(),                                    
         "Test",
         "Test",
         "test@test.com",
         null,
         null);


        // ASSERT                                           // Returnerar falskt när användare ej finns. 
        Assert.False(result);
    }





    // ** FindByIdAsync **
    // UpdateProfileAsync
    // Returnerar True när användaren hittas i databasen
    [Fact]
    public async Task UserExist_ProfileSaved_ReturnsTrue()
    {
        //ARRANGE
        var existingUser = new AppUser { Id = "1", Email = "test@test.com" }; // skapar en fake användare

        _repoMock.Setup(r => r.FindByIdAsync(It.IsAny<string>()))   // Hittar användaren
                 .ReturnsAsync(existingUser);

        _repoMock.Setup(r => r.UpdateProfileAsync(It.IsAny<AppUser>()))
                 .ReturnsAsync(true);

        //ACT
        var result = await _service.UpdateProfileAsync(
            Guid.NewGuid(),
            "Calle",
            "Svensson",
            "Calle@test.com",
            "070000000000",
            null);

        //ASSERT
        Assert.True(result);

    }








    //  ** CheckPasswordAsync **
    //  Testet kontrollerar att metoden nekar radering och returnerar FALSE när en existerande användare anger ett felaktigt lösenord.
    [Fact]
    public async Task DeleteAccountAsync_WrongPassword_ReturnsFalse()
    {

        // ARRANGE
        var existingUser = new AppUser { Id = "1", Email = "test@test.com" };

        _repoMock.Setup(r => r.FindByIdAsync(It.IsAny<string>()))
                 .ReturnsAsync(existingUser);

        _repoMock.Setup(r => r.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                  .ReturnsAsync(false);

        // ACT
        var result = await _service.DeleteAccountAsync(
            Guid.NewGuid(),
            "password",
            true
            );

        // ASSERT
       Assert.False(result);


    }





    // ** FindByIdAsync **
    // ** CheckPasswordAsync **
    // ** DeleteAsync **
    // Testet verifierar att hela raderingsprocessen lyckas och returnerar true när användaren finns, anger rätt lösenord och har bekräftat raderingen.
    [Fact]
    public async Task DeleteAccountAsync_UserExists_CorrectPassword_ReturnsTrue()
    {

        // ARRANGE
        // Skapa variabeln: Skapa en existingUser(instans av AppUser).
        var existingUser = new AppUser { Id = Guid.NewGuid().ToString(), Email = "test@test.com" };

        _repoMock.Setup(r => r.FindByIdAsync(It.IsAny<string>()))
        .ReturnsAsync(existingUser);

        _repoMock.Setup(r => r.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
        .ReturnsAsync(true);

        _repoMock.Setup(r => r.DeleteAsync(It.IsAny<AppUser>()))
        .ReturnsAsync(true);


        // ACT
        // Anropa DeleteAccountAsync med rätt lösenord och confirmDelete: true.
        var result = await _service.DeleteAccountAsync(
            Guid.NewGuid(),
            "password",
            true
            );


        // ASSERT
        Assert.True(result);
    
        }
    }
