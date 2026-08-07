using Domain.Auth.Entities;
using FluentAssertions;
using Xunit;

namespace Domain.UnitTests.Auth.Entities;

public class UserTests
{
    [Fact]
    public void Create_Should_Create_User_With_Valid_Properties()
    {
        // Arrange
        var userName = "John Doe";

        // Act
        var user = User.Create(userName);

        // Assert
        user.Id.Should().NotBe(Guid.Empty);
        user.UserName.Should().Be(userName);
        user.PasswordHash.Should().Be("");
        user.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void Create_Should_Throw_When_DisplayName_Is_Invalid(string? invalidUserName)
    {
        // Act
        var act = () => User.Create(invalidUserName!);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("User name cannot be empty or whitespace.*")
            .WithParameterName("userName");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void Activate_Should_Throw_When_PasswordHash_Is_Invalid(string? invalidPasswordHash)
    {
        // Arrange
        var userName = "John Doe";
        var user = User.Create(userName);

        // Act
        var act = () => user.ActivateUser(invalidPasswordHash!);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Password hash cannot be empty or whitespace.*")
            .WithParameterName("passwordHash");
    }

    [Fact]
    public void Create_Should_Generate_Unique_Id_For_Each_User()
    {
        // Arrange & Act
        var user1 = User.Create("User 1");
        var user2 = User.Create("User 2");

        // Assert
        user1.Id.Should().NotBe(user2.Id);
        user1.Id.Should().NotBe(Guid.Empty);
        user2.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void UpdateDisplayName_Should_Update_DisplayName()
    {
        // Arrange
        var user = User.Create("Original Name");
        var newUserName = "Updated Name";

        // Act
        user.UpdateUserName(newUserName);

        // Assert
        user.UserName.Should().Be(newUserName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void UpdateUserName_Should_Throw_When_UserName_Is_Invalid(string? invalidUserName)
    {
        // Arrange
        var user = User.Create("Original Name");

        // Act
        var act = () => user.UpdateUserName(invalidUserName!);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("User name cannot be empty or whitespace.*")
            .WithParameterName("newUserName");
    }

    [Fact]
    public void UpdatePassword_Should_Update_PasswordHash()
    {
        // Arrange
        var user = User.Create("Test User");
        var newPasswordHash = "new_hash_123";

        // Act
        user.UpdatePassword(newPasswordHash);

        // Assert
        user.PasswordHash.Should().Be(newPasswordHash);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("\t")]
    public void UpdatePassword_Should_Throw_When_PasswordHash_Is_Invalid(string? invalidPasswordHash)
    {
        // Arrange
        var user = User.Create("Test User");

        // Act
        var act = () => user.UpdatePassword(invalidPasswordHash!);

        // Assert
        act.Should().Throw<ArgumentException>()
            .WithMessage("Password hash cannot be empty or whitespace.*")
            .WithParameterName("newPasswordHash");
    }

    [Fact]
    public void User_Properties_Should_Have_Private_Setters()
    {
        // Arrange
        var user = User.Create("Test User");

        // Assert
        var idProperty = typeof(User).GetProperty(nameof(User.Id));
        var passwordHashProperty = typeof(User).GetProperty(nameof(User.PasswordHash));
        var userNameProperty = typeof(User).GetProperty(nameof(User.UserName));
        var createdAtProperty = typeof(User).GetProperty(nameof(User.CreatedAt));

        idProperty!.SetMethod!.IsPrivate.Should().BeTrue();
        passwordHashProperty!.SetMethod!.IsPrivate.Should().BeTrue();
        userNameProperty!.SetMethod!.IsPrivate.Should().BeTrue();
        createdAtProperty!.SetMethod!.IsPrivate.Should().BeTrue();
    }

    [Fact]
    public void CreatedAt_Should_Be_Set_To_UtcNow_When_User_Is_Created()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var user = User.Create("Test User");

        // Assert
        var afterCreation = DateTime.UtcNow;
        user.CreatedAt.Should().BeOnOrAfter(beforeCreation);
        user.CreatedAt.Should().BeOnOrBefore(afterCreation);
    }
}
