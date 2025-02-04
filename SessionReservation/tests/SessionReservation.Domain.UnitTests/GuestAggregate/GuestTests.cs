using SessionReservation.Domain.ParticipantAggregate;
using SessionReservation.Domain.UnitTests.SessionAggregate.TestUtils;
using SessionReservation.Domain.UnitTests.TestConstants;
using SessionReservation.Domain.UnitTests.TestUtils.CommonValueObjects;
using SessionReservation.Domain.UnitTests.TestUtils.Participants;
using FluentAssertions;

namespace SessionReservation.Domain.UnitTests.ParticipantAggregate;

public class ParticipantTests
{
    [Theory]
    [InlineData(1, 3, 1, 3)]
    [InlineData(1, 3, 2, 3)]
    [InlineData(1, 3, 2, 4)]
    [InlineData(1, 3, 0, 2)]
    public void AddSessionToSchedule_WhenSessionOverlapsWithAnotherSession_ShouldFail(
        int startHourSession1,
        int endHourSession1,
        int startHourSession2,
        int endHourSession2)
    {
        // Arrange
        var participant = ParticipantFactory.CreateParticipant();

        var session1 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession1, endHourSession1),
            id: Guid.NewGuid());

        var session2 = SessionFactory.CreateSession(
            date: Constants.Session.Date,
            time: TimeRangeFactory.CreateFromHours(startHourSession2, endHourSession2),
            id: Guid.NewGuid());

        // Act
        var addSession1Result = participant.AddToSchedule(session1);
        var addSession2Result = participant.AddToSchedule(session2);

        // Assert
        addSession1Result.IsError.Should().BeFalse();

        addSession2Result.IsError.Should().BeTrue();
        addSession2Result.FirstError.Should().Be(ParticipantErrors.CannotHaveTwoOrMoreOverlappingSessions);
    }
}