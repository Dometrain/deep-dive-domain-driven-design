using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

using MediatR;

using SharedKernel.IntegrationEvents.GymManagement;

namespace SessionReservation.Application.Rooms.IntegrationEvents;

public class RoomRemovedEventHandler : INotificationHandler<RoomRemovedIntegrationEvent>
{
    private readonly IRoomsRepository _roomsRepository;

    public RoomRemovedEventHandler(IRoomsRepository roomsRepository)
    {
        _roomsRepository = roomsRepository;
    }

    public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        Room? room = await _roomsRepository.GetByIdAsync(notification.RoomId);

        if (room is not null)
        {
            await _roomsRepository.RemoveAsync(room);
        }
    }
}