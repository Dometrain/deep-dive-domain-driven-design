namespace UserManagement.Infrastructure.IntegrationEvents;

public record OutboxIntegrationEvent(string EventName, string EventContent);