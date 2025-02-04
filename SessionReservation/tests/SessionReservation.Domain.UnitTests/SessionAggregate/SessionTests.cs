using SessionReservation.Domain.SessionAggregate;
using SessionReservation.Domain.UnitTests.SessionAggregate.TestUtils;
using SessionReservation.Domain.UnitTests.TestConstants;
using SessionReservation.Domain.UnitTests.TestUtils.Participants;
using SessionReservation.Domain.UnitTests.TestUtils.Services;
using FluentAssertions;

namespace SessionReservation.Domain.UnitTests.SessionAggregate;

public class SessionTests
{
    [Fact]
    public void ReserveSpot_WhenNoMoreRoom_ShouldFailReservation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(maxParticipants: 1);
        var participant = ParticipantFactory.CreateParticipant();

        // Act
        var firstReservationResult = session.ReserveSpot(participant);
        var secondReservationResult = session.ReserveSpot(participant);

        // Assert
        firstReservationResult.IsError.Should().BeFalse();
        secondReservationResult.IsError.Should().BeTrue();
        secondReservationResult.FirstError.Should().Be(SessionErrors.CannotHaveMoreReservationsThanParticipants);
    }

    [Fact]
    public void CancelReservation_WhenCancellationTimeTooCloseToSession_ShouldFailCancellation()
    {
        // Arrange
        var session = SessionFactory.CreateSession(date: Constants.Session.Date);
        var participant = ParticipantFactory.CreateParticipant();

        var dateAndTimeOfCancellation = Constants.Session.Date.ToDateTime(TimeOnly.MinValue);

        // Act
        var reservationResult = session.ReserveSpot(participant);
        var cancellationResult = session.CancelReservation(
            participantId: participant.Id,
            dateTimeProvider: new TestDateTimeProvider(fixedDateTime: dateAndTimeOfCancellation));

        // Assert
        reservationResult.IsError.Should().BeFalse();
        cancellationResult.IsError.Should().BeTrue();
        cancellationResult.FirstError.Should().Be(SessionErrors.ReservationCanceledTooCloseToSession);
    }
}