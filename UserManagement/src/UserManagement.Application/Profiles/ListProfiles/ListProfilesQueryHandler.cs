using ErrorOr;

using MediatR;

using UserManagement.Application.Common.Interfaces;

namespace UserManagement.Application.Profiles.ListProfiles;

public class ListProfilesQueryHandler : IRequestHandler<ListProfilesQuery, ErrorOr<ListProfilesResult>>
{
    private readonly IUsersRepository _usersRepository;

    public ListProfilesQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<ListProfilesResult>> Handle(ListProfilesQuery query, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found");
        }

        return new ListProfilesResult(user.AdminId, user.ParticipantId, user.TrainerId);
    }
}