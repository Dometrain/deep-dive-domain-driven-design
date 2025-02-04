using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.RoomAggregate.Events;

using MediatR;

namespace SessionReservation.Application.Sessions.Events;

public class SessionScheduledEventHandler : INotificationHandler<SessionScheduledEvent>
{
    private readonly ISessionsRepository _sessionsRepository;

    public SessionScheduledEventHandler(ISessionsRepository sessionsRepository)
    {
        _sessionsRepository = sessionsRepository;
    }

    public async Task Handle(SessionScheduledEvent notification, CancellationToken cancellationToken)
    {
        await _sessionsRepository.AddSessionAsync(notification.Session);
    }
}
