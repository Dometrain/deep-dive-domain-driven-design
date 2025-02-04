namespace SharedKernel.IntegrationEvents.UserManagement;

public record TrainerProfileCreatedIntegrationEvent(Guid UserId, Guid TrainerId) : IIntegrationEvent;