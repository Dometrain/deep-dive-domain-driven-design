using SessionReservation.Core.Common;

namespace SessionReservation.Domain.SessionAggregate;

public class Reservation : Entity
{
    public Guid ParticipantId { get; }

    public Reservation(
        Guid participantId,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        ParticipantId = participantId;
    }

    private Reservation() { }
}