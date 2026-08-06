using Application.Auth.Commands;
using Application.Auth.Interfaces;
using Application.Shared.Interfaces;
using Domain.Auth.Entities;
using Domain.Shared.Results;

namespace Application.Auth.Handlers;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var existingUser = await _userRepository.GetByUserNameAsync(command.UserName, cancellationToken);
        if (existingUser != null)
            return Result.Failure(new Error("User with the same username already exists.", ErrorType.Validation));

        var user = User.Create(command.UserName);
        await _userRepository.CreateAsync(user, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return Result.Success();
    }
}
