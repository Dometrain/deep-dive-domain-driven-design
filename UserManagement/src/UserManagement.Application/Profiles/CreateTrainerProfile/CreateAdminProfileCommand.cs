using ErrorOr;

using MediatR;

namespace UserManagement.Application.Profiles.CreateTrainerProfile;

public record CreateTrainerProfileCommand(Guid UserId)
    : IRequest<ErrorOr<Guid>>;
