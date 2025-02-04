using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate;

using MediatR;

using SharedKernel.IntegrationEvents.GymManagement;


namespace SessionReservation.Application.Sessions.IntegrationEvents;

public class RoomRemovedEventHandler : INotificationHandler<RoomRemovedIntegrationEvent>
{
    private readonly ISessionsRepository _sessionsRepository;

    public RoomRemovedEventHandler(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(RoomRemovedIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var sessions = await _sessionsRepository.ListByRoomId(notification.RoomId);

        sessions.ForEach(session => session.Cancel());

        await _sessionsRepository.RemoveRangeAsync(sessions);
    }
}