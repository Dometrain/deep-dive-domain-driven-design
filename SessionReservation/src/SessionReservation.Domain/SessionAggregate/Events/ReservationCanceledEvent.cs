using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.EventualConsistency;

using ErrorOr;

namespace SessionReservation.Domain.SessionAggregate.Events;

public record ReservationCanceledEvent(Session Session, Reservation Reservation) : IDomainEvent
{
    public static readonly Error ParticipantNotFound = EventualConsistencyError.From(
        code: "ReservationCanceledEvent.ParticipantNotFound",
        description: "Participant not found");

    public static readonly Error ParticipantScheduleUpdateFailed = EventualConsistencyError.From(
        code: "ReservationCanceledEvent.ParticipantScheduleUpdateFailed",
        description: "Removing session from participant schedule failed");
}