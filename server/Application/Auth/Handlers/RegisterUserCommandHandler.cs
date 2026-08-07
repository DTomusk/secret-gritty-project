using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Auth.Interfaces;
using Application.Shared.Interfaces;
using Domain.Shared.Results;

namespace Application.Auth.Handlers;

public class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator,
    IUnitOfWork unitOfWork) : ICommandHandler<RegisterUserCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AuthResponse>> HandleAsync(
        RegisterUserCommand command,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await _userRepository.GetByRegistrationCodeAsync(command.RegistrationCode, cancellationToken);
        if (existingUser == null)
            return Result<AuthResponse>.Failure(new Error("Invalid registration code.", ErrorType.Validation));

        if (existingUser.IsActive)
            return Result<AuthResponse>.Failure(new Error("Registration code has already been used.", ErrorType.Validation));

        var passwordHash = _passwordHasher.HashPassword(command.Password);

        existingUser.ActivateUser(passwordHash);

        await _userRepository.UpdateAsync(existingUser, cancellationToken);

        var token = _tokenGenerator.GenerateToken(existingUser.Id, existingUser.UserName);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Result<AuthResponse>.Success(new AuthResponse(existingUser.Id, existingUser.UserName, token));
    }
}
