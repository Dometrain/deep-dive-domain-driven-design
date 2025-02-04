using System.Text.Json;

using MediatR;

using SharedKernel.IntegrationEvents;
using SharedKernel.IntegrationEvents.UserManagement;

using UserManagement.Domain.UserAggregate.Events;
using UserManagement.Infrastructure.IntegrationEvents;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.Api.IntegrationEvents;

public class OutboxWriterEventHandler
    : INotificationHandler<AdminProfileCreatedEvent>,
      INotificationHandler<ParticipantProfileCreatedEvent>,
      INotificationHandler<TrainerProfileCreatedEvent>

{
    private readonly UserManagementDbContext _dbContext;

    public OutboxWriterEventHandler(UserManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(AdminProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new AdminProfileCreatedIntegrationEvent(notification.UserId, notification.AdminId);
        await AddOutboxIntegrationEventAsync(integrationEvent);
    }

    public async Task Handle(ParticipantProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ParticipantProfileCreatedIntegrationEvent(notification.UserId, notification.ParticipantId);
        await AddOutboxIntegrationEventAsync(integrationEvent);
    }

    public async Task Handle(TrainerProfileCreatedEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new TrainerProfileCreatedIntegrationEvent(notification.UserId, notification.TrainerId);
        await AddOutboxIntegrationEventAsync(integrationEvent);
    }

    private async Task AddOutboxIntegrationEventAsync(IIntegrationEvent integrationEvent)
    {
        await _dbContext.OutboxIntegrationEvents.AddAsync(new OutboxIntegrationEvent(
            EventName: integrationEvent.GetType().Name,
            EventContent: JsonSerializer.Serialize(integrationEvent)));

        await _dbContext.SaveChangesAsync();
    }
}
