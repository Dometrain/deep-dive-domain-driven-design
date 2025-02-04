using ErrorOr;

using MediatR;

namespace UserManagement.Application.Profiles.CreateParticipantProfile;

public record CreateParticipantProfileCommand(Guid UserId)
    : IRequest<ErrorOr<Guid>>;
