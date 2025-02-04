using ErrorOr;

namespace SessionReservation.Domain.SessionAggregate;

public static class SessionErrors
{
    public static readonly Error ReservationNotFound = Error.NotFound(
        "Session.ReservationNotFound",
        "Session reservation not found");

    public static readonly Error ParticipantCannotReserveTwice = Error.Conflict(
        "Session.ParticipantCannotReserveTwice",
        "A participant cannot reserve twice to the same session");

    public static readonly Error ReservationCanceledTooCloseToSession = Error.Validation(
        "Session.ReservationCanceledTooCloseToSession",
        "A participant cannot cancel the reservation too close to session start time");

    public static readonly Error CannotCancelPastSession = Error.Validation(
        "Session.CannotCancelPastSession",
        "A participant cannot cancel a reservation for a session that has completed");

    public static readonly Error CannotHaveMoreReservationsThanParticipants = Error.Validation(
        "Session.CannotHaveMoreReservationsThanParticipants",
        "A session cannot have more reservations than maximum number of participants");
}