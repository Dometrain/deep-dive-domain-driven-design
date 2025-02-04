using ErrorOr;

using MediatR;

using UserManagement.Application.Common.Interfaces;

namespace UserManagement.Application.Profiles.CreateAdminProfile;

public class CreateAdminProfileCommandHandler : IRequestHandler<CreateAdminProfileCommand, ErrorOr<Guid>>
{
    private readonly IUsersRepository _usersRepository;

    public CreateAdminProfileCommandHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateAdminProfileCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        var createAdminProfileResult = user.CreateAdminProfile();

        await _usersRepository.UpdateAsync(user);

        return createAdminProfileResult;
    }
}