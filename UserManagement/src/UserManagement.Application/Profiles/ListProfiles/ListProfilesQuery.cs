using ErrorOr;

using MediatR;

namespace UserManagement.Application.Profiles.ListProfiles;

public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<ListProfilesResult>>;