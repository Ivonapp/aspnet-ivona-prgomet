using CoreFitness.Application.Interfaces;
using CoreFitness.Application.Services;
using CoreFitness.Domain.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

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


    [Fact]
    public void TestNamn()
    {
        //SKRIV KOD HÄR

        [Arrange]


        [Act]


        [Assert]
        Assert.False(result);



        // 1. *SPARA ANVÄNDARENS UPPGIFTER
        public async Task<bool> UpdateProfileAsync(Guid userId,
        string firstName,
        string lastName,
        string email,
        string? phoneNumber,
        IFormFile? file)
        {

            // TEST 1
            // 1. Om ingen användare existerar > ska det returneras false
            var findUser = await _accountRepository.FindByIdAsync(userId.ToString());

            if (findUser == null)
            {
                return false;
            }

        }











            // BILDFIL START
            if (file != null && file.Length > 0)                                    // kontrollerar att filen exisrerar och har ett innehåll
            {
                var uploadFolder = Path.Combine(_env.WebRootPath, "Uploads");
                Directory.CreateDirectory(uploadFolder);

                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                findUser.ProfileImageUrl = fileName;
            }
            // BILDFIL END


            // 2. Skriv över fälten (firstname, lastname, email, phone etc) i användarobjektet med den NYA infon från form.
            // UpdateNormalizedUserNameAndEmailAsync = en metod i identity

            findUser.UserName = email; //denna gör så kunden kan logga in med sin NYA email, för email är ju tekniskt sät usernamet vi skriver in 
            findUser.FirstName = firstName;
            findUser.LastName = lastName;
            findUser.Email = email;
            findUser.PhoneNumber = phoneNumber;

            var UpdatedProfileFields = await _accountRepository.UpdateProfileAsync(findUser); //Kör UpdateAsync för att skicka ändringarna till databasen med _userManager.

            // 4. Kontrollera om uppdateringen lyckades via Succeeded.

            if (UpdatedProfileFields)
            {
                return true;
            }

            return false;
        }














    }















