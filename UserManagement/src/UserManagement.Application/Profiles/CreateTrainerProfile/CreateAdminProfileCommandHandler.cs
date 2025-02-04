using ErrorOr;

using MediatR;

using UserManagement.Application.Common.Interfaces;

namespace UserManagement.Application.Profiles.CreateTrainerProfile;

public class CreateTrainerProfileCommandHandler : IRequestHandler<CreateTrainerProfileCommand, ErrorOr<Guid>>
{
    private readonly IUsersRepository _usersRepository;

    public CreateTrainerProfileCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateTrainerProfileCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        var createTrainerProfileResult = user.CreateTrainerProfile();

        await _usersRepository.UpdateAsync(user);

        return createTrainerProfileResult;
    }
}