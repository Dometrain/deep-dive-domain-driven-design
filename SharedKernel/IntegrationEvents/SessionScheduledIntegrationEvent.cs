namespace SharedKernel.IntegrationEvents;

public record SessionScheduledIntegrationEvent(Guid RoomId, Guid TrainerId) : IIntegrationEvent;