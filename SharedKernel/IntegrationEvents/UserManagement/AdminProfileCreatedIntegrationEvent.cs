namespace SharedKernel.IntegrationEvents.UserManagement;

public record AdminProfileCreatedIntegrationEvent(Guid UserId, Guid AdminId) : IIntegrationEvent;