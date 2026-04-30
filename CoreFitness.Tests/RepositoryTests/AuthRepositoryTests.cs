using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Persistence;
using CoreFitness.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;

namespace CoreFitness.Tests.RepositoryTests;

public class AuthRepositoryTests
{
    private readonly Mock<UserManager<AppUser>> _mockUserManager;
    private readonly Mock<SignInManager<AppUser>> _mockSignInManager;
    private readonly AuthRepository _repository;

    
    public AuthRepositoryTests()
    {
        var store = new Mock<IUserStore<AppUser>>();
        _mockUserManager = new Mock<UserManager<AppUser>>(store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        var contextAccessor = new Mock<IHttpContextAccessor>();
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<AppUser>>();
        _mockSignInManager = new Mock<SignInManager<AppUser>>(_mockUserManager.Object, contextAccessor.Object, claimsFactory.Object, null!, null!, null!, null!);

        _repository = new AuthRepository(_mockUserManager.Object, _mockSignInManager.Object);
    }

    // ** Här ska vi skriva koden **


    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_WhenUserIsCreated()
    {
        // ARRANGE
        var user = new AppUser { Email = "test@test.com", UserName = "test@test.com" };
        var password = "Password";
        _mockUserManager.Setup(x => x.CreateAsync(user, password))
            .ReturnsAsync(IdentityResult.Success);

        // ACT
        var result = await _repository.CreateAsync(user, password);

        // ASSERT
        Assert.True(result);
        _mockUserManager.Verify(x => x.CreateAsync(user, password), Times.Once);
    }

    [Fact]
    public async Task DoesEmailAlreadyExistAsync_ShouldReturnTrue_WhenEmailExists()
    {
        // ARRANGE
        var email = "test@test.com";
        var users = new List<AppUser> { new AppUser { Email = email } };

        var mockUsers = users.BuildMock();

        _mockUserManager.Setup(x => x.Users).Returns(mockUsers);

        // ACT
        var result = await _repository.DoesEmailAlreadyExistAsync(email);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task PasswordSignInAsync_ShouldReturnTrue_WhenCredentialsAreCorrect()
    {
        // ARRANGE
        var email = "test@test.com";
        var password = "Password";
        _mockSignInManager.Setup(x => x.PasswordSignInAsync(email, password, false, false))
            .ReturnsAsync(SignInResult.Success);

        // ACT
        var result = await _repository.PasswordSignInAsync(email, password, false);

        // ASSERT
        Assert.True(result);
    }

    [Fact]
    public async Task SignOutAsync_ShouldReturnTrue()
    {
        // ARRANGE
        _mockSignInManager.Setup(x => x.SignOutAsync()).Returns(Task.CompletedTask);

        // ACT
        var result = await _repository.SignOutAsync();

        // ASSERT
        Assert.True(result);
        _mockSignInManager.Verify(x => x.SignOutAsync(), Times.Once);
    }
























}
