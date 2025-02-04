using ErrorOr;

using MediatR;

using UserManagement.Application.Common.Interfaces;
using UserManagement.Application.Profiles.CreateParticipantProfile;

namespace UserManagement.Application.Profiles.Commands.CreateProfile;

public class CreateParticipantProfileCommandHandler : IRequestHandler<CreateParticipantProfileCommand, ErrorOr<Guid>>
{
    private readonly IUsersRepository _usersRepository;

    public CreateParticipantProfileCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateParticipantProfileCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        var createParticipantProfileResult = user.CreateParticipantProfile();

        await _usersRepository.UpdateAsync(user);

        return createParticipantProfileResult;
    }
}