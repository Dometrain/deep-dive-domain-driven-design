using ErrorOr;

using MediatR;

namespace SessionReservation.Application.Reservations.Commands.CreateReservation;

public record CreateReservationCommand(
    Guid SessionId,
    Guid ParticipantId) : IRequest<ErrorOr<Success>>;