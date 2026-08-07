using Application.Auth.Commands;
using Application.Auth.Handlers;
using Application.Auth.Interfaces;
using Application.Shared.Interfaces;
using Domain.Auth.Entities;
using Domain.Auth.ValueObjects;
using Domain.Shared.Results;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Application.UnitTests.Auth.Handlers;

public class RegisterUserCommandHandlerTests
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _tokenGenerator = Substitute.For<ITokenGenerator>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new RegisterUserCommandHandler(_userRepository, _passwordHasher, _tokenGenerator, _unitOfWork);
    }

    [Fact]
    public async Task HandleAsync_Should_Return_Success_With_AuthResponse_When_User_Registered_Successfully()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "jwt_token";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(invitedUser.Id, userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        result.Value.Should().NotBeNull();
        result.Value.UserName.Should().Be(userName);
        result.Value.Token.Should().Be(token);
        result.Value.UserId.Should().Be(invitedUser.Id);
    }

    [Fact]
    public async Task HandleAsync_Should_Return_Failure_When_Registration_Code_Is_Invalid()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var password = "password123";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns((User?)null);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Message.Should().Be("Invalid registration code.");
        result.Error.Type.Should().Be(ErrorType.Validation);
    }

    [Fact]
    public async Task HandleAsync_Should_Return_Failure_When_Registration_Code_Already_Used()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var password = "password123";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var activeUser = User.Create("inviteduser");
        activeUser.ActivateUser("existing_password_hash");

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(activeUser);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().NotBeNull();
        result.Error.Message.Should().Be("Registration code has already been used.");
        result.Error.Type.Should().Be(ErrorType.Validation);
    }

    [Fact]
    public async Task HandleAsync_Should_Call_GetByRegistrationCodeAsync_With_Correct_Code()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "jwt_token";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(Arg.Any<Guid>(), userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        await _userRepository.Received(1).GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_Should_Hash_Password_With_Correct_Value()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "jwt_token";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(Arg.Any<Guid>(), userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _passwordHasher.Received(1).HashPassword(password);
    }

    [Fact]
    public async Task HandleAsync_Should_Activate_User_With_Password_Hash()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "jwt_token";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(Arg.Any<Guid>(), userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        await _userRepository.Received(1).UpdateAsync(
            Arg.Is<User>(u => u.UserName == userName && u.IsActive && u.PasswordHash == passwordHash),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_Should_Generate_Token_With_User_Id_And_UserName()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "jwt_token";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(invitedUser.Id, userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _tokenGenerator.Received(1).GenerateToken(invitedUser.Id, userName);
    }

    [Fact]
    public async Task HandleAsync_Should_Commit_Changes()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "jwt_token";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(Arg.Any<Guid>(), userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        await _unitOfWork.Received(1).CommitAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task HandleAsync_Should_Return_AuthResponse_With_Correct_UserId()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "jwt_token";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(invitedUser.Id, userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.Value.UserId.Should().Be(invitedUser.Id);
    }

    [Fact]
    public async Task HandleAsync_Should_Return_AuthResponse_With_Correct_UserName()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser123";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "jwt_token";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(Arg.Any<Guid>(), userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.Value.UserName.Should().Be(userName);
    }

    [Fact]
    public async Task HandleAsync_Should_Return_AuthResponse_With_Generated_Token()
    {
        // Arrange
        var registrationCode = new RegistrationCode();
        var userName = "inviteduser";
        var password = "password123";
        var passwordHash = "hashed_password";
        var token = "generated_jwt_token_12345";
        var command = new RegisterUserCommand(registrationCode.ToString(), password);

        var invitedUser = User.Create(userName);

        _userRepository.GetByRegistrationCodeAsync(registrationCode, Arg.Any<CancellationToken>())
            .Returns(invitedUser);

        _passwordHasher.HashPassword(password)
            .Returns(passwordHash);

        _tokenGenerator.GenerateToken(Arg.Any<Guid>(), userName)
            .Returns(token);

        _unitOfWork.CommitAsync(Arg.Any<CancellationToken>())
            .Returns(1);

        // Act
        var result = await _handler.HandleAsync(command);

        // Assert
        result.Value.Token.Should().Be(token);
    }
}
