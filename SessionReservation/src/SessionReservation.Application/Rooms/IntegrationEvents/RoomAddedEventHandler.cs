using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

using MediatR;

using SharedKernel.IntegrationEvents.GymManagement;

namespace SessionReservation.Application.Rooms.IntegrationEvents;

public class RoomAddedEventHandler : INotificationHandler<RoomAddedIntegrationEvent>
{
    private readonly IRoomsRepository _roomsRepository;

    public RoomAddedEventHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task Handle(RoomAddedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var room = new Room(
            notification.Name,
            notification.MaxDailySessions,
            notification.GymId,
            id: notification.RoomId);

        await _roomsRepository.AddRoomAsync(room);
    }
}