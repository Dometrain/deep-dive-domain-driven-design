namespace GymManagement.Infrastructure.IntegrationEvents;

public record OutboxIntegrationEvent(string EventName, string EventContent);