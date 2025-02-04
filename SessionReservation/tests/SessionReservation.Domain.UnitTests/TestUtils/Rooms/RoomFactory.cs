using SessionReservation.Domain.RoomAggregate;
using SessionReservation.Domain.UnitTests.TestConstants;

namespace SessionReservation.Domain.UnitTests.TestUtils.Rooms;

public static class RoomFactory
{
    public static Room CreateRoom(
        string name = Constants.Room.Name,
        int maxSessions = Constants.Room.MaxSessions,
        Guid? gymId = null,
        Guid? id = null)
    {
        return new Room(
            name: name,
            maxDailySessions: maxSessions,
            gymId: gymId ?? Constants.Gym.Id,
            id: id ?? Constants.Room.Id);
    }
}