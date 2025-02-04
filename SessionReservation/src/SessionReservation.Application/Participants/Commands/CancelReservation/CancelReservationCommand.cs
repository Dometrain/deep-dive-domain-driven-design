using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Participants.Commands.CancelReservation;

public record CancelReservationCommand(Guid ParticipantId, Guid SessionId) : IRequest<ErrorOr<Deleted>>;