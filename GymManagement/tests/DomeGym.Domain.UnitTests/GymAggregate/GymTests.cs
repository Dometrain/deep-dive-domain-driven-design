using GymManagement.Domain.GymAggregate;
using GymManagement.Domain.UnitTests.TestUtils.Gyms;
using GymManagement.Domain.UnitTests.TestUtils.Rooms;

using FluentAssertions;

namespace GymManagement.Domain.UnitTests.GymAggregate;

public class GymTests
{
    [Fact]
    public void AddRoom_WhenMoreThanSubscriptionAllows_ShouldFail()
    {
        // Arrange
        var gym = GymFactory.CreateGym(maxRooms: 1);
        var room1 = RoomFactory.CreateRoom(id: Guid.NewGuid());
        var room2 = RoomFactory.CreateRoom(id: Guid.NewGuid());

        // Act
        var addRoom1Result = gym.AddRoom(room1);
        var addRoom2Result = gym.AddRoom(room2);

        // Assert
        addRoom1Result.IsError.Should().BeFalse();

        addRoom2Result.IsError.Should().BeTrue();
        addRoom2Result.FirstError.Should().Be(GymErrors.CannotHaveMoreRoomsThanSubscriptionAllows);
    }
}