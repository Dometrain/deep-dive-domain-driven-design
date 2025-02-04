using SessionReservation.Core.Common;
using SessionReservation.Domain.Common.Interfaces;
using SessionReservation.Domain.Common.ValueObjects;
using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.SessionAggregate.Events;

using ErrorOr;

namespace SessionReservation.Domain.SessionAggregate;

public class Session : AggregateRoot
{
    private readonly List<Reservation> _reservations = new();
    private readonly List<SessionCategory> _categories = new();

    public int NumParticipants => _reservations.Count;

    public DateOnly Date { get; }

    public TimeRange Time { get; } = null!;

    public string Name { get; } = null!;

    public string Description { get; } = null!;

    public Guid RoomId { get; }

    public int MaxParticipants { get; }

    public IReadOnlyList<SessionCategory> Categories => _categories;

    public Guid TrainerId { get; }

    public Session(
        string name,
        string description,
        int maxParticipants,
        Guid roomId,
        Guid trainerId,
        DateOnly date,
        TimeRange time,
        List<SessionCategory> categories,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Name = name;
        Description = description;
        MaxParticipants = maxParticipants;
        RoomId = roomId;
        TrainerId = trainerId;
        Date = date;
        Time = time;
        _categories = categories;
    }

    public ErrorOr<Success> ReserveSpot(Participant participant)
    {
        if (_reservations.Count >= MaxParticipants)
        {
            return SessionErrors.CannotHaveMoreReservationsThanParticipants;
        }

        var reservation = new Reservation(participantId: participant.Id);
        if (_reservations.Any(existingReservation => existingReservation.ParticipantId == reservation.ParticipantId))
        {
            return SessionErrors.ParticipantCannotReserveTwice;
        }

        _reservations.Add(reservation);
        _domainEvents.Add(new SessionSpotReservedEvent(this, reservation));

        return Result.Success;
    }

    public ErrorOr<Success> CancelReservation(Guid participantId, IDateTimeProvider dateTimeProvider)
    {
        if (!_reservations.Any(reservation => reservation.ParticipantId == participantId))
        {
            return SessionErrors.ReservationNotFound;
        }

        if (IsPastSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.CannotCancelPastSession;
        }

        if (IsTooCloseTooSession(dateTimeProvider.UtcNow))
        {
            return SessionErrors.ReservationCanceledTooCloseToSession;
        }

        var reservation = _reservations.First(reservation => reservation.ParticipantId == participantId);
        _reservations.Remove(reservation);

        _domainEvents.Add(new ReservationCanceledEvent(this, reservation));

        return Result.Success;
    }

    private bool IsPastSession(DateTime utcNow)
    {
        return (Date.ToDateTime(Time.End) - utcNow).TotalHours < 0;
    }

    private bool IsTooCloseTooSession(DateTime utcNow)
    {
        const int MinHours = 24;

        return (Date.ToDateTime(Time.Start) - utcNow).TotalHours < MinHours;
    }

    public bool HasReservationForParticipant(Guid participantId)
    {
        return _reservations.Any(reservation => reservation.ParticipantId == participantId);
    }

    public bool IsBetweenDates(DateTime startDateTime, DateTime endDateTime)
    {
        var sessionDateTime = Date.ToDateTime(Time.Start);

        return sessionDateTime >= startDateTime && sessionDateTime <= endDateTime;
    }

    public void Cancel()
    {
        _domainEvents.Add(new SessionCanceledEvent(this));
    }

    public List<Guid> GetParticipantIds()
    {
        return _reservations.ConvertAll(reservation => reservation.ParticipantId);
    }

    private Session() { }
}