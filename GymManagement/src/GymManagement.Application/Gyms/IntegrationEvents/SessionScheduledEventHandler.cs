using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Common.EventualConsistency;

using MediatR;

using SharedKernel.IntegrationEvents;

using Throw;

namespace GymManagement.Application.Gyms.IntegrationEvents;

public class SessionScheduledEventHandler : INotificationHandler<SessionScheduledIntegrationEvent>
{
    private readonly IGymsRepository _gymsRepository;

    public SessionScheduledEventHandler(IGymsRepository gymsRepository)
    {
        _gymsRepository = gymsRepository;
    }

    public async Task Handle(SessionScheduledIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var gym = await _gymsRepository.GetByIdAsync(notification.RoomId);
        gym.ThrowIfNull();

        gym.AddTrainer(notification.TrainerId);
    }
}
