using SessionReservation.Domain.Common;
using SessionReservation.Domain.Common.EventualConsistency;

using ErrorOr;

namespace SessionReservation.Domain.SessionAggregate.Events;

public record SessionSpotReservedEvent(Session Session, Reservation Reservation) : IDomainEvent
{
    public static readonly Error ParticipantScheduleUpdateFailed = EventualConsistencyError.From(
        code: "SessionSpotReserved.ParticipantScheduleUpdateFailed",
        description: "Adding session to participant schedule failed");
}