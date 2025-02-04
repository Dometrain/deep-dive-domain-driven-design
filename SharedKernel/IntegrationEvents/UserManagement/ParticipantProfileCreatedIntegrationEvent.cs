namespace SharedKernel.IntegrationEvents.UserManagement;

public record ParticipantProfileCreatedIntegrationEvent(Guid UserId, Guid ParticipantId) : IIntegrationEvent;