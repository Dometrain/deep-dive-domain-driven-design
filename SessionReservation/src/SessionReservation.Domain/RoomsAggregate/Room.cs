using SessionReservation.Core.Common;
using SessionReservation.Domain.Common.Entities;
using SessionReservation.Domain.RoomAggregate.Events;
using SessionReservation.Domain.SessionAggregate;

using ErrorOr;

using Throw;

namespace SessionReservation.Domain.RoomAggregate;

public class Room : AggregateRoot
{
    private readonly int _maxSessions;
    private readonly Schedule _schedule = Schedule.Empty();
    private readonly List<Guid> _sessionIds = new();

    public string Name { get; } = null!;

    public Guid GymId { get; }

    public IReadOnlyList<Guid> SessionIds => _sessionIds.AsReadOnly();

    public Room(
        string name,
        int maxDailySessions,
        Guid gymId,
        Schedule? schedule = null,
        Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _maxSessions = maxDailySessions;
        GymId = gymId;
        _schedule = schedule ?? Schedule.Empty();
    }

    public ErrorOr<Success> ScheduleSession(Session session)
    {
        _sessionIds.Throw()
            .IfTrue(ids => ids.Any(id => id == session.Id));

        if (_sessionIds.Count >= _maxSessions)
        {
            return RoomErrors.CannotHaveMoreSessionThanSubscriptionAllows;
        }

        var addEventResult = _schedule.BookTimeSlot(session.Date, session.Time);

        if (addEventResult.IsError && addEventResult.FirstError.Type == ErrorType.Conflict)
        {
            return RoomErrors.CannotHaveTwoOrMoreOverlappingSessions;
        }

        _sessionIds.Add(session.Id);
        _domainEvents.Add(new SessionScheduledEvent(this, session));

        return Result.Success;
    }

    public bool HasSession(Guid sessionId)
    {
        return _sessionIds.Contains(sessionId);
    }

    private Room() { }
}