using ErrorOr;

using MediatR;

namespace UserManagement.Application.Profiles.CreateAdminProfile;

public record CreateAdminProfileCommand(Guid UserId)
    : IRequest<ErrorOr<Guid>>;
