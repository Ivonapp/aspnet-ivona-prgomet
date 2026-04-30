using CoreFitness.Domain.Entities;
using CoreFitness.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace CoreFitness.Tests.RepositoryTests;

public class AccountRepositoryTests
{
    private readonly Mock<UserManager<AppUser>> _mockUserManager;
    private readonly AccountRepository _repository;

    public AccountRepositoryTests()
    {
        // Setup för UserManager (samma som i Auth)
        var store = new Mock<IUserStore<AppUser>>();
        _mockUserManager = new Mock<UserManager<AppUser>>(
            store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        // Initiera AccountRepository med mock UserManager
        _repository = new AccountRepository(_mockUserManager.Object);
    }






    [Fact]
    public async Task FindByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // ARRANGE
        var userId = "1";

        var user = new AppUser {
            Id = userId,
            UserName = "testuser"
        };

        _mockUserManager.Setup(x => x.FindByIdAsync(userId))
            .ReturnsAsync(user);

        // ACT
        var result = await _repository.FindByIdAsync(userId);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(userId, result?.Id);
    }






    [Fact]
    public async Task CheckPasswordAsync_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        // ARRANGE
        var user = new AppUser {
            UserName = "testuser"
        };

        var password = "Password";
        _mockUserManager.Setup(x => x.CheckPasswordAsync(user, password))
            .ReturnsAsync(true);

        // ACT
        var result = await _repository.CheckPasswordAsync(user, password);

        // ASSERT
        Assert.True(result);
    }






    [Fact]
    public async Task UpdateProfileAsync_ShouldReturnTrue_WhenUpdateSucceeds()
    {
        // ARRANGE
        var user = new AppUser {
            Id = "1",
            UserName = "updateduser"
        };

        _mockUserManager.Setup(x => x.UpdateAsync(user))
            .ReturnsAsync(IdentityResult.Success);

        // ACT
        var result = await _repository.UpdateProfileAsync(user);

        // ASSERT
        Assert.True(result);
        _mockUserManager.Verify(x => x.UpdateAsync(user), Times.Once);
    }






    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenDeleteSucceeds()
    {
        // ARRANGE
        var user = new AppUser {
            Id = "1"
        };

        _mockUserManager.Setup(x => x.DeleteAsync(user))
            .ReturnsAsync(IdentityResult.Success);

        // ACT
        var result = await _repository.DeleteAsync(user);

        // ASSERT
        Assert.True(result);
        _mockUserManager.Verify(x => x.DeleteAsync(user), Times.Once);
    }
}